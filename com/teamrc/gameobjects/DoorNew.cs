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
 * Controls the New Door class.
 * New doors can be approached to answer questions.
 * Once they are activated they become Used Doors.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class DoorNew : A_Door {
        /**
         * Constructs the New Door class.
         * 
         * @param x - the doors x position.
         * @param y - the doors y position.
         */
        public DoorNew(int x, int y) : base(x, y) {
            InitializeComponent();

            this.init();
        }

        /**
         * Constructs the New Door class.
         * 
         * @param x - the doors x position.
         * @param y - the doors y position.
         * @param container - a parent for the component.
         */
        public DoorNew(int x, int y, IContainer container) : base(x, y, container) {
            container.Add(this);

            InitializeComponent();

            this.init();
        }

        /**
         * Initializes the New Door Component.
         */
        private void init() {
            this._doorColor = Brushes.DarkOrange;

            this.type = CollisionManager.NEW_DOOR;

            CollisionManager.add(this);
        }

        /**
         * Gives a Used Door state to replace the New Door.
         * Called when the door becomes activated by the player
         * walking into it.
         */
        public DoorUsed activateDoor(QuestionAnswer question) {
            //if (question == null)
                //throw new NullReferenceException();

            CollisionManager.remove(this);

            return new DoorUsed(this._doorImage.X, this._doorImage.Y, question);
        }

        /**
         * Returns a character representing the door.
         * 
         * @returns N - a symbol representing the door.
         */
        public override String toString() {
            return "N";
        }
    }
}
