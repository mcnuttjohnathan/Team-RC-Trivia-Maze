using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TriviaMaze.com.teamrc.savefiles
{
    class Serializer
    {

        public Serializer()
        {
        }

        public void SerializeObject(string filename, SaveData sd)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, sd);
            stream.Close();
        }


        public SaveData DeSerializeObject(string filename)
        {
            SaveData saveData;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            saveData = (SaveData)bFormatter.Deserialize(stream);
            stream.Close();
            return saveData;
        }
    }
}
