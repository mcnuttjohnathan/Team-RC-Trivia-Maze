using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.gameobjects{
    public partial class Room : Component{

        Component[,] r;
        int exits;
        Floor f = new Floor(32, 32);
        Empty e = new Empty();

        public Room(){
            InitializeComponent();
            r = new Component[4, 4]{{f,f,f,e},
                                    {f,f,f,e},
                                    {f,f,f,e},
                                    {e,e,e,e}};
            exits = 0;
        }

        public Room(int x, IContainer container){
            container.Add(this);

            InitializeComponent();
            r = new Component[4, 4]{{f,f,f,e},
                                    {f,f,f,e},
                                    {f,f,f,e},
                                    {e,e,e,e}};

            exits = x;
            makeExits();
        }

        public int exit{
            get { return exits; } 
            set { exits = value; }  
        }

        public string toString(){
            string result = "";

            for (int i = 0; i < r.GetLength(0); i++){
                for (int j = 0; j < r.GetLength(1); j++){
                    result += r[i, j].toString();
                }
                result += "\n";
            }

            return result;
        }

        public void makeExits(){
            if (exits == 1){
                r[1, 3] = f;
            }
            if (exits == 2){
                r[3, 1] = f;
            }
            if (exits == 3){
                r[1, 3] = f;
                r[3, 1] = f;
            }
        }

    }
}
