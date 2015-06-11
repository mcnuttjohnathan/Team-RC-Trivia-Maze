using Finisar.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSystem
{
	/// <summary>
	/// This class is a container class that represents a single database.
	/// 
	/// This object contains the SQLite objects needed to make queries to a single database.
	/// </summary>
    public class Database
    {
        private static int nextId = 0;

        private String _name;
        private int _id;
        private List<Table> _tables;
        private SQLiteConnection _sqlConnection;
        private SQLiteCommand _sqlCommand;
        private SQLiteDataAdapter _sqlAdapter;
		private String _path;

		/// <summary>
		/// The constructor will attempt to open a single database based on the path given and
		/// import data into the object.
		/// </summary>
		/// <param name="pName"></param>
        public Database(String pName) {

            if (!pName.EndsWith(".db"))
            {
                pName += ".db";
            }

            this._name = pName;
			this._path = Path.GetFullPath(pName);

            if (File.Exists(this._name))
            {
                this._sqlConnection = new SQLiteConnection("Data Source=" + this._name + ";Version=3;New=False;Compress=True");
                this._sqlAdapter = new SQLiteDataAdapter(this._sqlCommand);
            }
            else
            {
                this._sqlConnection = new SQLiteConnection("Data Source=" + this._name + ";Version=3;New=True;Compress=True");
                this._sqlAdapter = new SQLiteDataAdapter(this._sqlCommand);
            }

            try
            {
                this._sqlConnection.Open();
                this._sqlCommand = this._sqlConnection.CreateCommand();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
            }

            this._id = Database.nextId++;
            this._tables = new List<Table>();

            this.importTables();
        }

		/// <summary>
		/// Gets the name of this database.
		/// </summary>
        public String Name
        {
            get { return this._name; }
        }

		/// <summary>
		/// Gets the path of this database.
		/// </summary>
		public String DatabasePath {
			get { return this._path; }
		}

		/// <summary>
		/// Gets or sets a table contained in this object.
		/// </summary>
		/// <param name="x">Selects which table to be used.</param>
		/// <returns>A table selected by the x variable</returns>
        public Table this[int x]
        {
            get
            {
                if (x < this._tables.Count)
                {
                    return this._tables[x];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (this._tables.Contains(value))
                {
                    this._tables.Remove(value);
                    this._tables.Insert(x, value);
                }
                else
                {
                    this._tables.Insert(x, value);
                }
            }
        }

		/// <summary>
		/// Returns the number of tables contained in this database.
		/// </summary>
        public int Count
        {
            get { return this._tables.Count; }
        }

		/// <summary>
		/// Checks to see if the passed in object is in the list of tables.
		/// </summary>
		/// <param name="obj">An object to check if it is in the list tables.</param>
		/// <returns>Whether the passed in object is in the list or not.</returns>
		public bool Contains(object obj) {
			for(int x = 0; x < this._tables.Count; x++){
				for(int y = 0; y < this._tables[x].Count; y++){
					if(this._tables[x][y] == obj) {
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Randomly chooses a table and get a random question from that table.
		/// </summary>
		/// <param name="rng">The Random object to be used in the method.</param>
		/// <returns>A QuestionAnswer randomly chosen from the chosen table.</returns>
        public QuestionAnswer randomQuestion(Random rng)
        {
			if(this._tables.Count > 0) {
				return this._tables[rng.Next(this._tables.Count)].randomQuestion(rng);
			} else {
				return null;
			}
        }

		/// <summary>
		/// Executes the passed in query if and only if the soure is a contained table.
		/// </summary>
		/// <param name="source">An object expected to be in the list of table.</param>
		/// <param name="p">A string to be executed as a query.</param>
		public void executeQuery(object source, String p) {
			if(this.Contains(source)) {
				this._sqlCommand.CommandText = p;
				this._sqlCommand.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Adds a new table to the database and return the newly created table.
		/// </summary>
		/// <param name="tableName">A string to be the name of the newly created table.</param>
		/// <returns>The newly created table.</returns>
        public Table addNewTable(String tableName) {
            Table t = new Table(this, tableName);
            this._tables.Add(t);
            return t;
        }

		/// <summary>
		/// Attempts to remove the passed in table from the list.
		/// </summary>
		/// <param name="t">The table to be removed from the list.</param>
        public void removeTable(Table t) {
            this._tables.Remove(t);
        }

		/// <summary>
		/// Saves all tables and questions in the object to the actual database.
		/// This will create and drop tables and create, update, and delete questions as needed.
		/// </summary>
		/// <returns>A string that contains the query used to update the database.</returns>
        public String saveDatabase() {
            String query = "";

            for (int x = 0; x < this._tables.Count; x++)
            {
                query += this._tables[x].saveTable();
            }

            try
            {
                this._sqlCommand.CommandText = query;
                this._sqlCommand.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
            }

            return query;
        }

		/// <summary>
		/// Loads all tables in the database and creates object as needed to contain them.
		/// </summary>
        public void importTables() {
            this._sqlCommand.CommandText = @"SELECT tbl_name FROM sqlite_master WHERE type='table';";
            this._sqlCommand.ExecuteNonQuery();
            this._sqlAdapter.SelectCommand = this._sqlCommand;

            try
            {

                DataTable dT = new DataTable();

                this._sqlAdapter.Fill(dT);

                DataTableReader dtR = dT.CreateDataReader();

                int tbl_name = dtR.GetOrdinal("tbl_name");
                Console.WriteLine("tbl_name: " + tbl_name);

                if (dtR.HasRows)
                {
                    for (int x = 0; x < dT.Rows.Count; x++)
                    {
                        this.addNewTable(dT.Rows[x][tbl_name].ToString());
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            // Use table names to access other tables to verify schema and read data into Table data structure.
            for (int x = 0; x < this._tables.Count; x++)
            {
                this._sqlCommand.CommandText = @"SELECT * FROM " + this._tables[x].Name + ";";
                this._sqlCommand.ExecuteNonQuery();

                DataTable dT = new DataTable();
                this._sqlAdapter.SelectCommand = this._sqlCommand;
                this._sqlAdapter.Fill(dT);
                dT.TableName = this._tables[x].Name;
                this._tables[x].importTable(dT);
            }
        }

		/// <summary>
		/// Finds the lowest ID that isn't in used.
		/// </summary>
		/// <returns>A number that is the lowest ID that is not in use.</returns>
        public int getNextAvailableId() {
            for (int x = 0; x < this._tables.Count; x++)
            {
                if (this._tables[x].Id != x)
                {
                    return x;
                }
            }

            return this._tables.Count;
        }
    }
}
