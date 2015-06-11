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
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class Floor : Component, I_Collidable {
        private Rectangle _floorImage;
        private Brush _floorColor = Brushes.BurlyWood;

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

            this.init(x, y);
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

            this.init(x, y);
        }

        /**
         * Initializes the Floor component.
         * 
         * @param x - starting x coordinate
         * @param y - starting y coordinate
         */
        private void init(int x, int y) {
            if (x % 32 != 0 || x % 32 != 0)
                throw new Exception();

            _floorImage = new Rectangle(x, y, 32, 32);

            CollisionManager.add(this);
        }

        /**
         * Returns the floors Rectangle image.
         * 
         * @returns image - the floor's Rectangle.
         */
        public Rectangle getImage() { return this._floorImage; }

        /**
         * Returns the floors Brush color.
         * 
         * @returns color - the floor's Brush color.
         */
        public Brush getColor() { return this._floorColor; }

        /**
         * Returns a String representing the floors type.
         * 
         * @returns type - the floors type.
         */
        public string getType() { return this.type; }

        /**
         * Returns a list of types the floor can collide with.
         * 
         * @returns collisionTypes - an array of Strings representing the types the floor can collide with.
         */
        public String[] getCollisionTypes() { return this.collisionTypes; }

        /**
         * Returns the floor's current position.
         * 
         * @returns position - A Point representing the floor's position.
         */
        public Point getPosition() { return new Point(this._floorImage.X, this._floorImage.Y); }

        /**
         * Returns a character representing the Component.
         * 
         * @returns O
         */
        public String toString() {
            return "O";
        }
    }
}
