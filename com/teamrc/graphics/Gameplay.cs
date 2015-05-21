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
using TriviaMaze.com.teamrc.util;

/**
 * Opens a window where the user can control a player.
 * used to demonstrate graphics and basic movement.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.graphics {
    public partial class Gameplay : Form {
        Player player = new Player(32, 32);
        Graphics graphics;
        Map map;
        MazeGenerator mazeGenerator = new MazeGenerator();
        Timer t = new Timer();

        /**
         * Constructs the gameplay window.
         */
        public Gameplay() {
            InitializeComponent();

            this.graphics = this.CreateGraphics();
            this.map = mazeGenerator.generate();

            t.Interval = 17;
            t.Tick += new EventHandler(update);
            t.Start();
        }

        /**
         * draws the windows graphics approximately 60 times per second
         */
        private void update(object sender, EventArgs e) {
            graphics.Clear(Color.Black);

            Room[,] rooms = map.getRooms();

            for (int i = 0; i < rooms.GetLength(0); i++) {
                for (int j = 0; j < rooms.GetLength(1); j++) {
                    I_Collidable[,] tiles = rooms[i, j].getRoom();

                    for (int k = 0; k < tiles.GetLength(0); k++) {
                        for (int l = 0; l < tiles.GetLength(1); l++) {
                            graphics.FillRectangle(tiles[k,l].getColor(), tiles[k,l].getImage());
                        }
                    }
                }
            }

            graphics.FillRectangle(player.playerColor, player.playerImage);
        }

        /**
         * Activates whenever the user presses a key.
         * Checks to see if the key is WASD and if the
         * player can move in that direction.
         */
        private void Gameplay_KeyDown_1(object sender, KeyEventArgs e) {
            if (e.KeyCode.Equals(Keys.W)) {
                I_Collidable collider = 
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X, this.player.playerImage.Y - 32), this.player);

                if(collider.getType().Equals(CollisionManager.FLOOR))
                    player.moveUp();
            }

            else if (e.KeyCode.Equals(Keys.S)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X, this.player.playerImage.Y + 32), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR))
                    player.moveDown();
            }

            else if (e.KeyCode.Equals(Keys.A)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X - 32, this.player.playerImage.Y), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR))
                    player.moveLeft();
            }

            else if (e.KeyCode.Equals(Keys.D)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X + 32, this.player.playerImage.Y), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR))
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
