using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TriviaMaze.com.teamrc.gameobjects;

/**
 * Opens a window where the user can control a player.
 * used to demonstrate graphics and basic movement.
 */
namespace TriviaMaze.com.teamrc.graphics {
    public partial class Gameplay : Form {
        Player player = new Player();
        Graphics g;
        Timer t = new Timer();

        Floor[,] flooring = new Floor[,]{
            {new Floor(32, 32), new Floor (32, 64), new Floor(32, 96)},
            {new Floor (64, 32), new Floor(64, 64), new Floor (64, 96)},
            {new Floor (96, 32), new Floor (96, 64), new Floor (96, 96)}
        };

        /**
         * Constructs the gameplay window.
         */
        public Gameplay() {
            InitializeComponent();

            g = this.CreateGraphics();

            t.Interval = 17;
            t.Tick += new EventHandler(update);
            t.Start();
        }

        /**
         * draws the windows graphics approximately 60 times per second
         */
        private void update(object sender, EventArgs e) {
            g.Clear(Color.Black);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    g.FillRectangle(flooring[i,j].floorColor, flooring[i, j].floorImage);

            g.FillRectangle(player.playerColor, player.playerImage);
        }

        /**
         * Activates whenever the user presses a key.
         * Checks to see if the key is WASD and if the
         * player can move in that direction.
         */
        private void Gameplay_KeyDown_1(object sender, KeyEventArgs e) {
            if (e.KeyCode.Equals(Keys.W)) {
                int move = this.player.playerImage.Y - 32;

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (this.flooring[i, j].floorImage.Y == move)
                            player.moveUp();
            }

            else if (e.KeyCode.Equals(Keys.S)) {
                int move = this.player.playerImage.Y + 32;

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (this.flooring[i, j].floorImage.Y == move)
                            player.moveDown();
            }

            else if (e.KeyCode.Equals(Keys.A)) {
                int move = this.player.playerImage.X - 32;

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (this.flooring[i, j].floorImage.X == move)
                            player.moveLeft();
            }

            else if (e.KeyCode.Equals(Keys.D)) {
                int move = this.player.playerImage.X + 32;

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (this.flooring[i, j].floorImage.X == move)
                            player.moveRight();
            }
        }

        /**
         * Checks if a key is released, if WASD is released
         * the player flag in the same direction is reset.
         */
        private void Gameplay_KeyUp_1(object sender, KeyEventArgs e) {
            if (e.KeyCode.Equals(Keys.W))
                this.player.resetUpFlag();

            if (e.KeyCode.Equals(Keys.S))
                this.player.resetDownFlag();

            if (e.KeyCode.Equals(Keys.A))
                this.player.resetLeftFlag();

            if (e.KeyCode.Equals(Keys.D))
                this.player.resetRightFlag();
        }

        /**
         * stops update from being called once the window is closed.
         */
        private void Gameplay_FormClosed(object sender, FormClosedEventArgs e) {
            t.Stop();
        }
    }
}
