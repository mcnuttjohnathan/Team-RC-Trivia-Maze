using DatabaseSystem;
using DatabaseSystem.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseDrive
{
    public partial class Tester : Form
    {
		public Tester()
        {
            InitializeComponent();
			DBManipulate dB = new DBManipulate();
			dB.Dock = DockStyle.Fill;
			dB.FolderPath = "./";
			this.Controls.Add(dB);
        }
    }
}