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
 * Controls the locked door class.
 * Locked doors cannot be passed through.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class DoorLocked : A_Door {
        /**
         * Constructs the Locked Door Class.
         * 
         * @param x - the doors x position.
         * @param y - the doors y position.
         */
        public DoorLocked(int x, int y) : base(x, y) {
            InitializeComponent();

            this.init();
        }

        /**
         * Constructs the Locked Door Class.
         * 
         * @param x - the doors x position.
         * @param y - the doors y position.
         * @param container - a parent for the component.
         */
        public DoorLocked(int x, int y, IContainer container) : base(x, y, container) {
            container.Add(this);

            InitializeComponent();

            this.init();
        }

        /**
         * Initializes the component
         */
        private void init() {
            this._doorColor = Brushes.Red;

            this.type = CollisionManager.LOCKED_DOOR;

            //CollisionManager.add(this);
        }

        /**
         * Returns a character representing the door.
         * 
         * @returns L - symbolizing a Locked Door.
         */
        public override String toString() {
            return "L";
        }
    }
}
