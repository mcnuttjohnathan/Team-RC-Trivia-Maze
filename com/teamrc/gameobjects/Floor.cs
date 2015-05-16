using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Controls the floor objects in game.
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public partial class Floor : Component {
        public Rectangle floorImage;
        public Brush floorColor = Brushes.BurlyWood;

        /**
         * constructs the floor object. Floor position must
         * be a multiple of 32.
         * 
         * @param x - starting x coordinate
         * @param y - starting y coordinate
         */
        public Floor(int x, int y) {
            InitializeComponent();

            if (x % 32 != 0 || x % 32 != 0)
                throw new Exception();

            floorImage = new Rectangle(x, y, 32, 32);
        }

        /**
         * constructs the floor object. Floor position must
         * be a multiple of 32. Also assigns the object a container.
         * 
         * @param x - starting x coordinate
         * @param y - starting y coordinate
         * @param container - a container the object will be placed in
         */
        public Floor(int x, int y, IContainer container) {
            container.Add(this);

            InitializeComponent();

            if (x % 32 != 0 || x % 32 != 0)
                throw new Exception();

            floorImage = new Rectangle(x, y, 32, 32);
        }
    }
}
