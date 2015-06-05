using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSystem
{
    public enum QUESTION_TYPE {MULTIPLE_CHOICE, TRUE_FALSE, INPUT};

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

        public QuestionAnswer(Table t)
        {
            this._database = t.GetDatabase();
            this._table = t;
            this._id = t.GetNextAvailableId();
            this._question = null;
            this._ans = new String[QuestionAnswer.MULTIPLE_CHOICE_ANSWERS];
            this._type = QUESTION_TYPE.MULTIPLE_CHOICE;
			this._imported = false;
			this._drop = false;
        }

        public String Database
        {
            get { return this._database.Name; }
        }

        public String Table
        {
            get { return this._table.Name; }
        }

        public int Id
        {
            get { return this._id; }
			set {
				if(value > -1) {
					this._id = value;
				}
			}
        }

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

        public String[] Answers
        {
            get { return this._ans; }
        }

		public bool Drop {
			get { return this._drop; }
			set { this._drop = value; }
		}

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

        public QUESTION_TYPE QuestionType
        {
            get { return this._type; }
            set { this._type = value; }
        }

		public bool Import {
			get { return this._imported; }
			set { this._imported = value; }
		}

		public void deleteQuestion(bool loaded) {
			if(loaded) {
				this._database.executeQuery(this, @"DELETE FROM '" + this._table.Name + @"' WHERE ID = " + this._id + @";");
			}

			this._table.RemoveQuestion(this);
		}

		public String ToDropQuery() {
			return @"DELETE FROM " + this._table.Name + @" WHERE ID = " + this._id + ";";
		}

        public String ToInsertQuery()
        {
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

        public String ToUpdateQuery()
        {
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

        public bool IsQuestionComplete()
        {
            return this._question != null && this._ans[0] != null && (int)this._type > -1 && (int)this._type < Enum.GetValues(typeof(QUESTION_TYPE)).Length;
        }

        public bool IsAnswerCorrect(String answer)
        {
            if (answer != null)
            {
                return this._ans[0].Equals(answer, StringComparison.CurrentCultureIgnoreCase);
            }
            else
            {
                return false;
            }
        }

		public override string ToString(){
			return this._database.Name + "." + this._table.Name + "." + this._id;
		}

		public string toPathString() {
			return this._database.DatabasePath + "|" + this._table.Name + "|" + this._id;
		}

		public int CompareTo(QuestionAnswer qA) {
			return this._id - qA._id;
		}
    }
}
