using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaMaze.com.teamrc.gameobjects;
using DatabaseSystem;

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
                        if (r[i, j].getExits() > 1)
                        {
                            s += (r[i, j].getDoorDown().toString());
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
            //get user input for file name
            //load in input
        }

    }
}
