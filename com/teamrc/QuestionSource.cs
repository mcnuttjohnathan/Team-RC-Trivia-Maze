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
			if(path.Contains('\n')) {
				path = path.Replace("\r", "");
				String[] data = path.Split('\n');

				for(int x = 0; x < data.Length; x++) {
					String[] line = data[x].Split('|');

					if(line.Length == 2) {
						if(File.GetLastWriteTime(line[0]).ToString().Equals(line[1])){
							Database d = new Database(line[0]);
							this.addDatabase(d);
						} else {
							throw new InvalidDataException("A database associated with this save file has been changed.");
						}
					} else if(line.Length == 3) {
						Database d = null;

						for(int y = 0; y < this._databases.Count; y++) {
							if(this._databases[y].DatabasePath.Equals(line[0])) {
								d = this._databases[y];
							}
						}

						if(d != null) {
							for(int y = 0; y < d.Count; y++) {
								if(d[y].Name.Equals(line[1])) {
									try {
										int id = Int32.Parse(line[2]);

										if(id > -1 && id < d[y].Count) {
											this._usedQuestions.Add(d[y][id]);
										}
									} catch(Exception e) {
										Console.WriteLine(e.Message);
									}
								}
							}
						}
					}
				}
			} else {
				this.addDatabasesFrom(path);
			}
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
			if(this._databases.Count > 0) {
				Random rng = new Random();
				int sum = this.QuestionCount;

				do {
					QuestionAnswer qA = this._databases[rng.Next(this._databases.Count)].randomQuestion(rng);

					if(!this._usedQuestions.Contains(qA) && qA != null) {
						this._usedQuestions.Add(qA);
						return qA;
					}
				} while(sum != this._usedQuestions.Count);
			}

            return null;
        }

		public void clearQuestions() {
			this._usedQuestions.Clear();
		}

		public String toSave() {
			String data = "";

			for(int x = 0; x < this._databases.Count; x++) {
				data += this._databases[x].DatabasePath + "|" + File.GetLastWriteTime(Path.GetFullPath(this._databases[x].DatabasePath)) + "\n";
			}

			for(int x = 0; x < this._usedQuestions.Count; x++) {
				data += this._usedQuestions[x].toPathString() + "\n";
			}

			return data;
		}
    }
}
