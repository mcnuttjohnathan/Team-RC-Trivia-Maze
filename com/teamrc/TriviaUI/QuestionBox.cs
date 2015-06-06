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

        private String[] question = new String[4];
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
            this.parseQuestion(question);

            if (location == TriviaController.Location.TOP) {
                this.boxImage = new Rectangle(32, 16, 416, 128);
                this.textPosition = new PointF(48, 32);
            }
            else if (location == TriviaController.Location.BOTTOM) {
                this.boxImage = new Rectangle(32, 272, 416, 120);
                this.textPosition = new PointF(48, 288);
            }
        }

        private void parseQuestion(String question) {
            String[] q = question.Split(new char[]{' '});
            int i = 0;
            int curr = 0;
            int buffer = 0;

            while (curr < q.GetLength(0)) {
                if (buffer + q[curr].Length + 1 <= 35){
                    buffer += q[curr].Length + 1;
                    this.question[i] += q[curr] + " ";
                    curr++;
                }
                else {
                    i++;
                    buffer = 0;
                }

            }


        }

        public Rectangle getImage() { return this.boxImage; }

        public Brush getBoxColor() { return this.boxColor; }

        public Brush getBorderColor() { return this.borderColor; }

        public Font getFont() { return this.textFont; }

        public Brush getTextColor() { return this.textColor; }

        public PointF getTextPosition() { return this.textPosition; }

        public String getQuestion() {
            String q = "";
 
            for(int i = 0; i < this.question.GetLength(0); i++)
                q += this.question[i] + "\n";

            return q;
        }
    }
}
