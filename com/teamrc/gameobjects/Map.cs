/*
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
using System.Runtime.Serialization;
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
            this.rnd = new Random(); 
            this.start = new Point(0, 0);  
            this.finish = new Point(this.height - 1, this.width - 1);         
            createMaze();
            addDoors();
        }

        public Map(Room[,] r, int h, int w)
        {
            this.map = r;
            this.height = h;
            this.width = w;
            this.rnd = new Random();
            this.start = new Point(0, 0);
            this.finish = new Point(this.height - 1, this.width - 1);
        }

        public Map(SerializationInfo info, StreamingContext ctxt)
        { 
            this.map = (Room[,])info.GetValue("Map", typeof(Room[,]));
            this.height = 4;
            this.width = 4;
            this.rnd = new Random();
            this.start = new Point(0,0);
            this.finish = new Point();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Map", this.map);
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
         * setRoom sets the room array to a new array
         *@param r - the new room array to be set to
         **/

        public void setRooms(Room[,] r)
        {
            this.map = r;
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

                    if (e % 2 == 1){
                        res += '-';
                    }else{
                        res += ' ';
                    }

                    if (e > 1){
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
                    if (i == finish.X && j == finish.Y)
                    {
                        r.makeFinish();
                    }
                    this.map[i, j] = r;
                    p.X = p.X + 128;
                }
                p.Y = p.Y + 128;
                p.X = 0;
            }

           generateMaze(this.start, this.start, false);
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
                e += 1;
                this.map[cur.X, cur.Y].setExits(e);
            }

            if (makeDoor && prev.Y == cur.Y){
                int e = map[cur.X, cur.Y].getExits();
                e += 2;
                this.map[cur.X, cur.Y].setExits(e);
            }

            
            ArrayList possible = new ArrayList();

            if (cur.X - 1 >= 0 && map[cur.X - 1, cur.Y].isConnected() == false){
                Point n = new Point(cur.X - 1, cur.Y);
                possible.Add(n);
            }

            if (cur.Y + 1 < this.width && map[cur.X, cur.Y + 1].isConnected() == false){
                Point n = new Point(cur.X, cur.Y + 1);
                possible.Add(n);
            }


            if (cur.X + 1 < this.height && map[cur.X + 1, cur.Y].isConnected() == false){
                Point n = new Point(cur.X + 1, cur.Y);
                possible.Add(n);
            }

            if (cur.Y - 1 >= 0 && map[cur.X, cur.Y - 1].isConnected() == false){
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
                        e += 1;
                        this.map[cur.X, cur.Y].setExits(e);
                    }

                    if (d == false && next.Y == cur.Y){
                        int e = map[cur.X, cur.Y].getExits();
                        e += 2;
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

                if (j + 1 < this.width && exits != 1){
                    exits += 1;
                }

                if (i + 1 < this.height && exits != 2){
                    exits += 2;
                }

            }

            this.map[i,j].setExits(exits);
        }

        /**
         *Calls the isSolvable function at the start of the maze and returns the value it gives
         *
         * @returns boolean - true means the maze is still solvable, false means it isn't
         **/
        public Boolean isSolvable(){
            int[,] visited = new int[this.height, this.width];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    visited[i, j] = 0;
                }
            }
                return solve(this.start, this.start, visited);
        }

        /**
         * Walks through the maze recursively to see if the end can still be reached
         * 
         * @param prev - the previous point 
         * @param cur - the current point 
         * @param v - the visited array        
         * @returns boolean - true if the end has been found along a pathway.
         **/
        public Boolean solve(Point prev, Point cur, int[,] v){
            v[cur.X, cur.Y] = 1;

            if (cur.X == this.finish.X & cur.Y == this.finish.Y){
                return true;
            }

            Point next = new Point();
            if (cur.Y + 1 != this.width && (map[cur.X, cur.Y].unlockedExits() == 1 || map[cur.X, cur.Y].unlockedExits() == 3) && v[cur.X, cur.Y+1] == 0){
                next.X = cur.X;
                next.Y = cur.Y + 1;
                Boolean b = solve(prev, next, v);
                if (b == true){
                    return b;
                }
            }

            if (cur.X + 1 != this.height && map[cur.X, cur.Y].unlockedExits() > 1 && v[cur.X + 1, cur.Y] == 0){
              
                next.X = cur.X + 1;
                next.Y = cur.Y;
                Boolean b = solve(prev, next, v);
                if (b == true){
                    return b;
                }
            }

            if (cur.Y - 1 >= 0 && (map[cur.X, cur.Y - 1].unlockedExits() == 1 || map[cur.X, cur.Y - 1].unlockedExits() == 3) && v[cur.X, cur.Y - 1] == 0){
                next.X = cur.X;
                next.Y = cur.Y - 1;

                Boolean b = solve(prev, next, v);

                if (b){
                    return b;
                }
            }

            if (cur.X - 1 >= 0 && map[cur.X - 1, cur.Y].unlockedExits() > 1 && v[cur.X - 1, cur.Y] == 0){                
                next.X = cur.X - 1;
                next.Y = cur.Y;
                Boolean b = solve(prev, next, v);

                if (b)
                {
                    return b;
                }
            }

            return false;
        }

    }
}
