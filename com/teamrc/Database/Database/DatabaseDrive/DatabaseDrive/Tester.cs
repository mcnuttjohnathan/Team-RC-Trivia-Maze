using DatabaseSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseDrive
{
    public partial class Tester : Form
    {
        private QuestionSource _source;
		private QuestionAnswer _question;

        public Tester()
        {
            InitializeComponent();

			this._source = new QuestionSource("./");
        }

        private QuestionAnswer GetQuestion()
        {
			return this._question;
        }

		private void randomQuestion() {
			this._question = this._source.randomQuestion();
			if(this._question != null) {
				this.updateGUI();
			} else {
				this._source.clearQuestions();
				this.randomQuestion();
			}	
		}

		private void updateGUI() {
			cmbDatabase.Text = this._question.Database;
			cmbTable.Text = this._question.Table;
			cmbQuestion.Text = this._question.Id.ToString();
			txtQuestion.Text = this._question.Question;
			txtAns0.Text = this._question[0];
			txtAns1.Text = this._question[1];
			txtAns2.Text = this._question[2];
			txtAns3.Text = this._question[3];
			cmbType.Text = Enum.GetName(typeof(QUESTION_TYPE), this._question.QuestionType);
		}

		private void btnNext_Click(object sender, EventArgs e) {
			this.randomQuestion();
		}
    }
}
