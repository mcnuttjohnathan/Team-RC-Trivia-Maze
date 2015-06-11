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
 * Finish component that the player must reach to beat the game
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class Finish : Component, I_Collidable {
        private Rectangle _finishImage;
        private Brush _finishColor = Brushes.Green;

        public String type = CollisionManager.FINISH;

        public String[] collisionTypes = { CollisionManager.NONE}; 
        
        /**
         * Constructs the finish component
         * @param x - the x position
         * @param y - the y position
         */
        public Finish(int x, int y) {
            InitializeComponent();

            this.init(x, y);
        }

        /**
         * Constructs the finish component
         * @param x - the x position
         * @param y - the y position
         * @param container - a parent container for the components
         */
        public Finish(int x, int y, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(x, y);
        }

        /**
         * @private
         * Initializes the component
         */
        private void init(int x, int y) {
            _finishImage = new Rectangle(x, y, 32, 32);

            CollisionManager.add(this);
        }

        /**
         * @returns type - a string representing the components type
         */
        public String getType() {
            return this.type;
        }

        /**
         * @returns collision types - types the component can collide with
         */
        public String[] getCollisionTypes() {
            return this.collisionTypes;
        }

        /**
         * @returns color
         */
        public Brush getColor() {
            return this._finishColor;
        }

        /**
         * @returns image - rectangle image of the finish
         */
        public Rectangle getImage() {
            return this._finishImage;
        }

        /**
         * @returns position - Point
         */
        public Point getPosition() {
            return new Point(this._finishImage.X, this._finishImage.Y);
        }

        /**
         * @returns a character to represent the finish
         */
        public String toString() {
            return "F";
        }
    }
}
