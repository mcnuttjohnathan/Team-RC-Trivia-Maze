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
 * Controls the Unlocked Door Component.
 * Unlocked Doors can be walked on like floors.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class DoorUnlocked : A_Door {
        /**
         * Constructs the Unlocked Door component.
         * 
         * @param x - the doors x position.
         * @param y - the doors y position.
         */
        public DoorUnlocked(int x, int y) : base(x, y) {
            InitializeComponent();

            this.init();
        }

        /**
         * Constructs the Unlocked Door component.
         * 
         * @param x - the doors x position.
         * @param y - the doors y position.
         * @param container - a parent for the component.
         */
        public DoorUnlocked(int x, int y, IContainer container) : base(x, y, container) {
            container.Add(this);

            InitializeComponent();

            this.init();
        }

        /**
         * Initializes the Component
         */
        private void init() {
            this._doorColor = Brushes.BurlyWood;

            this.type = CollisionManager.FLOOR;

            CollisionManager.add(this);
        }

        /**
         * Returns a character representing the door.
         * 
         * @returns O - symbolizing a Unlocked Door.
         */
        public override String toString() {
            return "O";
        }
    }
}
