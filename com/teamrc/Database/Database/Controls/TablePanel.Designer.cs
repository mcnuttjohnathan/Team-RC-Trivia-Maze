namespace DatabaseSystem.Controls {
	partial class TablePanel {
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
			this.txtName = new System.Windows.Forms.TextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.btnDrop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(0, 0);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(143, 22);
			this.txtName.TabIndex = 0;
			this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(822, 0);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(103, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add Question";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.addRow);
			// 
			// pnlMain
			// 
			this.pnlMain.AutoScroll = true;
			this.pnlMain.Location = new System.Drawing.Point(3, 28);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(919, 527);
			this.pnlMain.TabIndex = 2;
			this.pnlMain.SizeChanged += new System.EventHandler(this.TablePanel_Validated);
			this.pnlMain.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.recalcControls);
			// 
			// btnDrop
			// 
			this.btnDrop.Location = new System.Drawing.Point(149, 0);
			this.btnDrop.Name = "btnDrop";
			this.btnDrop.Size = new System.Drawing.Size(98, 22);
			this.btnDrop.TabIndex = 3;
			this.btnDrop.Text = "Drop Table";
			this.btnDrop.UseVisualStyleBackColor = true;
			this.btnDrop.Click += new System.EventHandler(this.dropTable);
			// 
			// TablePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Gray;
			this.Controls.Add(this.btnDrop);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.txtName);
			this.DoubleBuffered = true;
			this.Name = "TablePanel";
			this.Size = new System.Drawing.Size(925, 555);
			this.Resize += new System.EventHandler(this.TablePanel_Resize);
			this.Validated += new System.EventHandler(this.TablePanel_Validated);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Button btnDrop;
	}
}
