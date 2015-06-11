using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSystem
{
    public enum TABLE_FIELDS { ID, question, ans0, ans1, ans2, ans3, type}

	/// <summary>
	/// Author: Theodore Bickham
	/// The class is a container class representing a table inside a database.
	/// </summary>
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

		/// <summary>
		/// This constructor creates the object and associates it with the passed in database.
		/// </summary>
		/// <param name="d">The database that this table will be associated with.</param>
		/// <param name="pName">The name that will be given to this table.</param>
        public Table(Database d, String pName)
        {
            this._name = pName;
            this._database = d;
            this._id = d.getNextAvailableId();
            this._table = new List<QuestionAnswer>();
            this._initialCount = 0;
            this._imported = false;
			this._drop = false;
        }

		/// <summary>
		/// Gets or sets the name of this table.
		/// </summary>
        public String Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

		/// <summary>
		/// Gets the ID of this table.
		/// </summary>
        public int Id
        {
            get { return this._id; }
        }

		/// <summary>
		/// Gets or sets the database associated with this table.
		/// </summary>
        public Database Owner
        {
            get { return this._database; }
            set
            {
                this._database = value;
            }
        }

		/// <summary>
		/// Gets or sets a QuestionAnswer within this table.
		/// </summary>
		/// <param name="x">Selects which QuestionAnswer is to be used.</param>
		/// <returns>The QuestionAnswer selected by the x variable.</returns>
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

		/// <summary>
		/// Gets or sets whether this table is to be dropped on save.
		/// </summary>
		public bool Drop {
			get { return this._drop; }
			set { this._drop = value; }
		}

		/// <summary>
		/// Gets the number of QuestionAnswers in this table.
		/// </summary>
        public int Count
        {
            get { return this._table.Count; }
        }

		/// <summary>
		/// Returns a random QuestionAnswer from this table.
		/// </summary>
		/// <param name="rng">The Random object to be used for the method.</param>
		/// <returns>The QuestionAnswer randomly chosen from this table.</returns>
        public QuestionAnswer randomQuestion(Random rng) {
			if(this._table.Count > 0) {
				return this._table[rng.Next(this._table.Count)];
			} else {
				return null;
			}
        }

		/// <summary>
		/// Adds a new QuestionAnswer to this table and returns it to the caller.
		/// </summary>
		/// <returns>The newly created QuestionAnswer.</returns>
        public QuestionAnswer addNewQuestion() {
            QuestionAnswer qA = new QuestionAnswer(this);
            this._table.Add(qA);
            return qA;
        }

		/// <summary>
		/// Removes the passed in QuestionAnswer from the table.
		/// </summary>
		/// <param name="qA">The QuestionAnswer to be removed from the table.</param>
        public void removeQuestion(QuestionAnswer qA) {
            this._table.Remove(qA);
        }

		/// <summary>
		/// Returns the database that is associated with this table.
		/// </summary>
		/// <returns>The database associated with this table.</returns>
        public Database getDatabase() {
            return this._database;
        }

		/// <summary>
		/// Finds the lowest ID that isn't being used by a QuestionAnswer
		/// </summary>
		/// <returns>The lowest ID that isn't in use.</returns>
        public int getNextAvailableId() {
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

		/// <summary>
		/// Import the data from the passed in DataTable into the table.
		/// </summary>
		/// <param name="tR">The DataTable which to import data from.</param>
        public void importTable(DataTable tR) {
            DataTable schema = tR.CreateDataReader().GetSchemaTable();
            this._name = tR.TableName;

            if (this.isValidSchema(schema))
            {
                this._imported = true;

                for (int x = 0; x < tR.Rows.Count; x++)
                {
                    QuestionAnswer qA = this.addNewQuestion();

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

		/// <summary>
		/// Returns a string that, when executed as a query, would update the table in the database.
		/// </summary>
		/// <returns>A string that, when executed as a query, would update the table in the database.</returns>
        public String saveTable() {
            String query = "";

			if(!this._drop) {
				if(!this._imported) {
					query += @"CREATE TABLE '" + this._name + @"' ('ID' INTEGER NOT NULL UNIQUE, 'question' TEXT(140), 'ans0' TEXT(24), " +
													@"'ans1' TEXT(24), 'ans2' TEXT(24), 'ans3' TEXT(24), 'type' INTEGER, PRIMARY KEY(ID));";
					this._imported = true;
				}

				for(int x = 0; x < this._table.Count; x++) {
					if(this._table[x].Drop) {
						query += this._table[x].toDropQuery();
					} else {
						if(this._table[x].Import && this._imported) {
							query += this._table[x].toUpdateQuery();
						} else if(!this._table[x].Import || !this._imported) {
							query += this._table[x].toInsertQuery();
							this._table[x].Import = true;
						}
					}
				}
			} else {
				query = @"DROP TABLE " + this._name + @";";
			}

            return query;
        }

		/// <summary>
		/// Sorts the QuestionAnswers in this table by ID.
		/// </summary>
		public void sort() {
			this._table.Sort();
		}

		/// <summary>
		/// Private method.
		/// </summary>
        private bool isValidSchema(DataTable tR) {
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
