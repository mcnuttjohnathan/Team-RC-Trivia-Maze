﻿namespace TriviaMaze.com.teamrc.graphics {
    partial class Gameplay {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // Gameplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 480);
            this.Name = "Gameplay";
            this.Text = "Game Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Gameplay_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Gameplay_KeyDown_1);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Gameplay_KeyUp_1);
            this.ResumeLayout(false);

        }

        #endregion
    }
}