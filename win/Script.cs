using System;
using System.IO;

namespace strayex_shell_win
{
    class Script
    {
        private string Name = "";
        private string PathToFile = "";

        public Script(string Name, string PathToFile)
        {
            this.Name = Name;
            this.PathToFile = PathToFile;
        }

        public void ExecuteScript()
        {
            if(Directory.Exists(PathToFile))
            {
                using (var Reader = new StreamReader(PathToFile))
                {
                    for(int i = 0; Reader.EndOfStream; i++)
                    {
                        string temp = Reader.ReadLine();
                        // Split args and command into array:
                        string[] help = temp.Split(' ');
                        // First element of array is always command name!
                        Program.Cmd = help[0];

                        // Prepare args strings to add to shell:
                        int b = 0;
                        for (; b < 50; b++) Program.Args[b] = "";

                        for (int a = 1; a < help.Length; a++)
                        {
                            Program.Args[a - 1] = help[a];
                        }

                        // Interpret the command:
                        Program.CmdInterpreter();
                    }
                }
            }
            else
            {
                Console.WriteLine("No file called " + PathToFile + " exists!");
            }
        }
    }
}
