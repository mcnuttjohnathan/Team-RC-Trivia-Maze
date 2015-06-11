using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Input box that allows the player to input an
 * answer using the keyboard.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class InputBox : A_AnswerBox {
        private String _answer;
        private String _input = "";

        /**
         * Constructs the Input Box component
         */
        public InputBox(String answer, TriviaController.Location location)
            : base(new Font(FontFamily.GenericSerif, 24)) {
            InitializeComponent();

            this.init(answer, location);
        }

        /**
         * Constructs the Input Box component
         */
        public InputBox(String answer, TriviaController.Location location, IContainer container)
            : base(new Font(FontFamily.GenericSerif, 24)) {
            container.Add(this);

            InitializeComponent();

            this.init(answer, location);
        }

        /**
         * @private
         * initializes the component
         */
        private void init(String answer, TriviaController.Location location) {
            this._answer = answer;
            this.setText("Ans: " + _input);

            if (location == TriviaController.Location.TOP) {
                this.setImage(new Rectangle(32, 148, 416, 64));
                this.setTextPosition(new PointF(48, 160));
            }
            else if (location == TriviaController.Location.BOTTOM) {
                this.setImage(new Rectangle(32, 396, 416, 64));
                this.setTextPosition(new PointF(48, 408));
            }
        }

        /**
         * adds the sent character to the input text.
         * @param c - the character to add.
         */
        public void addCharacter(char c) {
            if (_input.Length < 20)
                _input += c;

            this.setText("Ans: " + _input);
        }

        /**
         * removes the last character from the input text.
         */
        public void removeCharacter() {
            if (_input.Length > 0)
                _input = _input.Substring(0, _input.Length - 1);

            this.setText("Ans: " + _input);
        }

        /**
         * Checks if the input matches the answer.
         * @returns correct - whether the answer is correct.
         */
        public override Boolean submitAnswer() {
            if (this._input.Equals(this._answer.ToLower()))
                return true;

            return false;
        }
    }
}
