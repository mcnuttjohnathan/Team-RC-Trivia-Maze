using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaMaze.com.teamrc.gameobjects;
using TriviaMaze.com.teamrc.util;
using DatabaseSystem;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TriviaMaze.com.teamrc.savefiles
{
    class SaveData : ISerializable
    {

        public Player player;
        public Map map;
        public QuestionSource questionSource;

        public SaveData(Player p, Map m, QuestionSource qs)
        {
            this.player = p;
            this.map = m;
            this.questionSource = qs;
        }

        public SaveData()
        {

        }

        public Player getPlayer(){
            return this.player;
        }

        public void setPlayer(Player p)
        {
            this.player = p;
        }

        public Map getMap()
        {
            return this.map;
        }

        public void setMap(Map m)
        {
            this.map = m;
        }

        public QuestionSource getQuestionSource()
        {
            return this.questionSource;
        }

        public void setQuestionSource(QuestionSource qs)
        {
            this.questionSource = qs;
        }

        public SaveData(SerializationInfo info, StreamingContext ctxt)
        {
            this.player = (Player)info.GetValue("Player", typeof(Player));
            this.map = (Map)info.GetValue("Map", typeof(Map));
            this.questionSource = (QuestionSource)info.GetValue("QuestionSource", typeof(QuestionSource));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Player", this.player);
            info.AddValue("Map", this.map);
            info.AddValue("QuestionSource", this.questionSource);
        }
    }
}
