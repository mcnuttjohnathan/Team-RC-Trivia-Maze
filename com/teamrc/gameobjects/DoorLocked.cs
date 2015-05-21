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
    public partial class DoorLocked : A_Door {
        public DoorLocked(int x, int y) : base(x, y) {
            InitializeComponent();

            this.init();
        }

        public DoorLocked(int x, int y, IContainer container) : base(x, y, container) {
            container.Add(this);

            InitializeComponent();

            this.init();
        }

        private void init() {
            this.doorColor = Brushes.Red;

            this.type = CollisionManager.LOCKED_DOOR;

            //CollisionManager.add(this);
        }

        public override String toString() {
            return "L";
        }
    }
}
