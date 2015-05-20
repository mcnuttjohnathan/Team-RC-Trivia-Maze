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

        private void makeRoom(){
            this.room = new I_Collidable[4,4];
            
            for (int x = 0; x < 3; x++){
                for (int y = 0; y < 3; y++){
                    this.room[x, y] = new Floor(location.X + (32 * x), location.Y + (32 * y));
                }
            }

            for (int x = 0; x < 4; x++){
                this.room[x, 3] = new Empty();
            }

            for (int y = 0; y < 3; y++){
                this.room[3, y] = new Empty();
            }
        }

        private void makeExits(){
            if (this.exits == 1){
                this.room[1, 3] = new Floor(location.X + 32, location.Y + 96);
            }

            if (this.exits == 2){
                this.room[3, 1] = new Floor(location.X + 96, location.Y + 32);
            }

            if (this.exits == 3){
                this.room[1, 3] = new Floor(location.X + 32, location.Y + 96);
                this.room[3, 1] = new Floor(location.X + 96, location.Y + 32);
            }
        }

        public I_Collidable[,] getRoom(){
            return this.room;
        }

        public Point getLocation(){
            return this.location;
        }

        public void setExits(int i){
            this.exits = i;
            makeExits();
        }

        public int getExits(){
            return this.exits;
        }

        public String toString(){
            String res = "";

            for (int i = 0; i < 4; i++){
                for (int j = 0; j < 4; j++){
                    res += (this.room[j, i].toString());
                }
                res += "\n";
            }

            return res;
        }
    }
}
