using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * A True or False Answer Box for trivia GUI
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class TrueOrFalseBox : A_AnswerBox {
        public enum ToF { TRUE, FALSE };

        private Boolean _correct;

        /**
         * Constructs the True or False Box component
         * @param correct - if the answer is correct
         * @param location - whether the box is top or bottom screen
         * @param tof - if this is a true or false box.
         */
        public TrueOrFalseBox(Boolean correct, TriviaController.Location location, ToF tof)
            : base(new Font(FontFamily.GenericSerif, 24)) {
            InitializeComponent();

            this.init(correct, location, tof);
        }

        /**
         * Constructs the True or False Box component
         * @param correct - if the answer is correct
         * @param location - whether the box is top or bottom screen
         * @param tof - if this is a true or false box.
         * @param container - becomes parent of this component
         */
        public TrueOrFalseBox(Boolean correct, TriviaController.Location location, ToF tof, IContainer container)
            : base(new Font(FontFamily.GenericSerif, 24)) {
            container.Add(this);

            InitializeComponent();

            this.init(correct, location, tof);
        }

        /**
         * @private
         * Initializes the component
         */
        private void init(Boolean correct, TriviaController.Location location, ToF tof) {
            this._correct = correct;

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

        /**
         * @returns of this answer is correct
         */
        public override Boolean submitAnswer() {
            return _correct;
        }
    }
}
