using System;

namespace TriviaMaze
{
    public class Room
    {
        char[,] r;
        int[] exits;

        public Room()
        {
            this.r = new char[5, 5]{{'X','X','X','X','X'},
                                   {'X','O','O','O','X'},
                                   {'X','O','O','O','X'},
                                   {'X','O','O','O','X'},
                                   {'X','X','X','X','X'}};
        }

        public Room(int[] e)
        {
            this.r = new char[5, 5]{{'X','X','X','X','X'},
                                   {'X','O','O','O','X'},
                                   {'X','O','O','O','X'},
                                   {'X','O','O','O','X'},
                                   {'X','X','X','X','X'}};

            this.exits = e;
            makeExits();
        }

        public int[] getExits()
        {
            return this.exits;
        }

        public void setExits(int[] e)
        {
            this.exits = e;
            makeExits();
        }

        public string tostring()
        {
            string result = "";
            for (int i = 0; i < r.GetLength(0); i++)
            {
                for (int j = 0; j < r.GetLength(1); j++)
                {
                    result += r[i, j];
                }
                result += "\n";
            }
            return result;
        }

        public void makeExits()
        {
            if (this.exits[0] == 1)
            {
                this.r[0, 2] = 'O';
            }
            if (this.exits[1] == 1)
            {
                this.r[2, 4] = 'O';
            }
            if (this.exits[2] == 1)
            {
                this.r[4, 2] = 'O';
            }
            if (this.exits[3] == 1)
            {
                this.r[2, 0] = 'O';
            }
        }
    }
}