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
	public partial class DatabasePanel : UserControl {
		private DBManipulate _owner;
		private Database _database;
		private List<TablePanel> _panels;

		public DatabasePanel(DBManipulate m, Database d) {
			InitializeComponent();
			this._owner = m;
			this._database = d;
			this._panels = new List<TablePanel>(d.Count);

			this.importTables();
		}

		private void addTable(object sender, EventArgs e) {
			TablePanel tP = new TablePanel(this, this._database);

			this._panels.Add(tP);
			this.pnlMain.Controls.Add(tP);
			this.recalcControls(this, new ControlEventArgs(this));
		}

		private void importTables() {
			for(int x = 0; x < this._database.Count; x++) {
				TablePanel tP = new TablePanel(this, this._database[x]);

				this._panels.Add(tP);
				this.pnlMain.Controls.Add(tP);
			}

			this.recalcControls(this, new ControlEventArgs(this));
		}

		public void dropTable(TablePanel p) {
			if(this._panels.Contains(p)) {
				this._panels.Remove(p);
				this.pnlMain.Controls.Remove(p);
			}
		}

		public void saveDatabase(object sender, EventArgs e) {
			if(this.allUniqueTableNames()) {
				this.btnSave.BackColor = Color.LightYellow;

				for(int x = 0; x < this._panels.Count; x++) {
					this._panels[x].updateRows();
				} 
				
				Console.WriteLine(this._database.SaveDatabase()); 
				
				for(int x = 0; x < this._panels.Count; x++) {
					this._panels[x].clearRows();
					this._panels[x].setAllAsLoaded();
					this._panels[x].Loaded = true;
				}

				this.btnSave.BackColor = Color.LightGreen;
			} else {
				this.btnSave.BackColor = Color.LightSalmon;
				MessageBox.Show("All tables must have an unique name!");
			}			
		}

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

		private void recalcControls(object sender, ControlEventArgs e) {
			for(int x = 0; x < this._panels.Count; x++) {
				this._panels[x].Location = new Point((this.pnlMain.Width / 2) - (this._panels[x].Width / 2), 5 + (x * (this._panels[x].Height + 5)));
			}
		}

		private void resize(object sender, EventArgs e) {
			this.btnAdd.Location = new Point(0, 0);
			this.btnSave.Location = new Point(this.Width - this.btnAdd.Width - 10, 0);
			this.pnlMain.Location = new Point(0, this.btnAdd.Height + 5);
			this.pnlMain.Size = new Size(this.Width, this.Height - this.btnAdd.Height - 5);
		}

		private void DatabasePanel_Validated(object sender, EventArgs e) {
			this.recalcControls(this, new ControlEventArgs(this));
		}
	}
}
