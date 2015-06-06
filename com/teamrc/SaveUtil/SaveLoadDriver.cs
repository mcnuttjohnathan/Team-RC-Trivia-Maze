﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaMaze.com.teamrc.gameobjects;
using DatabaseSystem;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

namespace TriviaMaze.com.teamrc.savefiles
{
    class SaveLoadDriver
    {

        Player player;
        Map map;
        QuestionSource questionSource;

        public SaveLoadDriver()
        {
            load();
        }

        public SaveLoadDriver(Player p, Map m, QuestionSource qs)
        {
            this.player = p;
            this.map = m;
            this.questionSource = qs;
            save();
        }

        public void save()
        {
            Console.WriteLine("Saving Data...");
            StreamWriter write = new StreamWriter("savedata.txt");
            Room[,] r = this.map.getRooms();
            write.WriteLine(r.GetLength(0) + " " + r.GetLength(1));
            for (int i = 0; i < r.GetLength(0); i++)
            {
                for (int j = 0; j < r.GetLength(1); j++)
                {
                    String s = i + " " + j + " " + r[i,j].getExits() + " ";
                    if(r[i,j].getExits() != 0){
                        if (r[i, j].getExits() % 2 == 1)
                        {
                            s += (r[i, j].getDoorRight().toString()) + " ";
                        }
                        else
                        {
                            s += "X ";
                        }
                        if (r[i, j].getExits() > 1)
                        {
                            s += (r[i, j].getDoorDown().toString());
                        }
                        else
                        {
                            s += "X";
                        }
                    }
                    write.WriteLine(s);
                }
            }
            write.Close();
            Console.WriteLine("Data Saved.");
 


            //save out
        }

        public void load()
        {
            Console.WriteLine("Loading File...");
            String line;
            System.IO.StreamReader file = new System.IO.StreamReader("savedata.txt");
            line = file.ReadLine();
            int h = (int)line[0];
            int w = (int)line[2];
            Room[,] recoveredMap = new Room[h, w];

            Point p = new Point(0, 0);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Room r = new Room(0, p);
                    if (i == h - 1 && j == w - 1)
                    {
                        r.makeFinish();
                    }
                    recoveredMap[i, j] = r;
                    p.X = p.X + 128;
                }
                p.Y = p.Y + 128;
                p.X = 0;
            }

            for (int m = 0; m < h; m++)
            {
                for (int n = 0; n < w; n++)
                {
                    line = file.ReadLine();
                    try
                    {
                        recoveredMap[m,n].setExits((int)line[4]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                    if((int)line[4] % 2 == 1){
                        if(line[6] == 'N'){
                            DoorNew d = new DoorNew(0,0);
                            recoveredMap[m,n].setDoorRight(d);
                        }
                        if(line[8] == 'N'){
                            DoorNew d = new DoorNew(0,0);
                            recoveredMap[m,n].setDoorDown(d);
                        }
                    }
                }
            }

            file.Close();
            Map nMap = new Map(recoveredMap, h, w);
            this.map = nMap;

        }

        public Map getLoadMap(){
            return this.map;
        }

    }
}
