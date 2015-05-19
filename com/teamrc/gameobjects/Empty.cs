using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaMaze.com.teamrc.util;

namespace TriviaMaze.com.teamrc.gameobjects
{
    public partial class Empty : Component, I_Collidable
    {

        private String type = CollisionManager.NONE;
        private String[] collisionTypes = {CollisionManager.NONE};

        public Empty()
        {
            InitializeComponent();
        }

        public Empty(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public String toString()
        {
            return "X";
        }

        public String getType()
        {
            return this.type;
        }
        
        public String[] getCollisionTypes()
        {
            return this.collisionTypes;
        }

        public Point getPosition()
        {
            return new Point(-1, -1);
        }

        public void collidedWith(I_Collidable c)
        {
            
        }
    }
}
