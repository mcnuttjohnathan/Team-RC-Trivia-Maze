using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class TrueOrFalseBox : A_AnswerBox {
        public enum ToF { TRUE, FALSE };

        private Boolean correct;

        public TrueOrFalseBox(Boolean correct, TriviaController.Location location, ToF tof)
            : base(new Font(FontFamily.GenericSerif, 24)) {
            InitializeComponent();

            this.init(correct, location, tof);
        }

        public TrueOrFalseBox(Boolean correct, TriviaController.Location location, ToF tof, IContainer container)
            : base(new Font(FontFamily.GenericSerif, 24)) {
            container.Add(this);

            InitializeComponent();

            this.init(correct, location, tof);
        }

        private void init(Boolean correct, TriviaController.Location location, ToF tof) {
            this.correct = correct;

            if (location == TriviaController.Location.TOP) {
                if (tof == ToF.TRUE) {
                    this.setImage(new Rectangle(32, 148, 200, 64));
                    this.setTextPosition(new PointF(48, 160));
                    this.setText("1) True");
                }
                else if (tof == ToF.FALSE) {
                    this.setImage(new Rectangle(248, 148, 200, 64));
                    this.setTextPosition(new PointF(260, 160));
                    this.setText("2) False");
                }
            }
            else if (location == TriviaController.Location.BOTTOM) {
                if (tof == ToF.TRUE) {
                    this.setImage(new Rectangle(32, 396, 200, 64));
                    this.setTextPosition(new PointF(48, 408));
                    this.setText("1) True");
                }
                else if (tof == ToF.FALSE) {
                    this.setImage(new Rectangle(248, 396, 200, 64));
                    this.setTextPosition(new PointF(260, 408));
                    this.setText("2) False");
                }
            }
        }//end init

        public override Boolean submitAnswer() {
            return correct;
        }
    }
}
