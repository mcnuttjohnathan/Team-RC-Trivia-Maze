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
            try
            {
                bFormatter.Serialize(stream, sd);
            }
            catch(SerializationException e)
            {
                Console.WriteLine(e);
            }
            stream.Close();
        }


        public SaveData DeSerializeObject(string filename)
        {
            SaveData saveData = new SaveData();
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            try { 
                saveData = (SaveData)bFormatter.Deserialize(stream);
                return saveData;
            }catch (SerializationException e){
                Console.Write(e);
            }
            stream.Close();
            return saveData;
            
        }
    }
}
