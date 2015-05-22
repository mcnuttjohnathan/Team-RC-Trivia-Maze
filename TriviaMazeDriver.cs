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
            //Code for Maze Generation goes here
            Console.WriteLine("Generating a 4X4 Maze with a 10% chance of opening more doors.");

            MazeGenerator mg = new MazeGenerator();
            Map m = mg.generate();
            String s = m.toString();
            Console.Write(s);

            Console.Read();
        }

        private void button3_Click(object sender, EventArgs e) {
            Database db = new Database("TestDatabase");
            Table t = null;

            if (db.Count < 1){
                t = db.AddNewTable("TestTable");
            } else {
                t = db[0];
            }

            if (t.Count < 5){
                t.AddNewQuestion();
            } else {
                t.RemoveQuestion(t[t.Count - 1]);
            }

            Console.WriteLine(db.SaveDatabase());
             
        }
    }
}
