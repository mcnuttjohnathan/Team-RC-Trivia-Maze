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
	/// A control that represents a database.
	/// </summary>
	public partial class DatabasePanel : UserControl {
		private DBManipulate _owner;
		private Database _database;
		private List<TablePanel> _panels;

		/// <summary>
		/// Creates a panel based on the passed in database.
		/// </summary>
		/// <param name="m">The manipulator using this panel.</param>
		/// <param name="d">The database that this panel is based on.</param>
		public DatabasePanel(DBManipulate m, Database d) {
			InitializeComponent();
			this._owner = m;
			this._database = d;
			this._panels = new List<TablePanel>(d.Count);

			this.importTables();
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void addTable(object sender, EventArgs e) {
			TablePanel tP = new TablePanel(this, this._database);

			this._panels.Add(tP);
			this.pnlMain.Controls.Add(tP);
			this.recalcControls(this, new ControlEventArgs(this));
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void importTables() {
			for(int x = 0; x < this._database.Count; x++) {
				TablePanel tP = new TablePanel(this, this._database[x]);

				this._panels.Add(tP);
				this.pnlMain.Controls.Add(tP);
			}

			this.recalcControls(this, new ControlEventArgs(this));
		}

		/// <summary>
		/// Removes the passed in TablePanel from the list.
		/// </summary>
		/// <param name="p">The TablePanel to be removed.</param>
		public void dropTable(TablePanel p) {
			if(this._panels.Contains(p)) {
				this._panels.Remove(p);
				this.pnlMain.Controls.Remove(p);
			}
		}

		/// <summary>
		/// An event method that saves the database and update the GUI accordingly.
		/// </summary>
		/// <param name="sender">The sender of the method.</param>
		/// <param name="e">The event arguments of the event.</param>
		public void saveDatabase(object sender, EventArgs e) {
			if(this.allUniqueTableNames()) {
				this.btnSave.BackColor = Color.LightYellow;

				for(int x = 0; x < this._panels.Count; x++) {
					this._panels[x].updateRows();
				} 
				
				Console.WriteLine(this._database.saveDatabase()); 
				
				for(int x = 0; x < this._panels.Count; x++) {
					this._panels[x].clearRows();
					this._panels[x].setAllAsLoaded();
					this._panels[x].Loaded = true;
				}

				for(int x = 0; x < this._panels.Count; x++) {
					if(this._panels[x].Drop) {
						this.pnlMain.Controls.Remove(this._panels[x]);
						this._panels.Remove(this._panels[x]);
					}
				}

				this.recalcControls(this, new ControlEventArgs(this));

				this.btnSave.BackColor = Color.LightGreen;
			} else {
				this.btnSave.BackColor = Color.LightSalmon;
				MessageBox.Show("All tables must have an unique name!");
			}			
		}

		/// <summary>
		/// Private method
		/// </summary>
		private bool allUniqueTableNames() {
			List<string> list = new List<string>(this._panels.Count);

			for(int x = 0; x < this._panels.Count; x++) {
				if(list.Contains(this._panels[x].TableName) || this._panels[x].TableName.Length == 0) {
					return false;
				} else {
					list.Add(this._panels[x].TableName);
				}
			}

			return true;
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void recalcControls(object sender, ControlEventArgs e) {
			for(int x = 0; x < this._panels.Count; x++) {
				this._panels[x].Location = new Point((this.pnlMain.Width / 2) - (this._panels[x].Width / 2), 5 + (x * (this._panels[x].Height + 5)));
			}
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void resize(object sender, EventArgs e) {
			this.btnAdd.Location = new Point(0, 0);
			this.btnSave.Location = new Point(this.Width - this.btnAdd.Width - 10, 0);
			this.pnlMain.Location = new Point(0, this.btnAdd.Height + 5);
			this.pnlMain.Size = new Size(this.Width, this.Height - this.btnAdd.Height - 5);
		}

		/// <summary>
		/// Private method
		/// </summary>
		private void DatabasePanel_Validated(object sender, EventArgs e) {
			this.recalcControls(this, new ControlEventArgs(this));
		}
	}
}
