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
    internal class SaveLoadDriver{

        private Player player;
        private Map map;
        private Point p;

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

                    String s = i + " " + j + " " + r[i, j].getExits() + " ";

                    if (r[i, j].getExits() != 0){

                        if (r[i, j].getExits()%2 == 1){
                            s += (r[i, j].getDoorRight().toString()) + " ";
                        }else{
                            s += "X ";
                        }

                        if (r[i, j].getExits() > 1){
                            s += (r[i, j].getDoorDown().toString());
                        }else{
                            s += "X";
                        }

                    }

                    data += s + "\n";
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
            this.p = new Point(x, y);

            Console.WriteLine("This is the string before the split : \n{0}", s);

            string[] mapString = s.Split('\n');

            Console.WriteLine("This is the string after the split:");
            for (int i = 0; i < mapString.GetLength(0); i++){
                Console.WriteLine(mapString[i]);
            }

            int h = (int) Char.GetNumericValue(mapString[0][0]);
            int w = (int) Char.GetNumericValue(mapString[0][2]);
            Map nmap = new Map(h, w);
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

            int count = 0;

            for (int m = 0; m < h; m++){
                for (int n = 0; n < w; n++){
                    count++;
                    String line = mapString[count];

                    if (line != null){
                        recoveredMap[m, n].setExits((int) Char.GetNumericValue(line[4]));

                        Point loc = recoveredMap[m, n].getLocation();

                        if ((int) Char.GetNumericValue(line[4])%2 == 1){


                            if (line[6] == 'N' || line[6] == 'U'){
                                DoorNew d = new DoorNew(loc.X + 96, loc.Y + 32);
                                recoveredMap[m, n].setDoorRight(d);
                            }else if (line[6] == 'L'){
                                DoorLocked d = new DoorLocked(loc.X + 96, loc.Y + 32);
                                recoveredMap[m, n].setDoorRight(d);
                            }else{
                                DoorUnlocked d = new DoorUnlocked(loc.X + 96, loc.Y + 32);
                                recoveredMap[m, n].setDoorRight(d);
                            }
                        }

                        if ((int) Char.GetNumericValue(line[4]) > 1){
                            if (line[8] == 'N' || line[8] == 'U'){
                                DoorNew d = new DoorNew(loc.X + 32, loc.Y + 96);
                                recoveredMap[m, n].setDoorDown(d);
                            } else if (line[8] == 'L'){
                                DoorLocked d = new DoorLocked(loc.X + 32, loc.Y + 96);
                                recoveredMap[m, n].setDoorDown(d);
                            }else{
                                DoorUnlocked d = new DoorUnlocked(loc.X + 32, loc.Y + 96);
                                recoveredMap[m, n].setDoorDown(d);
                            }
                        }
                    }
                }
            }

            nmap.setRooms(recoveredMap);
            this.map = nmap;

        }

        /*
         * This is the get method to get the point that the player exists at
         * @returns p - Point the player rectangle is located
         * */
        public Point getPlayer(){
            return this.p;
        }

         /*
          * This is the get method to get the map that was saved
          * @returns map - the loaded in map from the file
          */
        public Map getMap(){
            return this.map;
        }
    }

}
