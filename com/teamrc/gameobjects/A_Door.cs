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
 * An Abstract Door component that all doors inherit from.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.gameobjects {
    public abstract partial class A_Door : Component, I_Collidable {
        public Rectangle doorImage;
        public Brush doorColor;

        public String type;

        public String[] collisionTypes = { CollisionManager.NONE};
        
        /**
         * Constructs the abstract door
         * @param x - doors x position
         * @param y - doors y position
         */
        public A_Door(int x, int y) {
            InitializeComponent();

            if (x % 32 != 0 || y % 32 != 0)
                throw new Exception();

            this.doorImage = new Rectangle(x, y, 32, 32);
        }

        /**
         * Constructs the abstract door
         * @param x - doors x position
         * @param y - doors y position
         * @param container - a parent container for the component
         */
        public A_Door(int x, int y, IContainer container) {
            container.Add(this);

            InitializeComponent();

            if (x % 32 != 0 || y % 32 != 0)
                throw new Exception();

            this.doorImage = new Rectangle(x, y, 32, 32);
        }

        /**
         * @returns image - the doors rectangle image
         */
        public Rectangle getImage() { return this.doorImage; }

        /**
         * @returns color
         */
        public Brush getColor() { return this.doorColor; }

        /**
         * @returns type - a string represtation of its type
         */
        public String getType() { return type; }

        /**
         * @returns collosion types - all the types it can collide with.
         */
        public String[] getCollisionTypes() { return collisionTypes; }

        /**
         * @return position - the doors x and y position.
         */
        public Point getPosition() { return new Point(this.doorImage.X, this.doorImage.Y); }

        /**
         * @returns character representation for mapping purposes
         */
        public abstract String toString();
    }
}
