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
    public partial class DoorNew : A_Door {
        public DoorNew(int x, int y) : base(x, y) {
            InitializeComponent();

            this.init();
        }

        public DoorNew(int x, int y, IContainer container) : base(x, y, container) {
            container.Add(this);

            InitializeComponent();

            this.init();
        }

        private void init() {
            this.doorColor = Brushes.DarkOrange;

            this.type = CollisionManager.NEW_DOOR;

            CollisionManager.add(this);
        }

        public DoorUsed activateDoor() {
            return new DoorUsed(this.doorImage.X, this.doorImage.Y);
        }

        public override String toString() {
            return "N";
        }
    }
}
