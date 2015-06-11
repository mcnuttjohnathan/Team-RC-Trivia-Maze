using DatabaseSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaMaze.com.teamrc.gameobjects;

/**
 * Loads and Stores the current question and answer in use.
 * 
 * @author Johnathan McNutt
 */
namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class TriviaController : Component {
        public enum Location { TOP, BOTTOM };

        private Location _location;
        private QuestionBox _question;
        private A_AnswerBox[] _answers;

        /**
         * Constructs the Trivia Controller component
         */
        public TriviaController() {
            InitializeComponent();
        }

        /**
         * Constructs the Trivia Controller component
         * @param container - a container to parent the component
         */
        public TriviaController(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }

        /**
         * Constructs question and answers boxes for use
         * in trivia.
         */
        public void loadQuestionAnswer(QuestionAnswer qa, Player p) {
            if (p.getPosition().Y <= 224) {
                this._location = Location.BOTTOM;
            }
            else
                this._location = Location.TOP;

            this._question = new QuestionBox(qa.Question, _location);
            String[] ara = qa.Answers;
            String correct = ara[0];

            if (qa.QuestionType == QUESTION_TYPE.MULTIPLE_CHOICE) {
                this._answers = new A_AnswerBox[4];

                String temp;
                Random rand = new Random();
                int num;

                for (int i = 0; i < 4; i++) {
                    num = rand.Next(4);

                    temp = ara[num];
                    ara[num] = ara[i];
                    ara[i] = temp;
                }

                this._answers[0] = new MultipleChoiceBox(ara[0], correct.Equals(ara[0]), this._location, MultipleChoiceBox.Letter.A);
                this._answers[1] = new MultipleChoiceBox(ara[1], correct.Equals(ara[1]), this._location, MultipleChoiceBox.Letter.B);
                this._answers[2] = new MultipleChoiceBox(ara[2], correct.Equals(ara[2]), this._location, MultipleChoiceBox.Letter.C);
                this._answers[3] = new MultipleChoiceBox(ara[3], correct.Equals(ara[3]), this._location, MultipleChoiceBox.Letter.D);
            }
            else if (qa.QuestionType == QUESTION_TYPE.TRUE_FALSE) {
                this._answers = new A_AnswerBox[2];

                if(correct.ToLower().Equals("true")){
                    this._answers[0] = new TrueOrFalseBox(true, this._location, TrueOrFalseBox.ToF.TRUE);
                    this._answers[1] = new TrueOrFalseBox(false, this._location, TrueOrFalseBox.ToF.FALSE);
                }
                else if(correct.ToLower().Equals("false")){
                    this._answers[0] = new TrueOrFalseBox(false, this._location, TrueOrFalseBox.ToF.TRUE);
                    this._answers[1] = new TrueOrFalseBox(true, this._location, TrueOrFalseBox.ToF.FALSE);
                }
                else
                    throw new Exception("neither true or false");
            }
            else if (qa.QuestionType == QUESTION_TYPE.INPUT) {
                this._answers = new A_AnswerBox[1];

                this._answers[0] = new InputBox(ara[0], this._location);
            }

        }//end load questions method

        /**
         * @return location - whether the boxes are top or bottom
         */
        public Location getLocation() { return this._location; }

        /**
         * @returns the question box for the current question
         */
        public QuestionBox getQuestion() { return this._question; }

        /**
         * @returns a set of answer boxes for the current question
         */
        public A_AnswerBox[] getAnswers() { return this._answers; }
    }
}
