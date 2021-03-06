﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * A Multiple Choice Box that contains one of
 * four possible answers.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class MultipleChoiceBox : A_AnswerBox {
        public enum Letter { A, B, C, D };

        private String _answer;
        private Boolean _correct;

        /**
         * constructs the Multiple Choice Box Component
         * @param answer - the answer text
         * @param correct - if the answer is correct
         * @param location - whether the box is Top or Bottom
         * @param letter - the boxes respective letter (A, B, C, D)
         */
        public MultipleChoiceBox(String answer, Boolean correct, TriviaController.Location location, Letter letter)
            : base(new Font(FontFamily.GenericSerif, 12)) {
            InitializeComponent();

            this.init(answer, correct, location, letter);
        }

        /**
         * constructs the Multiple Choice Box Component
         * @param answer - the answer text
         * @param correct - if the answer is correct
         * @param location - whether the box is Top or Bottom
         * @param letter - the boxes respective letter (A, B, C, D)
         * @param container - a container for the component
         */
        public MultipleChoiceBox(String answer, Boolean correct, TriviaController.Location location, Letter letter, IContainer container)
            : base(new Font(FontFamily.GenericSerif, 12)) {
            container.Add(this);

            InitializeComponent();

            this.init(answer, correct, location, letter);
        }

        /**
         * @private
         * Initalizes the multiple choice box component.
         */
        private void init(String answer, Boolean correct, TriviaController.Location location, Letter letter) {
            this._answer = answer;
            this._correct = correct;

            if (location == TriviaController.Location.TOP) {
                if (letter == Letter.A) {
                    this.setImage(new Rectangle(32, 148, 200, 32));
                    this.setTextPosition(new PointF(48, 152));
                    this.setText("1) " + answer);
                }
                else if (letter == Letter.B) {
                    this.setImage(new Rectangle(248, 148, 200, 32));
                    this.setTextPosition(new PointF(260, 152));
                    this.setText("2) " + answer);
                }
                else if (letter == Letter.C) {
                    this.setImage(new Rectangle(32, 184, 200, 32));
                    this.setTextPosition(new PointF(48, 188));
                    this.setText("3) " + answer);
                }
                else if (letter == Letter.D) {
                    this.setImage(new Rectangle(248, 184, 200, 32));
                    this.setTextPosition(new PointF(260, 188));
                    this.setText("4) " + answer);
                }
            }
            else if (location == TriviaController.Location.BOTTOM) {
                if (letter == Letter.A) {
                    this.setImage(new Rectangle(32, 396, 200, 32));
                    this.setTextPosition(new PointF(48, 400));
                    this.setText("1) " + answer);
                }
                else if (letter == Letter.B) {
                    this.setImage(new Rectangle(248, 396, 200, 32));
                    this.setTextPosition(new PointF(260, 400));
                    this.setText("2) " + answer);
                }
                else if (letter == Letter.C) {
                    this.setImage(new Rectangle(32, 432, 200, 32));
                    this.setTextPosition(new PointF(48, 436));
                    this.setText("3) " + answer);
                }
                else if (letter == Letter.D) {
                    this.setImage(new Rectangle(248, 432, 200, 32));
                    this.setTextPosition(new PointF(260, 436));
                    this.setText("4) " + answer);
                }
            }
        }// end init

        /**
         * Check is the answer is correct.
         * @returns if the choice is correct
         */
        public override Boolean submitAnswer() {
            return _correct;
        }
    }
}
