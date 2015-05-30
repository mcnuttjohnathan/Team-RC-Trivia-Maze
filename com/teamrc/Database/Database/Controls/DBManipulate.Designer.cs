namespace DatabaseSystem.Controls {
	partial class DBManipulate {
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
			this.btnLoad = new System.Windows.Forms.Button();
			this.pnlFolder = new System.Windows.Forms.Panel();
			this.cmbFolder = new System.Windows.Forms.ComboBox();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.pnlFolder.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnLoad
			// 
			this.btnLoad.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnLoad.Location = new System.Drawing.Point(652, 0);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(114, 25);
			this.btnLoad.TabIndex = 1;
			this.btnLoad.Text = "Load Folder";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.switchFolder);
			// 
			// pnlFolder
			// 
			this.pnlFolder.Controls.Add(this.cmbFolder);
			this.pnlFolder.Controls.Add(this.btnLoad);
			this.pnlFolder.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFolder.Location = new System.Drawing.Point(0, 0);
			this.pnlFolder.Name = "pnlFolder";
			this.pnlFolder.Size = new System.Drawing.Size(766, 25);
			this.pnlFolder.TabIndex = 2;
			// 
			// cmbFolder
			// 
			this.cmbFolder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmbFolder.FormattingEnabled = true;
			this.cmbFolder.Location = new System.Drawing.Point(0, 0);
			this.cmbFolder.Name = "cmbFolder";
			this.cmbFolder.Size = new System.Drawing.Size(652, 24);
			this.cmbFolder.TabIndex = 2;
			this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.switchPanel);
			this.cmbFolder.TextChanged += new System.EventHandler(this.cmbFolder_TextChanged);
			// 
			// pnlMain
			// 
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 25);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(766, 544);
			this.pnlMain.TabIndex = 3;
			// 
			// DBManipulate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.pnlFolder);
			this.DoubleBuffered = true;
			this.Name = "DBManipulate";
			this.Size = new System.Drawing.Size(766, 569);
			this.pnlFolder.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Panel pnlFolder;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.ComboBox cmbFolder;
	}
}
