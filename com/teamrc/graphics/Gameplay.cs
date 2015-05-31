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
        Player player;
        Graphics graphics;
        Map map;
        MazeGenerator mazeGenerator = new MazeGenerator();
        Timer t = new Timer();

        /**
         * Constructs the gameplay window.
         */
        public Gameplay() {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.graphics = this.CreateGraphics();
            this.map = mazeGenerator.generate();

            Point start = map.getStart();
            player = new Player(start.X, start.Y);

            t.Interval = 17;
            t.Tick += new EventHandler(update);
            t.Start();
        }

        /**
         * draws the windows graphics approximately 60 times per second
         */
        private void update(object sender, EventArgs e) {
            this.graphics.Clear(Color.Black);

            Room[,] rooms = map.getRooms();

            for (int i = 0; i < rooms.GetLength(0); i++) {
                for (int j = 0; j < rooms.GetLength(1); j++) {
                    I_Collidable[,] tiles = rooms[i, j].getRoom();

                    for (int k = 0; k < tiles.GetLength(0); k++) {
                        for (int l = 0; l < tiles.GetLength(1); l++) {
                            this.graphics.FillRectangle(tiles[k,l].getColor(), tiles[k,l].getImage());
                        }
                    }
                }
            }

            this.graphics.FillRectangle(player.playerColor, player.playerImage);

            //Question Top
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 16, 416, 128));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 16, 416, 128));
            this.graphics.DrawString("This is a question?", new Font(FontFamily.GenericSerif, 16), Brushes.Black, new PointF(48, 32));

            //Answer A Top
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 148, 200, 32));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 148, 200, 32));
            this.graphics.DrawString("1) Answer A", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(48, 152));

            //Answer B Top
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 148, 200, 32));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 148, 200, 32));
            this.graphics.DrawString("2) Answer B", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(260, 152));

            //Answer C Top
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 184, 200, 32));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 184, 200, 32));
            this.graphics.DrawString("3) Answer C", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(48, 188));

            //Answer D Top
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 184, 200, 32));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 184, 200, 32));
            this.graphics.DrawString("4) Answer D", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(260, 188));

            //Answer True Top

            //Answer False Top

            //Answer Input Top

            //Question Bottom
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 272, 416, 120));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 272, 416, 120));
            this.graphics.DrawString("This is a question?", new Font(FontFamily.GenericSerif, 16), Brushes.Black, new PointF(48, 288));

            //Answer A Bottom
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 396, 200, 32));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 396, 200, 32));
            this.graphics.DrawString("1) Answer A", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(48, 400));

            //Answer B Bottom
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 396, 200, 32));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 396, 200, 32));
            this.graphics.DrawString("2) Answer B", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(260, 400));

            //Answer C Bottom
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 432, 200, 32));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 432, 200, 32));
            this.graphics.DrawString("3) Answer C", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(48, 436));

            //Answer D Bottom
            this.graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 432, 200, 32));
            this.graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 432, 200, 32));
            this.graphics.DrawString("4) Answer D", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(260, 436));

            //Answer True Bottom

            //Answer False Bottom

            //Answer Input Bottom
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

                if (collider.getType().Equals(CollisionManager.FLOOR) || collider.getType().Equals(CollisionManager.NEW_DOOR))
                    player.moveUp();
            }

            else if (e.KeyCode.Equals(Keys.S)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X, this.player.playerImage.Y + 32), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR) || collider.getType().Equals(CollisionManager.NEW_DOOR))
                    player.moveDown();
            }

            else if (e.KeyCode.Equals(Keys.A)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X - 32, this.player.playerImage.Y), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR) || collider.getType().Equals(CollisionManager.NEW_DOOR))
                    player.moveLeft();
            }

            else if (e.KeyCode.Equals(Keys.D)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X + 32, this.player.playerImage.Y), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR) || collider.getType().Equals(CollisionManager.NEW_DOOR))
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
