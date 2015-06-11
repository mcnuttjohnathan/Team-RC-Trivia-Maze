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
	/// <summary>
	/// A control that represents a single QuestionAnswer.
	/// </summary>
	public partial class QuestionRow : UserControl, IComparable<QuestionRow> {
		private const int DELETE_ID = 3;

		private TablePanel _owner;
		private QuestionAnswer _question;
		private bool _dbLoad;

		/// <summary>
		/// Creates a new QuestionRow with preset data. Considered new.
		/// </summary>
		/// <param name="p">The TablePanel this object will belong to.</param>
		/// <param name="t">The Table the QuestionAnswer will belong to.</param>
		public QuestionRow(TablePanel p, Table t) {
			InitializeComponent();
			this._owner = p;
			this._question = t.addNewQuestion();
			this._dbLoad = false;
			this.cmbType.SelectedIndex = 0;
			this._question.QuestionType = QUESTION_TYPE.MULTIPLE_CHOICE;
		}

		/// <summary>
		/// Creates a QuestionRow using data from an existing QuestionAnswer. Considered old.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="qA"></param>
		public QuestionRow(TablePanel p, QuestionAnswer qA) {
			InitializeComponent();
			this._owner = p;
			this._question = qA;
			this._dbLoad = true;
			this.toGui(this, new EventArgs());
		}

		/// <summary>
		/// Gets whether this QuestionRow will be deleted on save.
		/// </summary>
		public bool Drop {
			get { return this._question.Drop; }
		}

		/// <summary>
		/// Gets or set whether this QuestionRow is old or new.
		/// </summary>
		public bool Loaded {
			get { return this._dbLoad; }
			set { this._dbLoad = value; }
		}

		/// <summary>
		/// An event method that sets the QuestionAnswer's data to match the GUI's data.
		/// </summary>
		/// <param name="sender">The sender of the method.</param>
		/// <param name="e">Event arguments from the event.</param>
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

		/// <summary>
		/// An event method that sets the GUI's data to match the QuestionAnswer's data.
		/// </summary>
		/// <param name="sender">The sender of the method.</param>
		/// <param name="e">Event arguments from the event.</param>
		public void toGui(object sender, EventArgs e) {
			this.cmbType.SelectedIndex = (int)this._question.QuestionType;

			this.updateType();

			this.txtQuestion.Text = this._question.Question;
			this.txtAns0.Text = this._question[0];
			this.txtAns1.Text = this._question[1];
			this.txtAns2.Text = this._question[2];
			this.txtAns3.Text = this._question[3];
		}

		/// <summary>
		/// Updates the GUI to limit field use based on the Question Type.
		/// </summary>
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

		/// <summary>
		/// Compares the rows based on their ID.
		/// </summary>
		/// <param name="qR">The row to be compared to this object.</param>
		/// <returns>The difference between this object's ID and the passed in row's ID.</returns>
		public int CompareTo(QuestionRow qR) {
			return this._question.Id - qR._question.Id;
		}
	}
}
