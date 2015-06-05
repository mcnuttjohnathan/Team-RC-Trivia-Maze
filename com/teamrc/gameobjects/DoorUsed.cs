using DatabaseSystem;
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
 * Controls the Used Door component.
 * Used doors can become either locked or unlocked doors
 * depending on if the player answers right.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class DoorUsed : A_Door {
        QuestionAnswer questionAnswer;
        
        /**
         * Constructs the Used Door Component
         * 
         * @param x - the door's x position.
         * @param y - the door's y position.
         */
        public DoorUsed(int x, int y, QuestionAnswer question) : base(x, y) {
            InitializeComponent();

            this.init(question);
        }

        /**
         * Constructs the Used Door Component
         * 
         * @param x - the door's x position.
         * @param y - the door's y position.
         * @param container - a parent container for the door.
         */
        public DoorUsed(int x, int y, QuestionAnswer question, IContainer container) : base(x, y, container) {
            container.Add(this);

            InitializeComponent();

            this.init(question);
        }

        /**
         * Initializes the Used Door Component.
         */
        public void init(QuestionAnswer questionAnswer) {
            if (questionAnswer == null)
                throw new NullReferenceException();
            
            this.questionAnswer = questionAnswer;

            this.doorColor = Brushes.DarkOrange;

            this.type = CollisionManager.USED_DOOR;

            CollisionManager.add(this);
        }

        /**
         * Returns a Unlocked Door state if the player
         * answered correctly.
         * 
         * @returns unlockedDoor - a new door state.
         */
        public DoorUnlocked unlockDoor() {
            CollisionManager.remove(this);

            return new DoorUnlocked(this.doorImage.X, this.doorImage.Y);
        }

        /**
         * Returns a Locked Door state if the player
         * answered wrong.
         * 
         * @returns lockedDoor - a new door state.
         */
        public DoorLocked lockDoor() {
            CollisionManager.remove(this);

            return new DoorLocked(this.doorImage.X, this.doorImage.Y);
        }

        public QuestionAnswer getQuestionAnswer() { return this.questionAnswer; }

        /**
         * Returns a character representation of the door.
         * 
         * @returns U - representation of the door.
         */
        public override String toString() {
            return "U";
        }
    }
}
