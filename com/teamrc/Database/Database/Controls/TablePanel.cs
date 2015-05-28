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
	public partial class TablePanel : UserControl {
		private DatabasePanel _owner;
		private Table _table;
		private List<QuestionRow> _rows;
		private bool _dbLoaded;
		private bool _drop;

		public TablePanel(DatabasePanel p, Database d) {
			InitializeComponent();
			this._owner = p;
			this._table = d.AddNewTable("Default");
			this._rows = new List<QuestionRow>();
			this.pnlMain.Location = new Point(0, this.btnAdd.Height + 5);
			this.pnlMain.Size = new Size(this.Width, this.Height - this.btnAdd.Height - 5);
			this._dbLoaded = false;
			this._drop = false;
		}

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

		public string TableName {
			get { return this._table.Name; }
		}

		public bool Loaded {
			get { return this._dbLoaded; }
			set { this._dbLoaded = value; }
		}

		public void updateRows() {
			this._table.sort();

			for(int x = 0; x < this._rows.Count; x++) {
				this._rows[x].toQuestion(this, new EventArgs());
			}
		}

		public void addRow(object sender, EventArgs e) {
			this._rows.Sort();
			this._table.sort();

			QuestionRow qR = new QuestionRow(this, this._table);
			qR.Parent = this;

			this._rows.Add(qR);
			this.pnlMain.Controls.Add(qR);
			this.recalcControls(this, new ControlEventArgs(this));
		}

		public void loadRows() {
			for(int x = 0; x < this._table.Count; x++) {
				QuestionRow qR = new QuestionRow(this, this._table[x]);
				qR.Location = new Point((this.Width / 2) - (qR.Width / 2), 5 + (x * qR.Height));
				qR.Parent = this;

				this._rows.Add(qR);
				this.pnlMain.Controls.Add(qR);
			}
		}

		public void deleteRow(QuestionRow qR) {
			if(this._rows.Contains(qR)) {
				this._rows.Remove(qR);
				this.pnlMain.Controls.Remove(qR);
			}
		}

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

		public void setAllAsLoaded() {
			for(int x = 0; x < this._rows.Count; x++) {
				this._rows[x].Loaded = true;
			}
		}

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
					this._table.Owner.RemoveTable(this._table);
				}
			} else {
				this._drop = false;
				this._table.Drop = false;
				this.pnlMain.Enabled = true;
				this.btnAdd.Enabled = true;
				this.btnDrop.Text = "Drop Table";
			}
		}

		private void recalcControls(object sender, ControlEventArgs e) {

			this._rows.Sort();

			for(int x = 0; x < this._rows.Count; x++) {
				this._rows[x].Location = new Point((this.Width / 2) - (this._rows[x].Width / 2), 5 + (x * this._rows[x].Height));
			}
		}

		private void TablePanel_Resize(object sender, EventArgs e) {
			this.btnAdd.Location = new Point(this.Width - this.btnAdd.Width - 10, 0);
			this.pnlMain.Location = new Point(0, this.btnAdd.Height + 5);
			this.pnlMain.Size = new Size(this.Width, this.Height - this.btnAdd.Height - 5);
		}

		private void txtName_Leave(object sender, EventArgs e) {
			this._table.Name = this.txtName.Text;
		}

		private void TablePanel_Validated(object sender, EventArgs e) {
			this.recalcControls(this, new ControlEventArgs(this));
		}
	}
}