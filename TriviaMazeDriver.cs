﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Finisar.SQLite;
using TriviaMaze.com.teamrc.savefiles;
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

            Console.WriteLine("Purpose: \n" +
                              "You must lead the Player through the Knowledge Maze. \n" +
                              "Along the way, you must solve questions to open doors. \n" +
                              "If you get a question wrong, that door will be permanently locked. \n" +
                              "The game is over when the Player either reaches the finish, \n" +
                              "or all possible routes to the exit are blocked with impassable doors. \n \n");
    
            Console.WriteLine("Controls: \n" +
                              " Move: \n" + 
                              "      UP : Up Arrow Key \n" +
                              "      DOWN : Down Arrow Key \n" +
                              "      LEFT : Left Arrow Key \n" +
                              "      RIGHT : Right Arrow Key \n \n" +
                              " Answer Questions: \n" +
                              "      Multiple Choice : Use Number Keys (or click?) \n" +
                              "      True or False : Use T or F Keys (or click?) \n" +
                              "      Short Answer : Use Keyboard to type answer, then press Enter \n");
            
            //Code for testing Serialize

            MazeGenerator mg = new MazeGenerator();
            Map m = mg.generate();

            Player p = new Player(0, 0);

            QuestionSource qs = new QuestionSource();

            SaveData sd = new SaveData(p, m, qs);
            Serializer serializer = new Serializer();
            serializer.SerializeObject("outputFile.txt", sd);
            String ms =  m.toString();
            Console.WriteLine(ms);

            SaveData saved = serializer.DeSerializeObject("outputFile.txt");
            Player player = saved.getPlayer();
            QuestionSource questionSource = saved.getQuestionSource();
            Map map = saved.getMap();
            try
            {
                String s = map.toString();
                Console.Write(s);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
			Form f = new Form();
			DBManipulate dbM = new DBManipulate();
			dbM.Dock = DockStyle.Fill;

			dbM.FolderPath = "./";

			f.Controls.Add(dbM);
			f.WindowState = FormWindowState.Maximized;

			f.ShowDialog();
        }
    }
}
