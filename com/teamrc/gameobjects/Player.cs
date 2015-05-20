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
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class Player : Component, I_Collidable {
        public Rectangle playerImage = new Rectangle(64, 64, 32, 32);
        public Brush playerColor = Brushes.Blue;

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
        public Player() {
            InitializeComponent();

            //CollisionManager.add(this);
        }

        /**
         * Constructs the player object with a container
         */
        public Player(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }

        /**
         * Moves the player rectangle up if the key press is new.
         */
        public void moveUp() {
            if (!this.upFlag) {
                this.upFlag = true;

                this.playerImage.Y -= MOVE_SPEED;
            }
        }

        /**
         * Moves the player rectangle down if the key press is new.
         */
        public void moveDown() {
            if (!this.downFlag) {
                this.downFlag = true;

                this.playerImage.Y += MOVE_SPEED;
            }
        }

        /**
         * Moves the player rectangle left if the key press is new.
         */
        public void moveLeft() {
            if (!this.leftFlag) {
                this.leftFlag = true;

                this.playerImage.X -= MOVE_SPEED;
            }
        }

        /**
         * Moves the player rectangle right if the key press is new.
         */
        public void moveRight() {
            if (!this.rightFlag) {
                this.rightFlag = true;

                this.playerImage.X += MOVE_SPEED;
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

        public string getType() { return this.type; }

        public String[] getCollisionTypes() { return this.collisionTypes; }

        public Point getPosition() { return new Point(this.playerImage.X, this.playerImage.Y); }

        public void collidedWith(I_Collidable c) {
            //TODO: fill stub
        }

        public String toString() {
            return "p";
        }
    }
}
