using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSystem
{
	/// <summary>
	/// This class is a simpleton class that loads databases and tracks use of questions.
	/// </summary>
    public class QuestionSource
    {
        private List<Database> _databases;
        private List<QuestionAnswer> _usedQuestions;

		/// <summary>
		/// Creates a source with no databases loaded.
		/// </summary>
        public QuestionSource() {
            this._databases = new List<Database>();
            this._usedQuestions = new List<QuestionAnswer>();
        }

		/// <summary>
		/// Creates a source with database loaded from the path. Also used to load a saved source.
		/// 
		/// Throws an exception if loading a source encounters an edited database.
		/// </summary>
		/// <param name="path">A string that represents a folder path or save data from a source.</param>
        public QuestionSource(String path)
            : this() {
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

		/// <summary>
		/// Gets an used QuestionAnswer
		/// </summary>
		/// <param name="x">Selects which QuestionAnswer to receive.</param>
		/// <returns>A QuestionAnswer that has been used.</returns>
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

		/// <summary>
		/// Gets the number of questions stored in all loaded databases.
		/// </summary>
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

		/// <summary>
		/// Adds a loaded database to the source.
		/// </summary>
		/// <param name="d">The database to be added to the source.</param>
        public void addDatabase(Database d) {
            if (!this._databases.Contains(d))
            {
                this._databases.Add(d);
            }
        }

		/// <summary>
		/// Loads all databases in the passed in path.
		/// </summary>
		/// <param name="p">A string that represents a path to a folder.</param>
        public void addDatabasesFrom(String p) {
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

		/// <summary>
		/// Removes the passed in database from the source.
		/// </summary>
		/// <param name="d"></param>
        public void removeDatabase(Database d) {
            this._databases.Remove(d);
        }

		/// <summary>
		/// Gets a random question from the databases stored in the source.
		/// 
		/// The pulled random question will be considered used.
		/// </summary>
		/// <returns>A QuestionAnswer to hasn't been used.</returns>
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

		/// <summary>
		/// Removes all QuestionAnswers from the used list, allowing them to be randomly pulled again.
		/// </summary>
		public void clearQuestions() {
			this._usedQuestions.Clear();
		}

		/// <summary>
		/// Creates a string that can be used with this(string) to recreate this source.
		/// </summary>
		/// <returns>A string that can be used with this(string) to recreate this source.</returns>
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
