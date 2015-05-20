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

        public static void reset() {
            collidableTypes = new Dictionary<string, ArrayList>();
        }

        public static void add(I_Collidable o) {
            if (!collidableTypes.ContainsKey(o.getType()))
                collidableTypes.Add(o.getType(), new ArrayList());

            collidableTypes[o.getType()].Add(o);
        }

        public static void remove(I_Collidable o) {
            if (collidableTypes.ContainsKey(o.getType())) {
                collidableTypes[o.getType()].Remove(o);

                if (collidableTypes[o.getType()].Count == 0)
                    collidableTypes.Remove(o.getType());
            }
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

        public static void testSingleCollison(I_Collidable headObject, I_Collidable collidedObject) {
            //
        }

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
    }
}
