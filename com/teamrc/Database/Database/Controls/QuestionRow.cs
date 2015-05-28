using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseSystem.Controls {
	public partial class QuestionRow : UserControl, IComparable<QuestionRow> {
		private const int DELETE_ID = 3;

		private TablePanel _owner;
		private QuestionAnswer _question;
		private bool _dbLoad;

		public QuestionRow(TablePanel p, Table t) {
			InitializeComponent();
			this._owner = p;
			this._question = t.AddNewQuestion();
			this._dbLoad = false;
			this.cmbType.SelectedIndex = 0;
			this._question.QuestionType = QUESTION_TYPE.MULTIPLE_CHOICE;
		}

		public QuestionRow(TablePanel p, QuestionAnswer qA) {
			InitializeComponent();
			this._owner = p;
			this._question = qA;
			this._dbLoad = true;
			this.toGui(this, new EventArgs());
		}

		public bool Drop {
			get { return this._question.Drop; }
		}

		public bool Loaded {
			get { return this._dbLoad; }
			set { this._dbLoad = value; }
		}

		public void toQuestion(object sender, EventArgs e) {
			this._question.QuestionType = (QUESTION_TYPE)this.cmbType.SelectedIndex;

			this.updateType();

			this._question.Question = this.txtQuestion.Text;
			this._question[0] = this.txtAns0.Text;

			if(this._question.QuestionType == QUESTION_TYPE.MULTIPLE_CHOICE) {
				this._question[1] = this.txtAns1.Text;
				this._question[2] = this.txtAns2.Text;
				this._question[3] = this.txtAns3.Text;
			} else {
				for(int x = 1; x < 4; x++) {
					this._question[x] = "";
				}
			}
		}

		public void toGui(object sender, EventArgs e) {
			this.cmbType.SelectedIndex = (int)this._question.QuestionType;

			this.updateType();

			this.txtQuestion.Text = this._question.Question;
			this.txtAns0.Text = this._question[0];
			this.txtAns1.Text = this._question[1];
			this.txtAns2.Text = this._question[2];
			this.txtAns3.Text = this._question[3];
		}

		public void updateType() {

			this._question.Drop = false;
			this.Enabled = true;
			this.txtQuestion.Enabled = true;
			this.txtAns0.Enabled = true;

			if(cmbType.SelectedIndex == 0) {
				this.txtAns1.Enabled = true;
				this.txtAns2.Enabled = true;
				this.txtAns3.Enabled = true;
			} else if(cmbType.SelectedIndex == QuestionRow.DELETE_ID) {
				if(this._dbLoad) {
					this._question.Drop = true;
					this.txtQuestion.Enabled = false;
					this.txtAns0.Enabled = false;
					this.txtAns1.Enabled = false;
					this.txtAns2.Enabled = false;
					this.txtAns3.Enabled = false;
				} else {
					this._question.deleteQuestion(false);
					this._owner.deleteRow(this);
				}
			} else {
				this.txtAns1.Enabled = false;
				this.txtAns2.Enabled = false;
				this.txtAns3.Enabled = false;
			}
		}

		public int CompareTo(QuestionRow qR) {
			return this._question.Id - qR._question.Id;
		}
	}
}
