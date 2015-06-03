using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Finisar.SQLite;

using TriviaMaze.com.teamrc.gameobjects;
using TriviaMaze.com.teamrc.graphics;
using DatabaseSystem;
using DatabaseSystem.Controls;
using System.IO;


/**
 * Driver class for Team Rapidash Cachers Trivia Maze project for CSCD 350 in Spring 2015
 * 
 * Iteration 1: one section of code will be added by each team member. Each one is given
 * a button to show their effects
 * 
 * Code:
 * Johnathan McNutt - Graphics and Movement
 * Zoe Baker - Maze Generation
 * Ted Bickham - Database Management
 */
namespace TriviaMaze {
    public partial class TriviaMazeDriver : Form {
        public TriviaMazeDriver() {
            InitializeComponent();
        }

        /**
         * When clicked opens a window that demos the basic graphics
         * and movement. WASD too move. The player can only move on
         * flooring.
         */
        private void button1_Click(object sender, EventArgs e) {
            Gameplay demo = new Gameplay();
            demo.Show();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            //Code for printing directions goes here
            
            Console.WriteLine("Controls: \n" +
                              "Move: UP : Up Arrow Key \n" +
                              "      DOWN : Down Arrow Key \n" +
                              "      LEFT : Left Arrow Key \n" +
                              "      RIGHT : Right Arrow Key \n \n" +
                              "Select Answer: " +
                              "" +
                              "");
            
        }

        private void button3_Click(object sender, EventArgs e) {
			Form f = new Form();
			DBManipulate dbM = new DBManipulate();
			dbM.Dock = DockStyle.Fill;

			if(!File.Exists("./testDB.db")) {
				Database d = new Database("testDB.db");
			}

			dbM.FolderPath = "./";

			f.Controls.Add(dbM);
			f.WindowState = FormWindowState.Maximized;

			f.ShowDialog();
        }
    }
}
