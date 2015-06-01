using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class TrueOrFalseBox : Component {
        public enum ToF { TRUE, FALSE };

        private Rectangle boxImage;
        private Brush boxColor = Brushes.AntiqueWhite;
        private Brush borderColor = Brushes.Black;

        private Boolean correct;
        private Font textFont = new Font(FontFamily.GenericSerif, 24);
        private Brush textColor = Brushes.Black;
        private PointF textPosition;

        public TrueOrFalseBox(Boolean correct, TriviaController.Location location, ToF tof) {
            InitializeComponent();

            this.init(correct, location, tof);
        }

        public TrueOrFalseBox(Boolean correct, TriviaController.Location location, ToF tof, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(correct, location, tof);
        }

        private void init(Boolean correct, TriviaController.Location location, ToF tof) {
            this.correct = correct;

            if (location == TriviaController.Location.TOP) {
                if (tof == ToF.TRUE) {
                    this.boxImage = new Rectangle(32, 148, 200, 64);
                    this.textPosition = new PointF(48, 160);
                }
                else if (tof == ToF.FALSE) {
                    this.boxImage = new Rectangle(248, 148, 200, 64);
                    this.textPosition = new PointF(260, 160);
                }
            }
            else if (location == TriviaController.Location.BOTTOM) {
                if (tof == ToF.TRUE) {
                    this.boxImage = new Rectangle(32, 396, 200, 64);
                    this.textPosition = new PointF(48, 408);
                }
                else if (tof == ToF.FALSE) {
                    this.boxImage = new Rectangle(248, 396, 200, 64);
                    this.textPosition = new PointF(260, 408);
                }
            }
        }//end init

        public Boolean submitAnswer() {
            return correct;
        }
    }
}
