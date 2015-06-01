using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class MultipleChoiceBox : Component {
        public enum Letter { A, B, C, D };

        private Rectangle boxImage;
        private Brush boxColor = Brushes.AntiqueWhite;
        private Brush borderColor = Brushes.Black;

        private String answer;
        private Boolean correct;
        private Font textFont = new Font(FontFamily.GenericSerif, 12);
        private Brush textColor = Brushes.Black;
        private PointF textPosition;
        
        public MultipleChoiceBox(String answer, Boolean correct, TriviaController.Location location, Letter letter) {
            InitializeComponent();

            this.init(answer, correct, location, letter);
        }

        public MultipleChoiceBox(String answer, Boolean correct, TriviaController.Location location, Letter letter, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(answer, correct, location, letter);
        }

        private void init(String answer, Boolean correct, TriviaController.Location location, Letter letter) {
            this.answer = answer;
            this.correct = correct;

            if (location == TriviaController.Location.TOP) {
                if (letter == Letter.A) {
                    this.boxImage = new Rectangle(32, 148, 200, 32);
                    this.textPosition = new PointF(48, 152);
                }
                else if (letter == Letter.B) {
                    this.boxImage = new Rectangle(248, 148, 200, 32);
                    this.textPosition = new PointF(260, 152);
                }
                else if (letter == Letter.C) {
                    this.boxImage = new Rectangle(32, 184, 200, 32);
                    this.textPosition = new PointF(48, 188);
                }
                else if (letter == Letter.D) {
                    this.boxImage = new Rectangle(248, 184, 200, 32);
                    this.textPosition = new PointF(260, 188);
                }
            }
            else if (location == TriviaController.Location.BOTTOM) {
                if (letter == Letter.A) {
                    this.boxImage = new Rectangle(32, 396, 200, 32);
                    this.textPosition = new PointF(48, 400);
                }
                else if (letter == Letter.B) {
                    this.boxImage = new Rectangle(248, 396, 200, 32);
                    this.textPosition = new PointF(260, 400);
                }
                else if (letter == Letter.C) {
                    this.boxImage = new Rectangle(32, 432, 200, 32);
                    this.textPosition = new PointF(48, 436);
                }
                else if (letter == Letter.D) {
                    this.boxImage = new Rectangle(248, 432, 200, 32);
                    this.textPosition = new PointF(260, 436);
                }
            }
        }// end init

        public Boolean submitAnswer() {
            return correct;
        }
    }
}
