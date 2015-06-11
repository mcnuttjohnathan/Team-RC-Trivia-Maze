using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Abstract Answer Box that all other answer boxes inherit from.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.TriviaUI {
    public abstract partial class A_AnswerBox : Component {
        private Rectangle _boxImage = new Rectangle(0, 0, 32, 32);
        private Brush _boxColor = Brushes.AntiqueWhite;
        private Brush _borderColor = Brushes.Black;

        private String _text;
        private Font _textFont;
        private Brush _textColor = Brushes.Black;
        private PointF _textPosition = new PointF();
        
        /**
         * Constructs the abstract answer box component.
         */
        public A_AnswerBox(Font font) {
            InitializeComponent();

            this.init(font);
        }

        /**
         * Constructs the abstract answer box component.
         */
        public A_AnswerBox(Font font, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(font);
        }

        /**
         * @private
         * Initialize the component.
         */
        private void init(Font font) {
            this._textFont = font;
        }

        /**
         * checks the users answer and returns if its correct.
         */
        public abstract Boolean submitAnswer();

        /**
         * returns the answer boxes rectangle.
         */
        public Rectangle getImage() { return this._boxImage; }

        /**
         * sets the answer boxes rectangle.
         */
        public void setImage(Rectangle r) { this._boxImage = r; }

        /**
         * returns the answer boxes inner color.
         */
        public Brush getBoxColor() { return this._boxColor; }

        /**
         * returns the answer boxes outer color.
         */
        public Brush getBorderColor() { return this._borderColor; }

        /**
         * returns the answer boxes text.
         */
        public String getText() { return this._text; }

        /**
         * sets the answer boxes text.
         */
        public void setText(String text) { this._text = text; }

        /**
         * returns the text font.
         */
        public Font getFont() { return this._textFont; }

        /**
         * returns the text color.
         */
        public Brush getTextColor() { return this._textColor; }

        /**
         * returns the text position.
         */
        public PointF getTextPosition() { return this._textPosition; }

        /**
         * sets the text position.
         */
        public void setTextPosition(PointF pf) { this._textPosition = pf; }
    }
}
