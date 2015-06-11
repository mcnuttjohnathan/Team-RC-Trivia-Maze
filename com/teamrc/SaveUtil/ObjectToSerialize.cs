using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TriviaMaze.com.teamrc.gameobjects;

/*
 *ObjectToSerialize is the object used to hold all of the data that needs to be serialized and has get and set methods for those items
 */

namespace TriviaMaze.com.teamrc.SaveUtil{
    [Serializable()]
    public class ObjectToSerialize : ISerializable{
        private string map;
        private int playerX;
        private int playerY;

        /*
         * The string map get and set methods
         */
        public string Map{
            get { return this.map; }
            set { this.map = value; }
        }

        /*
         * The int player's x location get and set methods
         */
        public int PlayerXLoc{
            get { return this.playerX; }
            set { this.playerX = value; }
        }

        /*
         * The int player's y location get and set methods
         */
        public int PlayerYLoc{
            get { return this.playerY; }
            set { this.playerY = value; }
        }

        /* 
         * The default value constructor for this object
         */
        public ObjectToSerialize(){        }

        /* 
         * The explicit value constructor for this object
         * @param info - Serialization information
         * @param ctxt - Serialization context
        */
        public ObjectToSerialize(SerializationInfo info, StreamingContext ctxt){
            this.map = (string)info.GetValue("Map", typeof(string));
            this.playerX = (int) info.GetValue("XValue", typeof (int));
            this.playerY = (int) info.GetValue("YValue", typeof (int));
        }

        /*
         * The method used to write data into the serialization
         * @param info - Serialization Information
         * @param ctxt - Serialization Context
         */
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt){
            info.AddValue("Map", this.map);
            info.AddValue("XValue", this.playerX);
            info.AddValue("YValue", this.playerY);
        }
    }
}
