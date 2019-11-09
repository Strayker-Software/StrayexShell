using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace strayex_shell_win
{
    public class ShellAddons
    {
        private string[] FileTypes { get; set; }
        private string[] ProgramTypes { get; set; }

        // Returns new string form given, starting from given index to given char:
        public string SubStringChar(string Value, int Index, char LastChar)
        {
            // Where is last char in the string?
            int IndexOfLastChar = 0;
            for (; Value[IndexOfLastChar] != LastChar; IndexOfLastChar++) ;

            // Prepare memory for new string:
            var NewString = new StringBuilder
            {
                Length = IndexOfLastChar
            };

            // Create new string:
            int j = 0;
            for (int i = Index + 1; i < IndexOfLastChar; i++)
            {
                NewString[j] += Value[i];
                j++;
            }

            return NewString.ToString();
        }

        // Counts, how much times the given char appears in string, maybe will be usefull in future:
        public int CountChar(char x, string y)
        {
            int Long = y.Length;
            int Counter = 0;

            for (int i = 0; i < Long; i++) if (y[i] == x) Counter++;

            return Counter;
        }

        // Checks file extension, runs specific program or returns false, if can't recognise file:
        public bool DiscussFile(string FileName)
        {
            // Is it only executable filename or filename with extenstion?
            if (FileName.Contains("."))
            {
                // With extension!

                try
                {
                    Process.Start(Program.ShellPath + '\\' + FileName, Program.Args.ToString());
                }
                catch (Exception a)
                {
                    Console.WriteLine("Can't execute program: " + a.Message);
                    throw new CantExecuteProgramException(a.Message);
                }

                return true;
            }
            else if(!FileName.Contains("."))
            {
                // Just filename! So shell have to find app in workdir:
                string[] Apps = Directory.GetFileSystemEntries(Program.ShellPath);

                for (int i = 0; i < Apps.Length; i++)
                {
                    if (Apps[i].Contains(Program.ShellPath + '\\' + FileName))
                    {
                        try
                        {
                            Process.Start(Apps[i], Program.Args.ToString());
                        }
                        catch (Exception a)
                        {
                            Console.WriteLine("Can't execute program: " + a.Message);
                            throw new CantExecuteProgramException(a.Message);
                        }

                        return true;
                    }
                }

                return false;
            }
            else return false;
        }

        // Reads the file types to array:
        public bool LoadFileTypes(string PathToFile)
        {
            if(PathToFile != null && PathToFile != "" && File.Exists(PathToFile))
            {
                using (var reader = new StreamReader(PathToFile))
                {
                    string x = reader.ReadToEnd();
                    FileTypes = x.Split('\n');
                }
            }
            else throw new ArgumentException();

            return false;
        }

        // Reads the programs to array:
        public bool LoadPrograms(string PathToFile)
        {
            if (PathToFile != null && PathToFile != "" && File.Exists(PathToFile))
            {
                using (var reader = new StreamReader(PathToFile))
                {
                    string x = reader.ReadToEnd();
                    ProgramTypes = x.Split('\n');
                }
            }
            else throw new ArgumentException();

            return false;
        }

        // Checks, if there is the same amount of file types and programs:
        public bool CheckCompatibility()
        {
            if (FileTypes.Length == ProgramTypes.Length) return true;
            else return false;
        }
    }
}
