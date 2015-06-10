using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaMaze.com.teamrc.gameobjects;
using DatabaseSystem;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
/**
 * This class allows the user to save the map to a file and read it back in
 * 
 * @author Zoe Baker
 **/
using TriviaMaze.com.teamrc.graphics;
using TriviaMaze.com.teamrc.SaveUtil;

namespace TriviaMaze.com.teamrc.savefiles{
    class SaveLoadDriver{

        Player player;
        Map map;
        
        /*
         *This is the default constructor, used to load in a map
         */
        public SaveLoadDriver(){
            load();
        }

        /* 
         * This is the explicit value constructor, used to save a map
         * 
         * @param p - the player to be saved
         * @param m - the map to be saved
         * @param qs - the question source to be saved
         **/
        public SaveLoadDriver(Player p, Map m){
            this.player = p;
            this.map = m;
            save();
        }

        /** 
         * This is the save method which goes through the map array and writes out the data needed
         * to the file savedata.txt
         **/
        public void save(){
            Console.WriteLine("Saving Data...");

            string data = "";
            Room[,] r = this.map.getRooms();
            data += r.GetLength(0) + " " + r.GetLength(1) + "\n";

            for (int i = 0; i < r.GetLength(0); i++){
                for (int j = 0; j < r.GetLength(1); j++){

                    String s = i + " " + j + " " + r[i,j].getExits() + " ";

                    if(r[i,j].getExits() != 0){

                        if (r[i, j].getExits() % 2 == 1){
                            s += (r[i, j].getDoorRight().toString()) + " ";
                        }else{
                            s += "X ";
                        }

                        if (r[i, j].getExits() > 1){
                            s += (r[i, j].getDoorDown().toString());
                        } else{
                            s += "X";
                        }

                    }
                    data += s;
                }
            }

            ObjectToSerialize objectToSerialize = new ObjectToSerialize();
            objectToSerialize.Map = data;
            Point p = this.player.getPosition();
            objectToSerialize.PlayerXLoc = p.X;
            objectToSerialize.PlayerYLoc = p.Y;

            Serializer serializer = new Serializer();
            serializer.SerializeObject(objectToSerialize);

            Console.WriteLine("Data Saved.");
        }

        /** 
         * The method called to read in and convert data from the savedata.txt
         **/
        public void load(){

            Console.WriteLine("Loading File...");

            Serializer serializer = new Serializer();
            ObjectToSerialize objectToSerialize = serializer.DeSerializeObject();
            string s = objectToSerialize.Map;
            int x = objectToSerialize.PlayerXLoc;
            int y = objectToSerialize.PlayerYLoc;

            
            //nm.setRooms(objectToSerialize.Map);

            String line;
            System.IO.StreamReader file = new System.IO.StreamReader("savedata.txt");

            line = file.ReadLine();
            int h = (int)Char.GetNumericValue(line[0]);
            int w = (int)Char.GetNumericValue(line[2]);
            Room[,] recoveredMap = new Room[h, w];

            Point p = new Point(0, 0);

            for (int i = 0; i < h; i++){
                for (int j = 0; j < w; j++){

                    Room r = new Room(0, p);

                    if (i == h - 1 && j == w - 1){
                        r.makeFinish();
                    }

                    recoveredMap[i, j] = r;
                    p.X = p.X + 128;
                }

                p.Y = p.Y + 128;
                p.X = 0;
            }

            for (int m = 0; m < h; m++){
                for (int n = 0; n < w; n++){
                    line = file.ReadLine();

                    if (line != null){
                        try{

                            recoveredMap[m, n].setExits((int)Char.GetNumericValue(line[4]));

                            if ((int)Char.GetNumericValue(line[4]) % 2 == 1){
                                if (line[6] == 'N'){
                                    DoorNew d = new DoorNew(0, 0);
                                    recoveredMap[m, n].setDoorRight(d);
                                }

                                if (line[8] == 'N'){
                                    DoorNew d = new DoorNew(0, 0);
                                    recoveredMap[m, n].setDoorDown(d);
                                }

                            }

                        }
                        catch (Exception e){
                            Console.WriteLine(e);
                        }
                    }
                }
            }

            file.Close();
            Map nMap = new Map(recoveredMap, h, w);
            this.map = nMap;

            

        }

        /**
         * This returns the Map from the loaded 
         * 
         * @return map - the map read in from the file converted back to a map object
         **/
        public Map getLoadMap(){
            return this.map;
        }

    }
}
