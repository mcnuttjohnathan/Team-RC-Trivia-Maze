using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class QuestionBox : Component {
        private Rectangle boxImage;
        private Brush boxColor = Brushes.AntiqueWhite;
        private Brush borderColor = Brushes.Black;

        private String question;
        private Font textFont = new Font(FontFamily.GenericSerif, 16);
        private Brush textColor = Brushes.Black;
        private PointF textPosition;
        
        public QuestionBox(String question, TriviaController.Location location) {
            InitializeComponent();

            this.init(question, location);
        }

        public QuestionBox(String question, TriviaController.Location location, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(question, location);
        }

        private void init(String question, TriviaController.Location location) {
            this.question = question;

            if (location == TriviaController.Location.TOP) {
                this.boxImage = new Rectangle(32, 16, 416, 128);
                this.textPosition = new PointF(48, 32);
            }
            else if (location == TriviaController.Location.BOTTOM) {
                this.boxImage = new Rectangle(32, 272, 416, 120);
                this.textPosition = new PointF(48, 288);
            }
        }

        public Rectangle getImage() { return this.boxImage; }

        public Brush getBoxColor() { return this.boxColor; }

        public Brush getBorderColor() { return this.borderColor; }

        public Font getFont() { return this.textFont; }

        public Brush getTextColor() { return this.textColor; }

        public PointF getTextPosition() { return this.textPosition; }

        public String getQuestion() { return this.question; }
    }
}
