/*
 * Map stores an array of rooms and keeps track of the start and exit of the maze
 * 
 * @author Zoe Baker
 **/

using System;
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
        int size;
        Point start;
        Point finish;


        /**
         * This is the main constructor from map, initializes array
         * 
         * @param s - an int with the size of the array that the map should be (square)
         **/
        public Map(int s){
            InitializeComponent();

            this.map = new Room[s, s];
            this.size = s;
            this.start = new Point(0, 0);
            this.finish = new Point(s - 1, s - 1);
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

            for (int i = 0; i < this.size; i++){

                String nextRow ="";

                for (int j = 0; j < this.size; j++){

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

    }
}
