﻿/*
 * Map stores an array of rooms and keeps track of the start and exit of the maze
 * 
 * @author Zoe Baker
 **/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using TriviaMaze.com.teamrc.util;

namespace TriviaMaze.com.teamrc.gameobjects{
    public partial class Map : Component{

        private Room[,] map;
        int height;
        int width;
        Point start;
        Point finish;
        Random rnd;


        /**
         * This is the main constructor from map, initializes array
         * 
         * @param s - an int with the size of the array that the map should be (square)
         **/
        public Map(int h, int w){
            InitializeComponent();

            this.map = new Room[h, w];
            this.height = h;
            this.width = w;
            this.start = new Point(0, 0);
            this.finish = new Point(h - 1, w - 1);
            this.rnd = new Random();
            createMaze();
            addDoors();
        }

        public Map(IContainer container){
            container.Add(this);

            InitializeComponent();
        }

        /**
         * getRoom returns the room at the specified coords
         * 
         * @param i - the i index which the room lies on
         * @param j - the j index which the room lies on
         * @returns Room - the room component at that index
         **/
        public Room getRoom(int i, int j){
            return this.map[i, j];
        }

        /**
         * setRoom sets the room at this coords with the passed in room
         * 
         * @param i - the i index of the room to replace
         * @param j - the j index of the room to replace
         * @param r - the new room to set
         **/
        public void setRoom(int i, int j, Room r){
            this.map[i, j] = r;
        }

        /**
         * getRooms gives the array of room components
         * 
         * @returns rooms - the array of rooms.
         **/
        public Room[,] getRooms() { 
            return map; 
        }

        /**
         * Sets the coords of the start
         * 
         * @param i - the Point the start should be set to
         **/
        public void setStart(Point i){
            this.start = i;
        }

        /**
         * Returns the Point that Start exists on
         * 
         * @returns start - the Point that represents Start
         **/
        public Point getStart(){
            return this.start;
        }

        /**
         * Sets the coords of the finish
         * 
         * @param i - the Point the finish should be set to
         **/
        public void setFinish(Point i){
            this.finish = i;
        }

        /**
         * Returns the Point that finish exists on
         * 
         * @returns finish - the Point that represents finsih
         **/
        public Point getFinish(){
            return this.finish;
        }

        /**
         * Returns the String that represents the map and the existing pathways between the rooms, and the start and finish
         * 
         * @returns res - String representing the map
         **/
        public String toString(){
            String res = "";

            for (int i = 0; i < this.height; i++){

                String nextRow ="";

                for (int j = 0; j < this.width; j++){

                    int e = map[i, j].getExits();

                    if (start.X == i && start.Y == j){
                        res += 'S';
                    }else if (finish.X == i && finish.Y == j){
                        res += 'E';
                    }else{
                        res += 'O';
                    }

                    if (e > 1){
                        res += '-';
                    }else{
                        res += ' ';
                    }

                    if (e % 2 == 1){
                        nextRow += "| ";
                    }else{
                        nextRow += "  ";
                    }
                }

                res = res + "\n" + nextRow + "\n";
            }

            return res;
        }


        /**
         * createMaze fills the map array of empty rooms, and gives those rooms the 
         * coords of each for GUI purposes, then calls the recursive mazegeneration
         **/
        public void createMaze(){
            Point p = new Point(0, 0);

            for (int i = 0; i < height; i++){
                for (int j = 0; j < width; j++){
                    Room r = new Room(0, p);
                    this.map[i, j] = r;
                    p.Y = p.Y + 128;
                }
                p.X = p.X + 128;
                p.Y = 0;
            }

            generateMaze(start, start, false);
        }

        /**
         * generate maze goes through and using the perfect maze backtracking
         * recursion algoritm, it generates a maze with randomized connections 
         * between rooms
         * 
         * @param prev - the Point representation of the room you came from
         * @param cur - the Point representation of the current room you are in
         * @param makeDoor - the boolean value representing if you need to make an exit to connect cur to prev
         **/
        public void generateMaze(Point prev, Point cur, Boolean makeDoor){
            this.map[cur.X, cur.Y].setConnected(true);

            if (cur.X == this.finish.X && cur.Y == this.finish.Y){
                return;
            }
            
            if (makeDoor && prev.X == cur.X){
                int e = map[cur.X, cur.Y].getExits();
                e += 2;
                this.map[cur.X, cur.Y].setExits(e);
            }

            if (makeDoor && prev.Y == cur.Y){
                int e = map[cur.X, cur.Y].getExits();
                e += 1;
                this.map[cur.X, cur.Y].setExits(e);
            }

            
            ArrayList possible = new ArrayList();

            if (cur.X - 1 >= 0 && !map[cur.X - 1, cur.Y].isConnected()){
                Point n = new Point(cur.X - 1, cur.Y);
                possible.Add(n);
            }

            if (cur.Y + 1 < this.width && !map[cur.X, cur.Y + 1].isConnected()){
                Point n = new Point(cur.X, cur.Y + 1);
                possible.Add(n);
            }


            if (cur.X + 1 < this.height && !map[cur.X + 1, cur.Y].isConnected()){
                Point n = new Point(cur.X + 1, cur.Y);
                possible.Add(n);
            }

            if (cur.Y - 1 >= 0 && !map[cur.X, cur.Y - 1].isConnected()){
                Point n = new Point(cur.X, cur.Y - 1);
                possible.Add(n);
            }

            if (possible.Count == 0){
                return;
            }

            while (possible.Count > 0){

                int r = rnd.Next(0, possible.Count);
                Point next = (Point)possible[r];
                possible.RemoveAt(r);
                Boolean d = false;

                if (this.map[next.X, next.Y].isConnected() == false){
                    if ((cur.X == next.X && cur.Y - 1 == next.Y) || (cur.X - 1 == next.X && cur.Y == next.Y)){
                        d = true;
                    }

                    if (d == false && next.X == cur.X){
                        int e = map[cur.X, cur.Y].getExits();
                        e += 2;
                        this.map[cur.X, cur.Y].setExits(e);
                    }

                    if (d == false && next.Y == cur.Y){
                        int e = map[cur.X, cur.Y].getExits();
                        e += 1;
                        this.map[cur.X, cur.Y].setExits(e);
                    }

                    generateMaze(cur, next, d);
                    
                }
            }
        }


        /**
         * addDoors goes through and randomly opens new doors throughout the maze
         * to make it easier for the player to get to the exit
         **/
        public void addDoors(){
            double chance = .1;

            for (int i = 0; i < this.height; i++){
                for (int j = 0; j < this.width; j++){
                    double c = rnd.NextDouble();

                    if (c <= chance){
                        addOneDoor(i, j);
                    }
                }
            }
        }

        /**
         * addOneDoor is called when a door should be opened in a room
         * and determines where, if any, a door can be opened.
         * 
         * @param i - the i index that the room is on
         * @param j - the j index that the room is on
         **/
        public void addOneDoor(int i, int j){
            int exits = this.map[i, j].getExits();

            if (exits != 3){

                if (i != this.height - 1 && exits != 1){
                    exits += 1;
                }

                if (j != this.width - 1 && exits != 2){
                    exits += 2;
                }

            }

            this.map[i,j].setExits(exits);
        }

    }
}
