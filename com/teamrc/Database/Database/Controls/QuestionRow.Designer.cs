namespace DatabaseSystem.Controls {
	partial class QuestionRow {
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
			this.txtQuestion = new System.Windows.Forms.TextBox();
			this.txtAns0 = new System.Windows.Forms.TextBox();
			this.txtAns1 = new System.Windows.Forms.TextBox();
			this.txtAns2 = new System.Windows.Forms.TextBox();
			this.txtAns3 = new System.Windows.Forms.TextBox();
			this.cmbType = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// txtQuestion
			// 
			this.txtQuestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.txtQuestion.Location = new System.Drawing.Point(3, 3);
			this.txtQuestion.MaxLength = 140;
			this.txtQuestion.Name = "txtQuestion";
			this.txtQuestion.Size = new System.Drawing.Size(200, 22);
			this.txtQuestion.TabIndex = 0;
			this.txtQuestion.Text = "Question";
			this.txtQuestion.Leave += new System.EventHandler(this.toQuestion);
			// 
			// txtAns0
			// 
			this.txtAns0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.txtAns0.Location = new System.Drawing.Point(209, 3);
			this.txtAns0.MaxLength = 24;
			this.txtAns0.Name = "txtAns0";
			this.txtAns0.Size = new System.Drawing.Size(100, 22);
			this.txtAns0.TabIndex = 1;
			this.txtAns0.Text = "Correct Answer";
			this.txtAns0.Leave += new System.EventHandler(this.toQuestion);
			// 
			// txtAns1
			// 
			this.txtAns1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.txtAns1.Location = new System.Drawing.Point(315, 3);
			this.txtAns1.MaxLength = 24;
			this.txtAns1.Name = "txtAns1";
			this.txtAns1.Size = new System.Drawing.Size(100, 22);
			this.txtAns1.TabIndex = 2;
			this.txtAns1.Text = "Wrong Answer";
			this.txtAns1.Leave += new System.EventHandler(this.toQuestion);
			// 
			// txtAns2
			// 
			this.txtAns2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.txtAns2.Location = new System.Drawing.Point(421, 3);
			this.txtAns2.MaxLength = 24;
			this.txtAns2.Name = "txtAns2";
			this.txtAns2.Size = new System.Drawing.Size(100, 22);
			this.txtAns2.TabIndex = 3;
			this.txtAns2.Text = "Wrong Answer";
			this.txtAns2.Leave += new System.EventHandler(this.toQuestion);
			// 
			// txtAns3
			// 
			this.txtAns3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.txtAns3.Location = new System.Drawing.Point(527, 3);
			this.txtAns3.MaxLength = 24;
			this.txtAns3.Name = "txtAns3";
			this.txtAns3.Size = new System.Drawing.Size(100, 22);
			this.txtAns3.TabIndex = 4;
			this.txtAns3.Text = "Wrong Answer";
			this.txtAns3.Leave += new System.EventHandler(this.toQuestion);
			// 
			// cmbType
			// 
			this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbType.FormattingEnabled = true;
			this.cmbType.Items.AddRange(new object[] {
            "Multiple Choice",
            "True or False",
            "Input Question",
            "Delete"});
			this.cmbType.Location = new System.Drawing.Point(633, 3);
			this.cmbType.Name = "cmbType";
			this.cmbType.Size = new System.Drawing.Size(121, 24);
			this.cmbType.TabIndex = 5;
			this.cmbType.SelectionChangeCommitted += new System.EventHandler(this.toQuestion);
			// 
			// QuestionRow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Silver;
			this.Controls.Add(this.cmbType);
			this.Controls.Add(this.txtAns3);
			this.Controls.Add(this.txtAns2);
			this.Controls.Add(this.txtAns1);
			this.Controls.Add(this.txtAns0);
			this.Controls.Add(this.txtQuestion);
			this.DoubleBuffered = true;
			this.MaximumSize = new System.Drawing.Size(0, 30);
			this.MinimumSize = new System.Drawing.Size(758, 30);
			this.Name = "QuestionRow";
			this.Size = new System.Drawing.Size(758, 30);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtQuestion;
		private System.Windows.Forms.TextBox txtAns0;
		private System.Windows.Forms.TextBox txtAns1;
		private System.Windows.Forms.TextBox txtAns2;
		private System.Windows.Forms.TextBox txtAns3;
		private System.Windows.Forms.ComboBox cmbType;
	}
}
