using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TriviaMaze.com.teamrc.gameobjects;

/**
 * A Collision Manager that tests collisions between the player and
 * other objects. Stores Objects for comparison.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.util {
    public static partial class CollisionManager : Object {
        public const String PLAYER = "PLAYER";
        public const String FLOOR = "FLOOR";
        public const String UNLOCKED_DOOR = "UNLOCKED_DOOR";
        public const String LOCKED_DOOR = "LOCKED_DOOR";
        public const String NEW_DOOR = "NEW_DOOR";
        public const String USED_DOOR = "USED_DOOR";
        public const String FINISH = "FINISH";
        public const String NONE = "NONE";

        private static Dictionary<String, ArrayList> collidableTypes = new Dictionary<String, ArrayList>();

        /**
         * Creates a new array to empty the Collision Manager.
         */
        public static void reset() {
            collidableTypes = new Dictionary<string, ArrayList>();
        }

        /**
         * Adds a new collidable component to the array.
         * 
         * @param c - the component to add.
         */
        public static void add(I_Collidable c) {
            if (!collidableTypes.ContainsKey(c.getType()))
                collidableTypes.Add(c.getType(), new ArrayList());

            collidableTypes[c.getType()].Add(c);
        }

        /**
         * Removes a collidable component to the array.
         * 
         * @param c - the component to remove.
         */
        public static void remove(I_Collidable c) {
            if (collidableTypes.ContainsKey(c.getType())) {
                collidableTypes[c.getType()].Remove(c);

                if (collidableTypes[c.getType()].Count == 0)
                    collidableTypes.Remove(c.getType());
            }
        }

        /**
         * Tests all objects in the collidable array against the player object and returns
         * any object that is detected to collide.
         * 
         * @param newPlayerPosition - the position the player is attempting to move too.
         * @param player - the player component.
         */
        public static I_Collidable testPlayerCollision(Point newPlayerPosition, Player player) {
            foreach (var c in collidableTypes) {
                for (int i = 0; i < c.Value.Count; i++ ) {
                    I_Collidable collidedObject = (I_Collidable)c.Value[i];

                    if (collidedObject.getPosition().Equals(newPlayerPosition))
                        return collidedObject;
                }
            }
            
            return player;
        }

        /*
        public static void testAllCollsions() {
            foreach(var a in collidableTypes){
                for (int i = 0; i < a.Value.Count; i++ ) {

                    I_Collidable headObject = (I_Collidable)a.Value[i];
                    String[] collisionTypes = headObject.getCollisionTypes();
                    foreach (var c in collidableTypes) {
                        if (collisionTypes.Contains(c.Key)) {
                            //
                        }
                    }
                }
            }
        }
        */
    }
}
