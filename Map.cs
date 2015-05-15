using System;

namespace TriviaMaze
{
    public class Map
    {
        Room[,] m;
        int[] start;
        int[] finish;

        public Map()
        {

        }

        public Map(int s)
        {
            this.m = new Room[s, s];
            this.start = new int[2] { 0, 0 };
            this.finish = new int[2] { s - 1, s - 1 };
        }

        public void setRoom(int i, int j, Room r)
        {
            m[i, j] = r;
        }

        public Room getRoom(int i, int j)
        {
            return m[i, j];
        }

        public void setStart(int[] i)
        {
            this.start = i;
        }

        public int[] getStart()
        {
            return this.start;
        }

        public void setFinish(int[] i)
        {
            this.finish = i;
        }

        public int[] getFinish()
        {
            return this.finish;
        }

        public string toString()
        {
            string result = "";
            for (int i = 0; i < this.m.GetLength(0); i++)
            {
                for (int j = 0; j < this.m.GetLength(1); j++)
                {
                    int[] e = m[i, j].getExits();
                    if (start[0] == i && start[1] == j)
                    {
                        result += 'S';
                    }
                    else if (finish[0] == i && finish[1] == j)
                    {
                        result += 'E';
                    }
                    else
                    {
                        result += 'O';
                    }
                    if (e[1] == 1)
                    {
                        result += '-';
                    }
                    else
                    {
                        result += ' ';
                    }
                }
                if (i != 3)
                {
                    result += "\n| | | | \n";
                }
            }
            return result;
        }

    }
}