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
    public partial class DoorUnlocked : A_Door {
        public DoorUnlocked(int x, int y) : base(x, y) {
            InitializeComponent();

            this.init();
        }

        public DoorUnlocked(int x, int y, IContainer container) : base(x, y, container) {
            container.Add(this);

            InitializeComponent();

            this.init();
        }

        private void init() {
            this.doorColor = Brushes.BurlyWood;

            this.type = CollisionManager.FLOOR;
        }

        public override String toString() {
            return "O";
        }
    }
}
