/**
 * Room is a component that stores floor and empty components to make up the room, as well as the overall location of the room in relation to the map.
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
    public partial class Room : Component{

        private I_Collidable[,] room;
        private int exits;
        private Point location;
        private Boolean connected;

        /**
         * This is the main constructor for room, taking in the exits and the locaiton
         * 
         * @param e - the int signifying which exits exist
         * @param l - a Point displaying where the tiles absolute position is
         * */
        public Room(int e, Point l){
            InitializeComponent();
            this.exits = e;

            if (l.X % 128 != 0 || l.Y % 128 != 0){
                throw new Exception();
            }

            this.location = l;

            makeRoom();
            makeExits();
        }

        public Room(IContainer container){
            container.Add(this);

            InitializeComponent();
        }

        /**
         * This initializes the array of the room using the default size hardcoded, and fills it will floors and a surrounding of emptyness
         **/
        private void makeRoom(){
            this.room = new I_Collidable[4,4];
            
            for (int i = 0; i < 3; i++){
                for (int j = 0; j < 3; j++){
                    this.room[i, j] = new Floor(location.X + (32 * j), location.Y + (32 * i));
                }
            }

            for (int i = 0; i < 4; i++){
                this.room[3, i] = new Empty();
            }

            for (int j = 0; j < 3; j++){
                this.room[j, 3] = new Empty();
            }
        }

        /**
         * This takes the int exits and calculates and places paths for the room exits
         **/
        private void makeExits(){
            if (this.exits == 1) {
                this.room[1, 3] = new DoorNew(location.X + 96, location.Y + 32);
            }
            
            if (this.exits == 2){
                this.room[3, 1] = new DoorNew(location.X + 32, location.Y + 96);
            }

            if (this.exits == 3){
                this.room[3,1] = new DoorNew(location.X + 32, location.Y + 96);
                this.room[1,3] = new DoorNew(location.X + 96, location.Y + 32);
            }
        }

        public void makeFinish()
        {
            this.room[1, 1] = new Finish(location.X + 32, location.Y + 32);
        }

        /**
         * This returns the array of the room
         * 
         * @return room - I_Collidable 2D array of room
         **/
        public I_Collidable[,] getRoom(){
            return this.room;
        }

        /**
         * This returns the location of the room
         * 
         * @returns location - the Point location
         **/
        public Point getLocation(){
            return this.location;
        }

        /**
         * This sets a new exitnumber and calls the method to remake the exits of the room
         * @param i - new int signifying which exits exist
         **/
        public void setExits(int i){
            this.exits = i;
            makeExits();
        }

        /**
         * This returns the int signifying the exits existing
         * 
         * @returns exits - an int ranging from 1-3 for the exits the room holds
         **/
        public int getExits(){
            return this.exits;
        }


        /**
         * This is called to see if the room is connected to any other rooms in the map
         * 
         * @returns connected - the boolean saying if the room is connected or not
         **/
        public Boolean isConnected()
        {
            return this.connected;
        }

        /**
         * This is called to set to room to a connected or unconnected state
         * 
         * @param v - the boolean saying if the room is connected to another room or not
         **/
        public void setConnected(Boolean v)
        {
            this.connected = v;
        }

        /**
         * This returns a String the can be printed out to show room layout
         * 
         * @returns res - a String representation of room
         **/
        public String toString(){
            String res = "";

            for (int i = 0; i < 4; i++){
                for (int j = 0; j < 4; j++){
                    res += (this.room[i, j].toString());
                }
                res += "\n";
            }

            return res;
        }
    }
}
