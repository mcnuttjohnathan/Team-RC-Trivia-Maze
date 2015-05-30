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
    public partial class Finish : Component, I_Collidable {
        public Rectangle finishImage;
        public Brush finishColor = Brushes.Green;

        public String type = CollisionManager.FINISH;

        public String[] collisionTypes = { CollisionManager.NONE}; 
        
        public Finish(int x, int y) {
            InitializeComponent();

            this.init(x, y);
        }

        public Finish(int x, int y, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(x, y);
        }

        private void init(int x, int y) {
            finishImage = new Rectangle(x, y, 32, 32);

            CollisionManager.add(this);
        }

        public String getType() {
            return this.type;
        }

        public String[] getCollisionTypes() {
            return this.collisionTypes;
        }

        public Brush getColor() {
            return this.finishColor;
        }

        public Rectangle getImage() {
            return this.finishImage;
        }

        public Point getPosition() {
            return new Point(this.finishImage.X, this.finishImage.Y);
        }

        public String toString() {
            return "F";
        }
    }
}
