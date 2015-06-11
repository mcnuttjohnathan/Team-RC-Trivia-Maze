using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/* 
 * The method that uses the serializer built in methods to save the objects to the file "savefile.txt"
 */

namespace TriviaMaze.com.teamrc.SaveUtil{
    public class Serializer{

        /*
         * The Default Value constructor for the serializer
         */
        public Serializer(){}

        /*
         * The method used to send the object that is needed to be serialized and write it out to the file
         * @param objectToSerialize - the object that needs to be saved
         */
        public void SerializeObject(ObjectToSerialize objectToSerialize){
            Stream stream = File.Open("savefile.txt", FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        /*
         * The method used to read in and return the previously serialized object from the file
         * @returns objectToSerialize - the object that was previously saved into the file
         */
        public ObjectToSerialize DeSerializeObject(){
            ObjectToSerialize objectToSerialize;

            Stream stream = File.Open("savefile.txt", FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (ObjectToSerialize) bFormatter.Deserialize(stream);
            stream.Close();

            return objectToSerialize;
        }
    }
}


