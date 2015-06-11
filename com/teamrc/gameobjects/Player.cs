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
 * Controls the player object in the game
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class Player : Component, I_Collidable {
        private Rectangle _playerImage;
        private Brush _playerColor = Brushes.Blue;

        public String type = CollisionManager.PLAYER;

        public String[] collisionTypes = { CollisionManager.NEW_DOOR, CollisionManager.USED_DOOR, CollisionManager.FINISH, CollisionManager.FLOOR };

        private Boolean upFlag = false;
        private Boolean downFlag = false;
        private Boolean leftFlag = false;
        private Boolean rightFlag = false;

        private static int MOVE_SPEED = 32;

        /**
         * constructs the player object
         */
        public Player(int x, int y) {
            InitializeComponent();

            this.init(x, y);
        }

        /**
         * Constructs the player object with a container
         */
        public Player(int x, int y, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(x, y);
        }

        /**
         * initializes the player component
         */
        private void init(int x, int y) {
            this._playerImage = new Rectangle(x, y, 32, 32);

            //CollisionManager.add(this);
        }

        /**
         * Moves the player rectangle up if the key press is new.
         */
        public void moveUp() {
            if (!this.upFlag) {
                this.upFlag = true;

                this._playerImage.Y -= MOVE_SPEED;
            }
        }

        /**
         * Moves the player rectangle down if the key press is new.
         */
        public void moveDown() {
            if (!this.downFlag) {
                this.downFlag = true;

                this._playerImage.Y += MOVE_SPEED;
            }
        }

        /**
         * Moves the player rectangle left if the key press is new.
         */
        public void moveLeft() {
            if (!this.leftFlag) {
                this.leftFlag = true;

                this._playerImage.X -= MOVE_SPEED;
            }
        }

        /**
         * Moves the player rectangle right if the key press is new.
         */
        public void moveRight() {
            if (!this.rightFlag) {
                this.rightFlag = true;

                this._playerImage.X += MOVE_SPEED;
            }
        }

        /**
         * Resets the up flag when the W key is released.
         */
        public void resetUpFlag() {
            this.upFlag = false;
        }

        /**
         * Resets the down flag when the S key is released.
         */
        public void resetDownFlag() {
            this.downFlag = false;
        }

        /**
         * Resets the left flag when the A key is released.
         */
        public void resetLeftFlag() {
            this.leftFlag = false;
        }

        /**
         * Resets the right flag when the D key is released.
         */
        public void resetRightFlag() {
            this.rightFlag = false;
        }

        /**
         * Returns the players Rectangle object
         * 
         * @returns image - player Rectangle
         */
        public Rectangle getImage() { return this._playerImage; }


        /**
         * Returns the player Brush color.
         * 
         * @returns color - the Brush color.
         */
        public Brush getColor() { return this._playerColor; }

        /**
         * Returns the players type.
         * 
         * @returns type - a String reperesenting the type.
         */
        public string getType() { return this.type; }

        /**
         * Returns a list of types the player can collide with.
         * 
         * @returns collisionTypes - an array of Strings representing the types the player can collide with.
         */
        public String[] getCollisionTypes() { return this.collisionTypes; }


        /**
         * Returns the players current position.
         * 
         * @returns position - A Point representing the players position.
         */
        public Point getPosition() { return new Point(this._playerImage.X, this._playerImage.Y); }

        /**
         * Returns a character representing the component.
         * 
         * @returns p
         */
        public String toString() {
            return "p";
        }
    }
}
