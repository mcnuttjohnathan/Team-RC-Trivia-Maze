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
            Room testRoom = new Room();
            string testRoomStr = testRoom.toString();
            Console.WriteLine(testRoomStr);

            int[] a = { 1, 0, 0, 0 };
            Room testRoom2 = new Room(a);
            string testRoomStr2 = testRoom2.toString();
            Console.WriteLine(testRoomStr2);

            int[] b = { 1, 1, 0, 0 };
            Room testRoom3 = new Room(b);
            string testRoomStr3 = testRoom3.toString();
            Console.WriteLine(testRoomStr3);

            int[] c = { 1, 1, 1, 0 };
            Room testRoom4 = new Room(c);
            string testRoomStr4 = testRoom4.toString();
            Console.WriteLine(testRoomStr4);

            int[] d = { 1, 1, 1, 1 };
            Room testRoom5 = new Room(d);
            string testRoomStr5 = testRoom5.toString();
            Console.WriteLine(testRoomStr5);

            int[] f = { 1, 0, 0, 1 };
            Room testRoom6 = new Room(f);
            string testRoomStr6 = testRoom6.toString();
            Console.WriteLine(testRoomStr6);

            MazeGenerator mg = new MazeGenerator();
            Map m = mg.generate();
            String s = m.toString();
            Console.Write(s);

            Console.Read();
        }

        private void button3_Click(object sender, EventArgs e) {
            //Code for Database Demo
        }
    }
}
