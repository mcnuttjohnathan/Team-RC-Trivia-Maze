using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TriviaMaze.com.teamrc.gameobjects;

namespace TriviaMaze.com.teamrc.SaveUtil
{
    [Serializable()]
    public class ObjectToSerialize : ISerializable
    {
        private Room[,] map;

        public Room[,] Map
        {
            get { return this.map; }
            set { this.map = value; }
        }

        public ObjectToSerialize()
        {
        }

        public ObjectToSerialize(SerializationInfo info, StreamingContext ctxt)
        {
            this.map = (Room[,])info.GetValue("Map", typeof(Room[,]));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Map", this.map);
        }
    }
}
