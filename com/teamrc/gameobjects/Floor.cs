using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TriviaMaze.com.teamrc.util;

/**
 * Controls the floor objects in game.
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class Floor : Component, I_Collidable {
        public Rectangle floorImage;
        public Brush floorColor = Brushes.BurlyWood;

        public String type = CollisionManager.FLOOR;

        public String[] collisionTypes = {CollisionManager.NONE};

        /**
         * constructs the floor object. Floor position must
         * be a multiple of 32.
         * 
         * @param x - starting x coordinate
         * @param y - starting y coordinate
         */
        public Floor(int x, int y) {
            InitializeComponent();
        }

        /**
         * constructs the floor object. Floor position must
         * be a multiple of 32. Also assigns the object a container.
         * 
         * @param x - starting x coordinate
         * @param y - starting y coordinate
         * @param container - a container the object will be placed in
         */
        public Floor(int x, int y, IContainer container) {
            container.Add(this);

            InitializeComponent();
        }

        private void init() {
            if (x % 32 != 0 || x % 32 != 0)
                throw new Exception();

            floorImage = new Rectangle(x, y, 32, 32);

            CollisionManager.add(this);
        }

        public string getType() { return this.type; }

        public String[] getCollisionTypes() { return this.collisionTypes; }

        public Point getPosition() { return new Point(this.floorImage.X, this.floorImage.Y); }

        public void collidedWith(I_Collidable c) {
            //TODO unstub
        }

        public String toString() {
            return "o";
        }
    }
}
