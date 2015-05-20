using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaMaze.com.teamrc.util;

namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class DoorUsed : A_Door {
        public DoorUsed(int x, int y) : base(x, y) {
            InitializeComponent();

            this.init();
        }

        public DoorUsed(int x, int y, IContainer container) : base(x, y, container) {
            container.Add(this);

            InitializeComponent();

            this.init();
        }

        public void init() {
            this.doorColor = Brushes.DarkOrange;

            this.type = CollisionManager.USED_DOOR;
        }

        public DoorUnlocked unlockDoor() {
            return new DoorUnlocked(this.doorImage.X, this.doorImage.Y);
        }

        public DoorLocked lockDoor() {
            return new DoorLocked(this.doorImage.X, this.doorImage.Y);
        }

        public override String toString() {
            return "U";
        }
    }
}
