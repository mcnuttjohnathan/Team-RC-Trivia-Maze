using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class A_AnswerBox : Component {
        private Rectangle boxImage;
        private Brush boxColor = Brushes.AntiqueWhite;
        private Brush borderColor = Brushes.Black;

        private Font textFont;
        private Brush textColor = Brushes.Black;
        private PointF textPosition;
        
        public A_AnswerBox() {
            InitializeComponent();
        }

        public A_AnswerBox(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }
    }
}
