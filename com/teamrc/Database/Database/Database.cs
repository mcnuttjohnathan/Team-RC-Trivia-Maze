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
    public class Database
    {
        private static int nextId = 0;

        private String _name;
        private int _id;
        private List<Table> _tables;
        private SQLiteConnection _sqlConnection;
        private SQLiteCommand _sqlCommand;
        private SQLiteDataAdapter _sqlAdapter;

        public Database(String pName) {

            if (!pName.EndsWith(".db"))
            {
                pName += ".db";
            }

            this._name = pName;
            Console.WriteLine(Path.GetFullPath(pName));

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

            this.ImportTables();
        }

        public String Name
        {
            get { return this._name; }
        }

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

        public int Count
        {
            get { return this._tables.Count; }
        }

        public QuestionAnswer randomQuestion(Random rng)
        {
            return this._tables[rng.Next(this._tables.Count)].randomQuestion(rng);
        }

        public Table AddNewTable(String tableName)
        {
            Table t = new Table(this, tableName);
            this._tables.Add(t);
            return t;
        }

        public void RemoveTable(Table t)
        {
            this._tables.Remove(t);

            this._sqlCommand.CommandText = @"DROP TABLE IF EXISTS " + t.Name + @";";
            this._sqlCommand.ExecuteNonQuery();
        }

        public String SaveDatabase()
        {
            String query = "";

            for (int x = 0; x < this._tables.Count; x++)
            {
                query += this._tables[x].SaveTable();
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

        public void ImportTables()
        {
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
                        this.AddNewTable(dT.Rows[x][tbl_name].ToString());
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
                this._tables[x].ImportTable(dT);
            }
        }

        public int GetNextAvailableId()
        {
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
