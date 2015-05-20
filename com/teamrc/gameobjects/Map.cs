using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using TriviaMaze.com.teamrc.util;

namespace TriviaMaze.com.teamrc.gameobjects
{
    public partial class Map : Component
    {

        private Room[,] map;
        int size;
        Point start;
        Point finish;

        public Map(int s)
        {
            InitializeComponent();
            this.map = new Room[s, s];
            this.size = s;
            this.start = new Point(0, 0);
            this.finish = new Point(s - 1, s - 1);
        }

        public Map(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public Room getRoom(int i, int j)
        {
            return map[i, j];
        }

        public void setRoom(int i, int j, Room r)
        {
            this.map[i, j] = r;
        }

        public void setStart(Point i)
        {
            this.start = i;
        }

        public Point getStart()
        {
            return this.start;
        }

        public void setFinish(Point i)
        {
            this.finish = i;
        }

        public Point getFinish()
        {
            return this.finish;
        }

        public String toString()
        {
            String res = "";

            for (int i = 0; i < this.size; i++)
            {
                String nextRow ="";
                for (int j = 0; j < this.size; j++)
                {
                    int e = map[i, j].getExits();

                    if (start.X == i && start.Y == j){
                        res += 'S';
                    }else if (finish.X == i && finish.Y == j){
                        res += 'E';
                    }else{
                        res += 'O';
                    }


                    if (e > 1){
                        res += '-';
                    }else{
                        res += ' ';
                    }


                    if (e % 2 == 1){
                        nextRow += "| ";
                    }else{
                        nextRow += "  ";
                    }
                }
                res = res + "\n" + nextRow + "\n";
            }
            return res;
        }

    }
}
