using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class QuestionBox : Component {
        public QuestionBox() {
            InitializeComponent();
        }

        public QuestionBox(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }
    }
}
