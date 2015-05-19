using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.gameobjects{
    public partial class Empty : Component{
        public Empty(){
            InitializeComponent();
        }

        public Empty(IContainer container){
            container.Add(this);

            InitializeComponent();
        }

        public string toString(){
            return "X";
        }
    }
}
