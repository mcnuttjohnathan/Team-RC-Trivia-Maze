using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class Floor : Component {
        public Rectangle floorImage;
        public Brush floorColor = Brushes.BurlyWood;

        public Floor(int x, int y) {
            InitializeComponent();

            if (x % 32 != 0 || x % 32 != 0)
                throw new Exception();

            floorImage = new Rectangle(x, y, 32, 32);
        }

        public Floor(int x, int y, IContainer container) {
            container.Add(this);

            InitializeComponent();

            if (x % 32 != 0 || x % 32 != 0)
                throw new Exception();

            floorImage = new Rectangle(x, y, 32, 32);
        }
    }
}
