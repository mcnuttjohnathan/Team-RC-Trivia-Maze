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
    public abstract partial class A_Door : Component, I_Collidable {
        public Rectangle doorImage;
        public Brush doorColor;

        public String type;

        public String[] collisionTypes = { CollisionManager.NONE};
        
        public A_Door(int x, int y) {
            InitializeComponent();

            if (x % 32 != 0 || y % 32 != 0)
                throw new Exception();

            this.doorImage = new Rectangle(x, y, 32, 32);
        }

        public A_Door(int x, int y, IContainer container) {
            container.Add(this);

            InitializeComponent();

            if (x % 32 != 0 || y % 32 != 0)
                throw new Exception();

            this.doorImage = new Rectangle(x, y, 32, 32);
        }

        public Rectangle getImage() { return this.doorImage; }

        public Brush getColor() { return this.doorColor; }

        public String getType() { return type; }

        public String[] getCollisionTypes() { return collisionTypes; }

        public Point getPosition() { return new Point(this.doorImage.X, this.doorImage.Y); }

        public abstract String toString();
    }
}
