using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DatabaseSystem.Controls {
	public partial class DBManipulate : UserControl {
		private DatabasePanel _currentPanel;
		private String _folderPath;
		private List<DatabasePanel> _folderPanels;

		public DBManipulate() {
			InitializeComponent();
			this._folderPath = @"./";
			this._folderPanels = new List<DatabasePanel>();

			this.switchFolder(this, new EventArgs());
			this.btnLoad.BackColor = SystemColors.Control;
		}

		public String FolderPath {
			get { return this._folderPath; }
			set { 
				this._folderPath = value;
				this.cmbFolder.Text = value;
			}
		}

		private void switchPanel(object sender, EventArgs e) {
			if(this.cmbFolder.SelectedIndex < this.cmbFolder.Items.Count && this.cmbFolder.SelectedIndex > -1) {
				this.pnlMain.Controls.Remove(this._currentPanel);
				this._currentPanel = this._folderPanels[this.cmbFolder.SelectedIndex];
				this.pnlMain.Controls.Add(this._currentPanel);
			}
		}

		public void cPromptSave(object sender, FormClosingEventArgs e) {
			if(!this.promptSave()) {
				e.Cancel = true;
			}
		}

		private bool promptSave() {
			if(this._folderPanels.Count != 0) {
				DialogResult r = MessageBox.Show("Do you want to save all loaded Databases?", "Save databases?", MessageBoxButtons.YesNoCancel);

				if(r == DialogResult.Yes) {
					for(int x = 0; x < this._folderPanels.Count; x++) {
						this._folderPanels[x].saveDatabase(this, new EventArgs());
					}
				} else if(r == DialogResult.Cancel) {
					return false;
				}
			}

			return true;
		}

		private void switchFolder(object sender, EventArgs e) {

			btnLoad.BackColor = Color.LightYellow;
			String newDB = null;

			if(this.cmbFolder.Text.Length > 0) {
				if(!Directory.Exists(this.cmbFolder.Text) && !File.Exists(this.cmbFolder.Text)) {

					String path = this.cmbFolder.Text;
					String folder = Path.GetFullPath(Path.GetDirectoryName(path));

					if((path.EndsWith(".db") || !path.EndsWith(@"/")) && Directory.Exists(folder)) {
						newDB = Path.GetFileName(this.cmbFolder.Text);
						this.cmbFolder.Text = this.cmbFolder.Text.Replace(newDB, "");
					}
				}
			}

			if(Directory.Exists(this.cmbFolder.Text)) {
				if(this.promptSave()) {
					this._folderPath = this.cmbFolder.Text;
					this.resetPanel();
					this._folderPanels.Clear();
					this.cmbFolder.Items.Clear();

					String[] fileNames = Directory.GetFiles(this._folderPath);

					for(int x = 0; x < fileNames.Length; x++) {
						if(fileNames[x].EndsWith(@".db")) {
							Database d = new Database(Path.GetFullPath(fileNames[x]));

							DatabasePanel dP = new DatabasePanel(this, d);
							dP.Dock = DockStyle.Fill;

							this._folderPanels.Add(dP);
							this.cmbFolder.Items.Add(d.Name);
						}
					}

					if(newDB != null) {
						Database d = new Database(this._folderPath + @"/" + newDB);

						DatabasePanel dP = new DatabasePanel(this, d);
						dP.Dock = DockStyle.Fill;

						this._folderPanels.Add(dP);
						this.cmbFolder.Items.Add(Path.GetFullPath(d.Name));
					}

					this.btnLoad.BackColor = Color.LightGreen;
					this.cmbFolder.Text = "Select database from drop-down list.";
				}
			} else {
				this.btnLoad.BackColor = Color.LightSalmon;
			}
		}

		private void resetPanel() {
			this.pnlMain.Controls.Clear();
			this._currentPanel = null;
		}

		private void cmbFolder_TextChanged(object sender, EventArgs e) {
			String path = this.cmbFolder.Text;

			if(Directory.Exists(Path.GetDirectoryName(path)) && Path.GetFileName(path).Length > 0 && Path.GetExtension(path).Length != 0) {
				this.btnLoad.Text = "New Database";
			} else {
				this.btnLoad.Text = "Load Folder";
			}
		}
	}
}
