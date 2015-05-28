using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSystem
{
    public enum TABLE_FIELDS { ID, question, ans0, ans1, ans2, ans3, type}

    public class Table
    {
        private const int COLUMN_NUM = 7;

        private Database _database;
        private String _name;
        private int _id;
        private int _initialCount;
        private bool _imported;
		private bool _drop;
        private List<QuestionAnswer> _table;

        public Table(Database d, String pName)
        {
            this._name = pName;
            this._database = d;
            this._id = d.GetNextAvailableId();
            this._table = new List<QuestionAnswer>();
            this._initialCount = 0;
            this._imported = false;
			this._drop = false;
        }

        public String Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public int Id
        {
            get { return this._id; }
        }

        public Database Owner
        {
            get { return this._database; }
            set
            {
                this._database = value;
            }
        }

        public QuestionAnswer this[int x]
        {
            get
            {
                if (x < this._table.Count)
                {
                    return this._table[x];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (this._table.Contains(value))
                {
                    this._table.Remove(value);
                    this._table.Insert(x, value);
                }
                else
                {
                    this._table.Insert(x, value);
                }
            }
        }

		public bool Drop {
			get { return this._drop; }
			set { this._drop = value; }
		}

        public int Count
        {
            get { return this._table.Count; }
        }

        public QuestionAnswer randomQuestion(Random rng)
        {
            return this._table[rng.Next(this._table.Count)];
        }

        public QuestionAnswer AddNewQuestion()
        {
            QuestionAnswer qA = new QuestionAnswer(this);
            this._table.Add(qA);
            return qA;
        }

        public void RemoveQuestion(QuestionAnswer qA)
        {
            this._table.Remove(qA);
        }

        public Database GetDatabase()
        {
            return this._database;
        }

        public int GetNextAvailableId()
        {
			this._table.Sort();

            for (int x = 0; x < this._table.Count; x++)
            {
                if (this._table[x].Id != x)
                {
                    return x;
                }
            }

            return this._table.Count;
        }

        public void ImportTable(DataTable tR)
        {
            DataTable schema = tR.CreateDataReader().GetSchemaTable();
            this._name = tR.TableName;

            if (IsValidSchema(schema))
            {
                this._imported = true;

                for (int x = 0; x < tR.Rows.Count; x++)
                {
                    QuestionAnswer qA = this.AddNewQuestion();

					qA.Import = true;

					try {
						qA.Id = Int32.Parse(tR.Rows[x][TABLE_FIELDS.ID.ToString()].ToString());
					} catch(FormatException e) {
						Console.WriteLine("Invalid Id, using next available ID instead.\n" + e.Message);
					}

                    qA.Question = tR.Rows[x][TABLE_FIELDS.question.ToString()].ToString();

                    for (int y = 0; y < qA.Answers.Length; y++)
                    {
                        qA[y] = tR.Rows[x][((TABLE_FIELDS)(y + (int)TABLE_FIELDS.ans0)).ToString()].ToString();
                    }

                    qA.QuestionType = (QUESTION_TYPE)Int32.Parse(tR.Rows[x][TABLE_FIELDS.type.ToString()].ToString());
                }

                this._initialCount = this._table.Count;
            }
        }

        public String SaveTable()
        {
            String query = "";

			if(!this._drop) {
				if(!this._imported) {
					query += @"CREATE TABLE '" + this._name + @"' ('ID' INTEGER NOT NULL UNIQUE, 'question' TEXT, 'ans0' TEXT, " +
													@"'ans1' TEXT, 'ans2' TEXT, 'ans3' TEXT, 'type' INTEGER, PRIMARY KEY(ID));";
				}

				for(int x = 0; x < this._table.Count; x++) {
					if(this._table[x].Drop) {
						query += this._table[x].ToDropQuery();
					} else {
						if(this._table[x].Import && this._imported) {
							query += this._table[x].ToUpdateQuery();
						} else if(!this._table[x].Import || !this._imported) {
							query += this._table[x].ToInsertQuery();
							this._table[x].Import = true;
						}
					}
				}
			} else {
				query = @"DROP TABLE " + this._name + @";";
			}

            return query;
        }

		public void sort() {
			this._table.Sort();
		}

        private bool IsValidSchema(DataTable tR)
        {
            for (int x = 0; x < tR.Rows.Count; x++)
            {
                if (!tR.Rows[x][0].ToString().Equals(Enum.GetNames(typeof(TABLE_FIELDS))[x]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
