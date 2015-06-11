using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseSystem.Controls {
	/// <summary>
	/// A control that represents a table.
	/// </summary>
	public partial class TablePanel : UserControl {
		private DatabasePanel _owner;
		private Table _table;
		private List<QuestionRow> _rows;
		private bool _dbLoaded;
		private bool _drop;

		/// <summary>
		/// Creates a new panel. Considered new.
		/// </summary>
		/// <param name="p">The DatabasePanel this panel will be associated with.</param>
		/// <param name="d">The Database the new table will be associated with.</param>
		public TablePanel(DatabasePanel p, Database d) {
			InitializeComponent();
			this._owner = p;
			this._table = d.addNewTable("Default");
			this._rows = new List<QuestionRow>();
			this.pnlMain.Location = new Point(0, this.btnAdd.Height + 5);
			this.pnlMain.Size = new Size(this.Width, this.Height - this.btnAdd.Height - 5);
			this._dbLoaded = false;
			this._drop = false;
		}

		/// <summary>
		/// Creates a panel based on an existing table. Considered old.
		/// </summary>
		/// <param name="p">The DatabasePanel this panel will be associated with.</param>
		/// <param name="t">The table this panel is based on.</param>
		public TablePanel(DatabasePanel p, Table t) {
			InitializeComponent();
			this._owner = p;
			this._table = t;
			this._rows = new List<QuestionRow>(t.Count);
			this.txtName.Text = t.Name;
			this.txtName.ReadOnly = true;

			this.loadRows();
			this._dbLoaded = true;
			this._drop = false;
		}

		/// <summary>
		/// Gets the name of the associated table.
		/// </summary>
		public string TableName {
			get { return this._table.Name; }
		}

		/// <summary>
		/// Gets or sets whether this panel will be deleted on save.
		/// </summary>
		public bool Drop {
			get { return this._drop; }
			set { this._drop = value; }
		}

		/// <summary>
		/// Gets or sets whether this panel is new or old.
		/// </summary>
		public bool Loaded {
			get { return this._dbLoaded; }
			set { this._dbLoaded = value; }
		}

		/// <summary>
		/// Sorts the rows and updates the contained QuestionAnswers.
		/// </summary>
		public void updateRows() {
			this._table.sort();

			for(int x = 0; x < this._rows.Count; x++) {
				this._rows[x].toQuestion(this, new EventArgs());
			}
		}

		/// <summary>
		/// An event method that adds a new QuestionRow to the panel.
		/// </summary>
		/// <param name="sender">The sender of the method.</param>
		/// <param name="e">The event arguments from the event.</param>
		public void addRow(object sender, EventArgs e) {
			this._rows.Sort();
			this._table.sort();

			QuestionRow qR = new QuestionRow(this, this._table);
			qR.Parent = this;

			this._rows.Add(qR);
			this.pnlMain.Controls.Add(qR);
			this.recalcControls(this, new ControlEventArgs(this));
		}

		/// <summary>
		/// Create rows for all QuestionAnswers in the table and add them to the panel.
		/// </summary>
		public void loadRows() {
			for(int x = 0; x < this._table.Count; x++) {
				QuestionRow qR = new QuestionRow(this, this._table[x]);
				qR.Location = new Point((this.Width / 2) - (qR.Width / 2), 5 + (x * qR.Height));
				qR.Parent = this;

				this._rows.Add(qR);
				this.pnlMain.Controls.Add(qR);
			}
		}

		/// <summary>
		/// Removes the QuestionRow from the panel.
		/// </summary>
		/// <param name="qR"></param>
		public void deleteRow(QuestionRow qR) {
			if(this._rows.Contains(qR)) {
				this._rows.Remove(qR);
				this.pnlMain.Controls.Remove(qR);
			}
		}

		/// <summary>
		/// Remove all QuestionRows set to be deleted.
		/// </summary>
		public void clearRows() {
			for(int x = 0; x < this._rows.Count; x++) {
				if(this._rows[x].Drop) {
					this.pnlMain.Controls.Remove(this._rows[x]);
					this._rows.RemoveAt(x);
					x--;
				}
			}

			this.recalcControls(this, new ControlEventArgs(this));
		}

		/// <summary>
		/// Sets all QuestionRows as old rows.
		/// </summary>
		public void setAllAsLoaded() {
			for(int x = 0; x < this._rows.Count; x++) {
				this._rows[x].Loaded = true;
			}
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void dropTable(object sender, EventArgs e) {
			if(!this._drop) {
				if(this._dbLoaded) {
					this._drop = true;
					this._table.Drop = true;
					this.pnlMain.Enabled = false;
					this.btnAdd.Enabled = false;
					this.btnDrop.Text = "Cancel Drop";
				} else {
					this._owner.dropTable(this);
					this._table.Owner.removeTable(this._table);
				}
			} else {
				this._drop = false;
				this._table.Drop = false;
				this.pnlMain.Enabled = true;
				this.btnAdd.Enabled = true;
				this.btnDrop.Text = "Drop Table";
			}
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void recalcControls(object sender, ControlEventArgs e) {

			this._rows.Sort();

			for(int x = 0; x < this._rows.Count; x++) {
				this._rows[x].Location = new Point((this.Width / 2) - (this._rows[x].Width / 2), 5 + (x * this._rows[x].Height));
			}
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void TablePanel_Resize(object sender, EventArgs e) {
			this.btnAdd.Location = new Point(this.Width - this.btnAdd.Width - 10, 0);
			this.pnlMain.Location = new Point(0, this.btnAdd.Height + 5);
			this.pnlMain.Size = new Size(this.Width, this.Height - this.btnAdd.Height - 5);
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void txtName_Leave(object sender, EventArgs e) {
			this._table.Name = this.txtName.Text;
			this._table.Name = this._table.Name.Replace("'", "");
			this._table.Name = this._table.Name.Replace(" ", "_");
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void TablePanel_Validated(object sender, EventArgs e) {
			this.recalcControls(this, new ControlEventArgs(this));
		}
	}
}