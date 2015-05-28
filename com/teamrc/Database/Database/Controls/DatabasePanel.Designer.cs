namespace DatabaseSystem.Controls {
	partial class DatabasePanel {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.btnAdd = new System.Windows.Forms.Button();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(3, 3);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(104, 23);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "Add Table";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.addTable);
			// 
			// pnlMain
			// 
			this.pnlMain.AutoScroll = true;
			this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.pnlMain.Location = new System.Drawing.Point(0, 32);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(720, 503);
			this.pnlMain.TabIndex = 1;
			this.pnlMain.SizeChanged += new System.EventHandler(this.DatabasePanel_Validated);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(595, 3);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(122, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save Database";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.saveDatabase);
			// 
			// DatabasePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.btnAdd);
			this.DoubleBuffered = true;
			this.Name = "DatabasePanel";
			this.Size = new System.Drawing.Size(720, 535);
			this.Resize += new System.EventHandler(this.resize);
			this.Validated += new System.EventHandler(this.DatabasePanel_Validated);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Button btnSave;
	}
}
