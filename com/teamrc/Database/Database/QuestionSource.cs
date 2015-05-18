using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSystem
{
    public class QuestionSource
    {
        private List<Database> _databases;
        private List<QuestionAnswer> _usedQuestions;

        public QuestionSource()
        {
            this._databases = new List<Database>();
            this._usedQuestions = new List<QuestionAnswer>();
        }

        public QuestionSource(String path)
            : this()
        {
            this.addDatabasesFrom(path);
        }

        public QuestionAnswer this[int x]
        {
            get
            {
                if (x < this._usedQuestions.Count)
                {
                    return this._usedQuestions[x];
                }
                else
                {
                    return null;
                }
            }
        }

        public int QuestionCount
        {
            get
            {
                int sum = 0;

                for (int x = 0; x < this._databases.Count; x++)
                {
                    Database d = this._databases[x];

                    for (int y = 0; y < d.Count; y++)
                    {
                        sum += d[y].Count;
                    }
                }

                return sum;
            }
        }

        public void addDatabase(Database d)
        {
            if (!this._databases.Contains(d))
            {
                this._databases.Add(d);
            }
        }

        public void addDatabasesFrom(String p)
        {
            if (Directory.Exists(p))
            {
                String[] files = Directory.GetFiles(p);

                for (int x = 0; x < files.Length; x++)
                {
                    if (files[x].EndsWith(".db") || files[x].EndsWith(".sqlite"))
                    {
                        this.addDatabase(new Database(files[x]));
                    }
                }
            }
        }

        public void removeDatabase(Database d)
        {
            this._databases.Remove(d);
        }

        public QuestionAnswer randomQuestion()
        {
            Random rng = new Random();
            int sum = this.QuestionCount;

            do
            {
                QuestionAnswer qA = this._databases[rng.Next(this._databases.Count)].randomQuestion(rng);

                if (!this._usedQuestions.Contains(qA))
                {
                    this._usedQuestions.Add(qA);
                    return qA;
                }
            } while (sum != this._usedQuestions.Count);

            return null;
        }

		public void clearQuestions() {
			this._usedQuestions.Clear();
		}
    }
}
