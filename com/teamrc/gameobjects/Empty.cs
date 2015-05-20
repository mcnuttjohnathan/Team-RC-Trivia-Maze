/**
 * Empty is the I_Collidable exteded component that represents the areas the main character cannot walk on, like walls
 * 
 * @author Zoe Baker
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaMaze.com.teamrc.util;

namespace TriviaMaze.com.teamrc.gameobjects{
    public partial class Empty : Component, I_Collidable{

        private String type = CollisionManager.NONE;
        private String[] collisionTypes = {CollisionManager.NONE};

        /**
         * Main constructor for the Empty Component. 
         * Nothing needs to be stored
         **/
        public Empty(){
            InitializeComponent();
        }

        /**
         * Default container constructor, will not be used
         **/
        public Empty(IContainer container){
            container.Add(this);

            InitializeComponent();
        }

        /**
         * The toString used for printing and testing purposes
         * 
         * @returns x - the string denoting a wall
         **/
        public String toString(){
            return "X";
        }

        /**
         * The overrided get type method
         * 
         * @returns string - denoting the type
         **/
        public String getType(){
            return this.type;
        }
        
        /**
         * The overrided get collision type
         * 
         * @returns String[] - denoting the collision type is empty
         **/
        public String[] getCollisionTypes(){
            return this.collisionTypes;
        }

        /**
         * The overrride get posion 
         * 
         * @returns Point - a random point of no value for an empty position
         **/
        public Point getPosition(){
            return new Point(-1, -1);
        }

        /** The overriden colllided with method
         * 
         * @param c - the I_Collidable object it could be colliding with
         **/
        public void collidedWith(I_Collidable c){
            
        }
    }
}
