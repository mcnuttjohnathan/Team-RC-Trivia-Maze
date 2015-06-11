using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * A GUI box containing a trivia question.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class QuestionBox : Component {
        private Rectangle _boxImage;
        private Brush _boxColor = Brushes.AntiqueWhite;
        private Brush _borderColor = Brushes.Black;

        private String[] _question = new String[4];
        private Font _textFont = new Font(FontFamily.GenericSerif, 16);
        private Brush _textColor = Brushes.Black;
        private PointF _textPosition;
        
        /**
         * constructs the Question Box Component
         * @param question - the question text
         * @param location - whether the box is on the top or bottom of the screen
         */
        public QuestionBox(String question, TriviaController.Location location) {
            InitializeComponent();

            this.init(question, location);
        }

        /**
         * constructs the Question Box Component
         * @param question - the question text
         * @param location - whether the box is on the top or bottom of the screen
         * @param container - a container to parent the component
         */
        public QuestionBox(String question, TriviaController.Location location, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(question, location);
        }

        /**
         * @private
         * Initializes the component.
         */
        private void init(String question, TriviaController.Location location) {
            this.parseQuestion(question);

            if (location == TriviaController.Location.TOP) {
                this._boxImage = new Rectangle(32, 16, 416, 128);
                this._textPosition = new PointF(48, 32);
            }
            else if (location == TriviaController.Location.BOTTOM) {
                this._boxImage = new Rectangle(32, 272, 416, 120);
                this._textPosition = new PointF(48, 288);
            }
        }

        /**
         * @private
         * seperates the question into words and divides them
         * to make them fit inside the box.
         */
        private void parseQuestion(String question) {
            String[] q = question.Split(new char[]{' '});
            int i = 0;
            int curr = 0;
            int buffer = 0;

            while (curr < q.GetLength(0)) {
                if (buffer + q[curr].Length + 1 <= 35){
                    buffer += q[curr].Length + 1;
                    this._question[i] += q[curr] + " ";
                    curr++;
                }
                else {
                    i++;
                    buffer = 0;
                }

            }


        }

        /**
         * @returns image - rectangle size and location
         */
        public Rectangle getImage() { return this._boxImage; }

        /**
         * @returns inner box color
         */
        public Brush getBoxColor() { return this._boxColor; }

        /**
         * @returns outer box color
         */
        public Brush getBorderColor() { return this._borderColor; }

        /**
         * @returns text font
         */
        public Font getFont() { return this._textFont; }

        /**
         * @returns text color
         */
        public Brush getTextColor() { return this._textColor; }

        /**
         * @returns text position - Point
         */
        public PointF getTextPosition() { return this._textPosition; }

        /**
         * @returns question text formatted
         */
        public String getQuestion() {
            String q = "";
 
            for(int i = 0; i < this._question.GetLength(0); i++)
                q += this._question[i] + "\n";

            return q;
        }
    }
}
