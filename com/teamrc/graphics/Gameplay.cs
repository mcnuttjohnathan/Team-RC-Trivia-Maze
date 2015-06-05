using DatabaseSystem;
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
        private Player player;
        private Map map;
        private MazeGenerator mazeGenerator = new MazeGenerator();
        private Timer t = new Timer();

        private Boolean inQuestion = false;
        private QuestionSource questionSource = new QuestionSource();

        /**
         * Constructs the gameplay window.
         */
        public Gameplay() {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.map = mazeGenerator.generate();

            Point start = map.getStart();
            player = new Player(start.X, start.Y);

            t.Interval = 17;
            t.Tick += new EventHandler(update);
            t.Start();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            e.Graphics.Clear(Color.Black);

            Room[,] rooms = map.getRooms();

            for (int i = 0; i < rooms.GetLength(0); i++) {
                for (int j = 0; j < rooms.GetLength(1); j++) {
                    I_Collidable[,] tiles = rooms[i, j].getRoom();

                    for (int k = 0; k < tiles.GetLength(0); k++) {
                        for (int l = 0; l < tiles.GetLength(1); l++) {
                            e.Graphics.FillRectangle(tiles[k, l].getColor(), tiles[k, l].getImage());
                        }
                    }
                }
            }

            e.Graphics.FillRectangle(player.playerColor, player.playerImage);

            if (this.inQuestion) {
                if (player.getPosition().Y > 224) {
                    //Question Top
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 16, 416, 128));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 16, 416, 128));
                    e.Graphics.DrawString("This is a question?", new Font(FontFamily.GenericSerif, 16), Brushes.Black, new PointF(48, 32));

                    //Answer A Top
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 148, 200, 32));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 148, 200, 32));
                    e.Graphics.DrawString("1) Answer A", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(48, 152));

                    //Answer B Top
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 148, 200, 32));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 148, 200, 32));
                    e.Graphics.DrawString("2) Answer B", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(260, 152));

                    //Answer C Top
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 184, 200, 32));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 184, 200, 32));
                    e.Graphics.DrawString("3) Answer C", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(48, 188));

                    //Answer D Top
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 184, 200, 32));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 184, 200, 32));
                    e.Graphics.DrawString("4) Answer D", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(260, 188));
                    /*
                    //Answer True Top
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 148, 200, 64));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 148, 200, 64));
                    e.Graphics.DrawString("1) True", new Font(FontFamily.GenericSerif, 24), Brushes.Black, new PointF(48, 160));

                    //Answer False Top
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 148, 200, 64));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 148, 200, 64));
                    e.Graphics.DrawString("2) False", new Font(FontFamily.GenericSerif, 24), Brushes.Black, new PointF(260, 160));
            
                    //Answer Input Top
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 148, 416, 64));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 148, 416, 64));
                    e.Graphics.DrawString("Answer: ", new Font(FontFamily.GenericSerif, 24), Brushes.Black, new PointF(48, 160));
                    */
                }
                else {
                    //Question Bottom
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 272, 416, 120));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 272, 416, 120));
                    e.Graphics.DrawString("This is a question?", new Font(FontFamily.GenericSerif, 16), Brushes.Black, new PointF(48, 288));

                    //Answer A Bottom
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 396, 200, 32));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 396, 200, 32));
                    e.Graphics.DrawString("1) Answer A", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(48, 400));

                    //Answer B Bottom
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 396, 200, 32));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 396, 200, 32));
                    e.Graphics.DrawString("2) Answer B", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(260, 400));

                    //Answer C Bottom
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 432, 200, 32));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 432, 200, 32));
                    e.Graphics.DrawString("3) Answer C", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(48, 436));

                    //Answer D Bottom
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 432, 200, 32));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 432, 200, 32));
                    e.Graphics.DrawString("4) Answer D", new Font(FontFamily.GenericSerif, 12), Brushes.Black, new PointF(260, 436));
                    /*
                    //Answer True Bottom
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 396, 200, 64));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 396, 200, 64));
                    e.Graphics.DrawString("1) True", new Font(FontFamily.GenericSerif, 24), Brushes.Black, new PointF(48, 408));

                    //Answer False Bottom
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(248, 396, 200, 64));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(248, 396, 200, 64));
                    e.Graphics.DrawString("2) False", new Font(FontFamily.GenericSerif, 24), Brushes.Black, new PointF(260, 408));
            
                    //Answer Input Bottom
                    e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(32, 396, 416, 64));
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(32, 396, 416, 64));
                    e.Graphics.DrawString("Answer: ", new Font(FontFamily.GenericSerif, 24), Brushes.Black, new PointF(48, 408));
                    */
                }
            }//end if statement
        }

        /**
         * draws the window graphics approximately 60 times per second
         */
        
        private void update(object sender, EventArgs e) {
            this.Invalidate();
        }
        

        /**
         * Activates whenever the user presses a key.
         * Checks to see if the key is WASD and if the
         * player can move in that direction.
         */
        private void Gameplay_KeyDown_1(object sender, KeyEventArgs e) {
            if (e.KeyCode.Equals(Keys.Up)) {
                I_Collidable collider = 
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X, this.player.playerImage.Y - 32), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR)) {
                    this.player.moveUp();
                    this.inQuestion = false;
                }
                else
                    this.otherCollisions(collider);
            }

            else if (e.KeyCode.Equals(Keys.Down)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X, this.player.playerImage.Y + 32), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR)) {
                    this.player.moveDown();
                    this.inQuestion = false;
                }
                else
                    this.otherCollisions(collider);
            }

            else if (e.KeyCode.Equals(Keys.Left)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X - 32, this.player.playerImage.Y), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR)) {
                    this.player.moveLeft();
                    this.inQuestion = false;
                }
                else
                    this.otherCollisions(collider);
            }

            else if (e.KeyCode.Equals(Keys.Right)) {
                I_Collidable collider =
                    CollisionManager.testPlayerCollision(new Point(this.player.playerImage.X + 32, this.player.playerImage.Y), this.player);

                if (collider.getType().Equals(CollisionManager.FLOOR)) {
                    this.player.moveRight();
                    this.inQuestion = false;
                }
                else
                    this.otherCollisions(collider);
            }

            else if (e.KeyCode.Equals(Keys.D0)) {
                //saving goes here
            }
        }

        /**
         * Checks if a key is released, if WASD is released
         * the player flag in the same direction is reset.
         */
        private void Gameplay_KeyUp_1(object sender, KeyEventArgs e) {
            if (e.KeyCode.Equals(Keys.Up))
                this.player.resetUpFlag();

            if (e.KeyCode.Equals(Keys.Down))
                this.player.resetDownFlag();

            if (e.KeyCode.Equals(Keys.Left))
                this.player.resetLeftFlag();

            if (e.KeyCode.Equals(Keys.Right))
                this.player.resetRightFlag();
        }

        private void otherCollisions(I_Collidable collider) {
            if (collider.getType().Equals(CollisionManager.NEW_DOOR))
                this.openNewDoor(collider);
            else if (collider.getType().Equals(CollisionManager.USED_DOOR)) {

            }
            else if (collider.getType().Equals(CollisionManager.FINISH)) {

            }
        }

        private void openNewDoor(I_Collidable door) {
            this.inQuestion = true;
            QuestionAnswer qa = this.questionSource.randomQuestion();

            Point p = door.getPosition();

            if ((p.X - 96) % 128 == 0) {
                int x = (p.X - 96) / 128;
                int y = (p.Y - 32) / 128;

                Room r = this.map.getRoom(y, x);

                r.setDoorRight(((DoorNew)door).activateDoor(qa).unlockDoor());
            }
            else if ((p.Y - 96) % 128 == 0) {
                int x = (p.X - 32) / 128;
                int y = (p.Y - 96) / 128;

                Room r = this.map.getRoom(y, x);

                r.setDoorDown(((DoorNew)door).activateDoor(qa).unlockDoor());
            }
            else
                throw new Exception();
        }

        /**
         * stops update from being called once the window is closed and resets the
         * CollisionManager singleton.
         */
        private void Gameplay_FormClosed(object sender, FormClosedEventArgs e) {
            t.Stop();

            CollisionManager.reset();
        }
    }
}
