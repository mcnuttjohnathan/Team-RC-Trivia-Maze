using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TriviaMaze.com.teamrc.gameobjects;

namespace TriviaMaze.com.teamrc.SaveUtil
{
    [Serializable()]
    public class ObjectToSerialize : ISerializable
    {
        private string map;
        private int playerX;
        private int playerY;

        public string Map
        {
            get { return this.map; }
            set { this.map = value; }
        }

        public int PlayerXLoc
        {
            get { return this.playerX; }
            set { this.playerX = value; }
        }

        public int PlayerYLoc
        {
            get { return this.playerY; }
            set { this.playerY = value; }
        }

        public ObjectToSerialize()
        {
        }

        public ObjectToSerialize(SerializationInfo info, StreamingContext ctxt)
        {
            this.map = (string)info.GetValue("Map", typeof(string));
            this.playerX = (int) info.GetValue("XValue", typeof (int));
            this.playerY = (int) info.GetValue("YValue", typeof (int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Map", this.map);
            info.AddValue("XValue", this.playerX);
            info.AddValue("YValue", this.playerY);
        }
    }
}
