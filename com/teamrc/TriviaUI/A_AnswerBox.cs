using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public abstract partial class A_AnswerBox : Component {
        private Rectangle boxImage = new Rectangle(0, 0, 32, 32);
        private Brush boxColor = Brushes.AntiqueWhite;
        private Brush borderColor = Brushes.Black;

        private String text;
        private Font textFont;
        private Brush textColor = Brushes.Black;
        private PointF textPosition = new PointF();
        
        public A_AnswerBox(Font font) {
            InitializeComponent();

            this.init(font);
        }

        public A_AnswerBox(Font font, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(font);
        }

        private void init(Font font) {
            this.textFont = font;
        }

        public abstract Boolean submitAnswer();

        public Rectangle getImage() { return this.boxImage; }

        public void setImage(Rectangle r) { this.boxImage = r; }

        public Brush getBoxColor() { return this.boxColor; }

        public Brush getBorderColor() { return this.borderColor; }

        public String getText() { return this.text; }

        public void setText(String text) { this.text = text; }

        public Font getFont() { return this.textFont; }

        public Brush getTextColor() { return this.textColor; }

        public PointF getTextPosition() { return this.textPosition; }

        public void setTextPosition(PointF pf) { this.textPosition = pf; }
    }
}
