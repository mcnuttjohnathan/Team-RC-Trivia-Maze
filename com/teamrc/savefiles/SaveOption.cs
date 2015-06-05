using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.savefiles
{
    class SaveOption
    {

        SaveData saveData;

        public SaveOption()
        {
            menu();
        }

        public SaveOption(SaveData sd)
        {
            this.saveData = sd;
            menu();
        }

        public void menu()
        {
            Console.WriteLine("Press 1 to save, 2 to load, or any other key to return to game. /n");
            int o = int.Parse(Console.ReadLine());
            if (o == 1)
            {
                save();
            }
            
            if (o == 2)
            {
                load();
            }
            
        }

        public void save()
        {
            Console.WriteLine("Enter what you want to save the file as: ");
            string fn = Console.ReadLine();

        //save out
        }

        public void load()
        {
            //Display files that can be loaded
            //get user input for file name
            //load in input
        }

    }
}
