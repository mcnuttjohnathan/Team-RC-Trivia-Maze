using System;

namespace TriviaMaze
{
    public class MazeGenerator
    {
        public Map generate()
        {

            int mazeSize = 4;
            Map m = new Map(mazeSize);
            for (int i = 0; i < mazeSize; i++)
            {
                for (int j = 0; j < mazeSize; j++)
                {
                    int[] curExit = calcExits(i, j, mazeSize);
                    Room r = new Room(curExit);
                    m.setRoom(i, j, r);
                }
            }
            return m;
        }

        public int[] calcExits(int i, int j, int s)
        {
            int[] exits = new int[4];
            if (i == 0)
            {
                exits[0] = 0;
            }
            else
            {
                exits[0] = 1;
            }

            if (i == s - 1)
            {
                exits[2] = 0;
            }
            else
            {
                exits[2] = 1;
            }
            if (j == 0)
            {
                exits[3] = 0;
            }
            else
            {
                exits[3] = 1;
            }
            if (j == s - 1)
            {
                exits[1] = 0;
            }
            else
            {
                exits[1] = 1;
            }
            return exits;
        }

    }
}