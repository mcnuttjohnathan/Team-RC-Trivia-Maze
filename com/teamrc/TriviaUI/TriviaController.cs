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

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class TriviaController : Component {
        public enum Location { TOP, BOTTOM };

        private Location location;
        private QuestionBox question;
        private A_AnswerBox[] answers;

        public TriviaController() {
            InitializeComponent();
        }

        public TriviaController(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }

        public void loadQuestionAnswer(QuestionAnswer qa, Player p) {
            if (p.getPosition().Y <= 224) {
                this.location = Location.BOTTOM;
            }
            else
                this.location = Location.TOP;

            this.question = new QuestionBox(qa.Question, location);
            String[] ara = qa.Answers;

            if (qa.QuestionType == QUESTION_TYPE.MULTIPLE_CHOICE) {
                this.answers = new A_AnswerBox[4];

                String temp;
                Random rand = new Random();
                int num;

                for (int i = 0; i < 4; i++) {
                    num = rand.Next(4);

                    temp = ara[num];
                    ara[num] = ara[i];
                    ara[i] = temp;
                }

                this.answers[0] = new MultipleChoiceBox(ara[0], qa.IsAnswerCorrect(ara[0]), this.location, MultipleChoiceBox.Letter.A);
                this.answers[1] = new MultipleChoiceBox(ara[1], qa.IsAnswerCorrect(ara[1]), this.location, MultipleChoiceBox.Letter.B);
                this.answers[2] = new MultipleChoiceBox(ara[2], qa.IsAnswerCorrect(ara[2]), this.location, MultipleChoiceBox.Letter.C);
                this.answers[3] = new MultipleChoiceBox(ara[3], qa.IsAnswerCorrect(ara[3]), this.location, MultipleChoiceBox.Letter.D);
            }
            else if (qa.QuestionType == QUESTION_TYPE.TRUE_FALSE) {
                this.answers = new A_AnswerBox[2];

                if(ara[0].ToLower().Equals("true")){
                    this.answers[0] = new TrueOrFalseBox(true, this.location, TrueOrFalseBox.ToF.TRUE);
                    this.answers[1] = new TrueOrFalseBox(false, this.location, TrueOrFalseBox.ToF.FALSE);
                }
                else if(ara[0].ToLower().Equals("false")){
                    this.answers[0] = new TrueOrFalseBox(false, this.location, TrueOrFalseBox.ToF.TRUE);
                    this.answers[1] = new TrueOrFalseBox(true, this.location, TrueOrFalseBox.ToF.FALSE);
                }
                else
                    throw new Exception("neither true or false");
            }
            else if (qa.QuestionType == QUESTION_TYPE.INPUT) {
                this.answers = new A_AnswerBox[1];

                this.answers[0] = new InputBox(ara[0], this.location);
            }

        }//end load questions method

        public Location getLocation() { return this.location; }

        public QuestionBox getQuestion() { return this.question; }

        public A_AnswerBox[] getAnswers() { return this.answers; }
    }
}
