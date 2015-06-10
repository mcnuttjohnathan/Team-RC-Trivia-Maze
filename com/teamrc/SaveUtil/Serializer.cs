using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TriviaMaze.com.teamrc.SaveUtil
{
    public class Serializer
    {
        public Serializer()
        {
        }

        public void SerializeObject(ObjectToSerialize objectToSerialize)
        {
            Stream stream = File.Open("savefile.txt", FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public ObjectToSerialize DeSerializeObject()
        {
            ObjectToSerialize objectToSerialize;
            Stream stream = File.Open("savefile.txt", FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (ObjectToSerialize) bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}


