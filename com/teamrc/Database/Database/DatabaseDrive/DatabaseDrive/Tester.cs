using DatabaseSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseDrive
{
    public partial class Tester : Form
    {
        private Database _database;
        private Table _table;
        private QuestionAnswer _question;

        public Tester()
        {
            InitializeComponent();

            this._database = new Database(cmbDatabase.Text);
            this._table = this.GetTable();
            this._question = this.GetQuestion();
        }

        private Table GetTable()
        {
            if (this._database.Count > 0)
            {
                return this._database[0];
            }
            else
            {
                return this._database.AddNewTable(cmbTable.Text);
            }
        }

        private QuestionAnswer GetQuestion()
        {
            if (this._table.Count > 0)
            {
                return this._table[0];
            }
            else
            {
                return this._table.AddNewQuestion();
            }
        }

        private void SaveDatabase(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine(this._database.SaveDatabase());
        }
    }
}
