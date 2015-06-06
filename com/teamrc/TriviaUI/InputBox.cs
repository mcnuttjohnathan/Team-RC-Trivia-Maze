using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class InputBox : A_AnswerBox {
        private String answer;
        private String input = "";

        public InputBox(String answer, TriviaController.Location location)
            : base(new Font(FontFamily.GenericSerif, 24)) {
            InitializeComponent();

            this.init(answer, location);
        }

        public InputBox(String answer, TriviaController.Location location, IContainer container)
            : base(new Font(FontFamily.GenericSerif, 24)) {
            container.Add(this);

            InitializeComponent();

            this.init(answer, location);
        }

        private void init(String answer, TriviaController.Location location) {
            this.answer = answer;
            this.setText("Ans: " + input);

            if (location == TriviaController.Location.TOP) {
                this.setImage(new Rectangle(32, 148, 416, 64));
                this.setTextPosition(new PointF(48, 160));
            }
            else if (location == TriviaController.Location.BOTTOM) {
                this.setImage(new Rectangle(32, 396, 416, 64));
                this.setTextPosition(new PointF(48, 408));
            }
        }

        public void addCharacter(char c) {
            if (input.Length < 20)
                input += c;

            this.setText("Ans: " + input);
        }

        public void removeCharacter() {
            if (input.Length > 0)
                input = input.Substring(0, input.Length - 1);

            this.setText("Ans: " + input);
        }

        public override Boolean submitAnswer() {
            if (this.input.Equals(this.answer.ToLower()))
                return true;

            return false;
        }
    }
}
