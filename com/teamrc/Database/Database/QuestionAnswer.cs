using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSystem
{
    public enum QUESTION_TYPE {MULTIPLE_CHOICE, TRUE_FALSE, INPUT};

	/// <summary>
	/// Author: Theodore Bickham
	/// This class is a container class for storing and manipulating Questions and their corresponding answers.
	/// </summary>

    public class QuestionAnswer : IComparable<QuestionAnswer>
    {
        private const int MULTIPLE_CHOICE_ANSWERS = 4;

        private Database _database;
        private Table _table;
        private int _id;
        private String _question;
        private String[] _ans;
        private QUESTION_TYPE _type;

		private bool _imported;
		private bool _drop;


		/// <summary>
		/// This constructor creates the QuestionAnswer and associates the object with the passed in table.
		/// </summary>
		/// <param name="t">The table the QuestionAnswer is to be contained in.</param>
        public QuestionAnswer(Table t)
        {
            this._database = t.getDatabase();
            this._table = t;
            this._id = t.getNextAvailableId();
            this._question = null;
            this._ans = new String[QuestionAnswer.MULTIPLE_CHOICE_ANSWERS];
            this._type = QUESTION_TYPE.MULTIPLE_CHOICE;
			this._imported = false;
			this._drop = false;
        }

		/// <summary>
		/// Gets the name of the Database this object is associated with.
		/// </summary>
        public String Database
        {
            get { return this._database.Name; }
        }

		/// <summary>
		/// Gets the name of the Table this object is associated with.
		/// </summary>
        public String Table
        {
            get { return this._table.Name; }
        }

		/// <summary>
		/// Gets or sets the ID that is assigned to this object.
		/// </summary>
        public int Id
        {
            get { return this._id; }
			set {
				if(value > -1) {
					this._id = value;
				}
			}
        }

		/// <summary>
		/// Gets or sets the question that is contained in this object.
		/// </summary>
        public String Question
        {
            get {
				String q = this._question;
				q = q.Replace("''", "'");
				return q; 
			}
            set { 
                this._question = value;
				this._question = this._question.Replace("''", "'");
                this._question = this._question.Replace("'", "''");
            }
        }

		/// <summary>
		/// Gets an array containing the answers contained in this object.
		/// </summary>
        public String[] Answers
        {
            get { return this._ans; }
        }

		/// <summary>
		/// Gets and sets whether this object is to be deleted on save or not.
		/// </summary>
		public bool Drop {
			get { return this._drop; }
			set { this._drop = value; }
		}

		/// <summary>
		/// Gets or sets a single answer from this object.
		/// </summary>
		/// <param name="x">Determines which answer to use.</param>
		/// <returns>The answer that is selected by the x variable</returns>
        public String this[int x]
        {
            get
            {
                if (x < QuestionAnswer.MULTIPLE_CHOICE_ANSWERS && x > -1)
                {
                    return this._ans[x];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (x > -1 && x < QuestionAnswer.MULTIPLE_CHOICE_ANSWERS)
                {
                    this._ans[x] = value;
                    this._ans[x] = this._ans[x].Replace("'", "''");
                }
            }
        }

		/// <summary>
		/// Gets or sets the question type of this object.
		/// </summary>
        public QUESTION_TYPE QuestionType
        {
            get { return this._type; }
            set { this._type = value; }
        }

		/// <summary>
		/// Gets or sets whether this object has an existing record in it's database and table.
		/// </summary>
		public bool Import {
			get { return this._imported; }
			set { this._imported = value; }
		}

		/// <summary>
		/// Removes this object from it's parent table and removes the question from the database if it is there.
		/// </summary>
		/// <param name="loaded">Whether the object has a record in the database or not.</param>
		public void deleteQuestion(bool loaded) {
			if(loaded) {
				this._database.executeQuery(this, @"DELETE FROM '" + this._table.Name + @"' WHERE ID = " + this._id + @";");
			}

			this._table.removeQuestion(this);
		}

		/// <summary>
		/// Returns a string that, if executed as a query, would remove the question from the database.
		/// </summary>
		/// <returns>A string that, if executed as a query, would remove the question from the database.</returns>
		public String toDropQuery() {
			return @"DELETE FROM " + this._table.Name + @" WHERE ID = " + this._id + ";";
		}

		/// <summary>
		/// Returns a string that, if executed as a query, would add the question to the database.
		/// </summary>
		/// <returns>A string that, if executed as a query, would add the question to the database.</returns>
        public String toInsertQuery() {
            String query = @"INSERT INTO " + this._table.Name + @" (ID, question, ans0, ans1, ans2, ans3, type) ";
            query += @"VALUES (" + this._id + @",";

            if (this._question == null)
            {
                query += @"null,";
            }
            else
            {
                query += @"'" + this._question + @"',";
            }

            for (int x = 0; x < this._ans.Length; x++)
            {
                if (this._ans[x] == null)
                {
                    query += @"null,";
                }
                else
                {
                    query += @"'" + this._ans[x] + @"',";
                }
            }

            query += (int)this._type + @");";

            return query;
        }

		/// <summary>
		/// Returns a string that, if executed as a query, would update the question in the database.
		/// </summary>
		/// <returns>A string that, if executed as a query, would update the question in the database.</returns>
        public String toUpdateQuery() {
            String query = @"UPDATE " + this._table.Name + " SET question = ";

            if (this._question == null)
            {
                query += @"null";
            }
            else
            {
                query += @"'" + this._question + @"'";
            }

            for (int x = 0; x < this._ans.Length; x++)
            {
                if (this._ans[x] == null)
                {
                    query += @", ans" + x + @" = null";
                }
                else
                {
                    query += @", ans" + x + @" = '" + this._ans[x] + @"'";
                }
            }

            query += @", type = " + (int)this._type;
            query += @" WHERE ID = " + this._id + @";";

            return query;
        }

		/// <summary>
		/// Returns whether the question has all essential variables has data or not.
		/// </summary>
		/// <returns>A boolean that represents whether all essential variables has data or not.</returns>
        public bool isQuestionComplete() {
            return this._question != null && this._ans[0] != null && (int)this._type > -1 && (int)this._type < Enum.GetValues(typeof(QUESTION_TYPE)).Length;
        }

		/// <summary>
		/// Returns whether the passed in answer matches the correct answer or not.
		/// </summary>
		/// <param name="answer">A string to check to see if it is the correct answer.</param>
		/// <returns>A boolean that represents whether the answer is correct or not.</returns>
        public bool isAnswerCorrect(String answer) {
            if (answer != null)
            {
                return this._ans[0].Equals(answer, StringComparison.CurrentCultureIgnoreCase);
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// Returns a string contained the name of the database, table, and id of this object.
		/// </summary>
		/// <returns>A string that contains the name of the database, table, and id of this object.</returns>
		public override string ToString(){
			return this._database.Name + "." + this._table.Name + "." + this._id;
		}

		/// <summary>
		/// Returns a string for the purposes of saving the game.
		/// </summary>
		/// <returns>A string to refers this question.</returns>
		public string toPathString() {
			return this._database.DatabasePath + "|" + this._table.Name + "|" + this._id;
		}

		/// <summary>
		/// Compares this object to the passed in object based on ID.
		/// </summary>
		/// <param name="qA">The object to compare to this object.</param>
		/// <returns>The difference between this object's ID and the passed in object's ID.</returns>
		public int CompareTo(QuestionAnswer qA) {
			return this._id - qA._id;
		}
    }
}
