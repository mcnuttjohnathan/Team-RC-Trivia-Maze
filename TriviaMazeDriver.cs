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
using TriviaMaze.com.teamrc.savefiles;
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
using TriviaMaze.com.teamrc.SaveUtil;

namespace TriviaMaze {
    public partial class TriviaMazeDriver : Form {
        public TriviaMazeDriver() {
            InitializeComponent();
        }

        /**
         * Starts a new game for the player.
         */
        private void button1_Click(object sender, EventArgs e) {
            Gameplay game = new Gameplay();
            game.Show();
            //this.Close();
        }

        /**
         * Loads a previously saved game for the player.
         */
        private void button4_Click(object sender, EventArgs e) {
            //loading game code goes here.
        }

        /**
         * Opens a window with instructions for how to play.
         */
        private void button2_Click(object sender, EventArgs e) {

            Form f = new Form();
            TextBox t = new TextBox();

            f.Height = 400;
            f.Width = 480;

            t.AppendText("Purpose: \r\n" +
                              "You must lead the Player through the Knowledge Labyrinth. \r\n" +
                              "Along the way, you must solve questions to open doors. \r\n" +
                              "If you get a question wrong, that door will be permanently locked. \r\n" +
                              "The game is over when the Player either reaches the finish, \r\n" +
                              "or all possible routes to the exit are blocked with impassable doors. \r\n \r\n");

            t.AppendText("Controls: \r\n" +
                              " Move: \r\n" +
                              "      UP : Up Arrow Key \r\n" +
                              "      DOWN : Down Arrow Key \r\n" +
                              "      LEFT : Left Arrow Key \r\n" +
                              "      RIGHT : Right Arrow Key \r\n \r\n" +
                              " Answer Questions: \r\n" +
                              "      Multiple Choice : Use Number Keys 1-4 \r\n" +
                              "      True or False : Use 1 for True and 2 for False \r\n" +
                              "      Short Answer : Use Keyboard to type answer, then press Enter \r\n");

            t.Enabled = false;
            t.Multiline = true;
            t.Dock = DockStyle.Fill;
            t.Font = new Font(FontFamily.GenericSerif, 12);
            t.ForeColor = Color.Black;

            f.Text = "Instructions";

            f.Controls.Add(t);

            f.ShowDialog();

        }

        /**
         * starts the database manipulator when clicked.
         */
        private void button3_Click(object sender, EventArgs e) {
			Form f = new Form();
			DBManipulate dbM = new DBManipulate();
			dbM.Dock = DockStyle.Fill;

			dbM.FolderPath = "./";

			f.Controls.Add(dbM);
			f.WindowState = FormWindowState.Maximized;

            try {
                f.ShowDialog();
            }
            catch (NullReferenceException eN) {
                Console.WriteLine(eN.InnerException.Message);
            }
        }
    }
}
