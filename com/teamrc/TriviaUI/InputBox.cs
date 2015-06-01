using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class InputBox : Component {
        private Rectangle boxImage;
        private Brush boxColor = Brushes.AntiqueWhite;
        private Brush borderColor = Brushes.Black;

        private String answer;
        private Font textFont = new Font(FontFamily.GenericSerif, 24);
        private Brush textColor = Brushes.Black;
        private PointF textPosition;
        
        public InputBox(String answer, TriviaController.Location location) {
            InitializeComponent();

            this.init(answer, location);
        }

        public InputBox(String answer, TriviaController.Location location, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(answer, location);
        }

        private void init(String answer, TriviaController.Location location) {
            this.answer = answer;

            if (location == TriviaController.Location.TOP) {
                this.boxImage = new Rectangle(32, 148, 416, 64);
                this.textPosition = new PointF(48, 160);
            }
            else if (location == TriviaController.Location.BOTTOM) {
                this.boxImage = new Rectangle(32, 396, 416, 64);
                this.textPosition = new PointF(48, 408);
            }
        }

        public Boolean submitAnswer(String answer) {
            if (answer.Equals(this.answer))
                return true;

            return false;
        }
    }
}
