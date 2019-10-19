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

        // Executes the given script:
        public void ExecuteScript()
        {
            // Checks if this script exists:
            if(File.Exists(PathToFile + '\\' + Name))
            {
                // Yes, it exists! Let's read commands from it:
                using (var Reader = new StreamReader(PathToFile + '\\' + Name))
                {
                    // For loop to execute each command from file, like in Main function of Program class:
                    for(int i = 0; !Reader.EndOfStream; i++)
                    {
                        // Get next command form script:
                        string temp = Reader.ReadLine();
                        // If line starts with #, it's comment, so execution will ignore it:
                        if (temp.StartsWith("#")) continue;
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

                    return;
                }
            }
            else
            {
                Console.WriteLine("No script called " + Name + " exists!");
                return;
            }
        }
    }
}
