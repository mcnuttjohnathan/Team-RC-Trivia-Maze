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
using TriviaMaze.com.teamrc.TriviaUI;
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
        private QuestionSource questionSource = new QuestionSource("./");
        private TriviaController triviaController = new TriviaController();

        private DoorUsed currDoor = null;

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
                QuestionBox qb = this.triviaController.getQuestion();
                A_AnswerBox[] ab = this.triviaController.getAnswers();

                //Question
                e.Graphics.FillRectangle(qb.getBoxColor(), qb.getImage());
                e.Graphics.DrawRectangle(new Pen(qb.getBorderColor()), qb.getImage());
                e.Graphics.DrawString(qb.getQuestion(), qb.getFont(), qb.getTextColor(), qb.getTextPosition());

                //Answers
                for(int i = 0; i < ab.GetLength(0); i++){
                    e.Graphics.FillRectangle(ab[i].getBoxColor(), ab[i].getImage());
                    e.Graphics.DrawRectangle(new Pen(ab[i].getBorderColor()), ab[i].getImage());
                    e.Graphics.DrawString(ab[i].getText(), ab[i].getFont(), ab[i].getTextColor(), ab[i].getTextPosition());
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
                    this.currDoor = null;
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
                    this.currDoor = null;
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
                    this.currDoor = null;
                }
                else
                    this.otherCollisions(collider);
            }
            else if (this.inQuestion) {
                int i = this.triviaController.getAnswers().GetLength(0);

                if (i == 1) {
                    //
                }
                if (i >= 2) {
                    if (e.KeyCode.Equals(Keys.D1)) {
                        if (this.triviaController.getAnswers()[0].submitAnswer())
                            this.openUsedDoor();
                        else
                            this.closeUsedDoor();
                    }
                    else if (e.KeyCode.Equals(Keys.D2)) {
                        if (this.triviaController.getAnswers()[1].submitAnswer())
                            this.openUsedDoor();
                        else
                            this.closeUsedDoor();
                    }
                }
                if (i == 4) {
                    if (e.KeyCode.Equals(Keys.D3)) {
                        if (this.triviaController.getAnswers()[2].submitAnswer())
                            this.openUsedDoor();
                        else
                            this.closeUsedDoor();
                    }
                    else if (e.KeyCode.Equals(Keys.D4)) {
                        if (this.triviaController.getAnswers()[3].submitAnswer())
                            this.openUsedDoor();
                        else
                            this.closeUsedDoor();
                    }
                }
                
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
            else if (collider.getType().Equals(CollisionManager.USED_DOOR))
                this.viewUsedDoor(collider);
            else if (collider.getType().Equals(CollisionManager.FINISH)) {

            }
        }

        private void openNewDoor(I_Collidable door) {
            this.inQuestion = true;
            QuestionAnswer qa = this.questionSource.randomQuestion();
            this.triviaController.loadQuestionAnswer(qa, this.player);

            Point p = door.getPosition();

            DoorUsed du = ((DoorNew)door).activateDoor(qa);

            if ((p.X - 96) % 128 == 0) {
                int x = (p.X - 96) / 128;
                int y = (p.Y - 32) / 128;

                Room r = this.map.getRoom(y, x);

                r.setDoorRight(du);
            }
            else if ((p.Y - 96) % 128 == 0) {
                int x = (p.X - 32) / 128;
                int y = (p.Y - 96) / 128;

                Room r = this.map.getRoom(y, x);

                r.setDoorDown(du);
            }
            else
                throw new Exception();

            this.currDoor = du;
        }

        private void viewUsedDoor(I_Collidable door) {
            this.inQuestion = true;
            QuestionAnswer qa = ((DoorUsed)door).getQuestionAnswer();
            this.triviaController.loadQuestionAnswer(qa, this.player);

            this.currDoor = (DoorUsed)door;
        }

        private void openUsedDoor() {
            Point p = this.currDoor.getPosition();

            if ((p.X - 96) % 128 == 0) {
                int x = (p.X - 96) / 128;
                int y = (p.Y - 32) / 128;

                Room r = this.map.getRoom(y, x);

                r.setDoorRight(((DoorUsed)this.currDoor).unlockDoor());
            }
            else if ((p.Y - 96) % 128 == 0) {
                int x = (p.X - 32) / 128;
                int y = (p.Y - 96) / 128;

                Room r = this.map.getRoom(y, x);

                r.setDoorDown(((DoorUsed)this.currDoor).unlockDoor());
            }
            else
                throw new Exception();

            this.inQuestion = false;
            this.currDoor = null;
        }

        private void closeUsedDoor() {
            Point p = this.currDoor.getPosition();

            if ((p.X - 96) % 128 == 0) {
                int x = (p.X - 96) / 128;
                int y = (p.Y - 32) / 128;

                Room r = this.map.getRoom(y, x);

                r.setDoorRight(((DoorUsed)this.currDoor).lockDoor());
            }
            else if ((p.Y - 96) % 128 == 0) {
                int x = (p.X - 32) / 128;
                int y = (p.Y - 96) / 128;

                Room r = this.map.getRoom(y, x);

                r.setDoorDown(((DoorUsed)this.currDoor).lockDoor());
            }
            else
                throw new Exception();

            this.inQuestion = false;
            this.currDoor = null;

            if (!this.map.isSolvable())
                Console.WriteLine("Game Over");
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
