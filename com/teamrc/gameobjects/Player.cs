using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class Player : Component {
        public Rectangle playerImage = new Rectangle(64, 64, 32, 32);
        public Brush playerColor = Brushes.Blue;

        private Boolean upFlag = false;
        private Boolean downFlag = false;
        private Boolean leftFlag = false;
        private Boolean rightFlag = false;

        private static int MOVE_SPEED = 32;

        public Player() {
            InitializeComponent();
        }

        public Player(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }

        public void moveUp() {
            if (!this.upFlag) {
                this.upFlag = true;

                this.playerImage.Y -= MOVE_SPEED;
            }
        }

        public void moveDown() {
            if (!this.downFlag) {
                this.downFlag = true;

                this.playerImage.Y += MOVE_SPEED;
            }
        }

        public void moveLeft() {
            if (!this.leftFlag) {
                this.leftFlag = true;

                this.playerImage.X -= MOVE_SPEED;
            }
        }

        public void moveRight() {
            if (!this.rightFlag) {
                this.rightFlag = true;

                this.playerImage.X += MOVE_SPEED;
            }
        }

        public void resetUpFlag() {
            this.upFlag = false;
        }

        public void resetDownFlag() {
            this.downFlag = false;
        }

        public void resetLeftFlag() {
            this.leftFlag = false;
        }

        public void resetRightFlag() {
            this.rightFlag = false;
        }
    }
}
