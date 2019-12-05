/*
    Strayex Shell
    Copyright © 2019 Daniel Strayker Nowak
    All rights reserved
 */

using System;
using System.IO;

namespace StrayexShellWindows
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
        public bool ExecuteScript()
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
                        // If line is empty, also skip it:
                        if (temp == "") continue;
                        // Split args and command into array:
                        string[] help = temp.Split(' ');
                        // First element of array is always command name!
                        Program.Cmd = help[0];

                        // Prepare args strings to add to shell:
                        Program.Args = new string[50];

                        for (int a = 1; a < help.Length; a++)
                        {
                            Program.Args[a - 1] = help[a];
                        }

                        // Command class instance:
                        var cmdc = new ShellCommand(Program.Cmd, Program.Args);

                        // Interpret the command:
                        var IfInterpreted = cmdc.Interpret();

                        if (IfInterpreted == false) Console.WriteLine("Line {0}. Command or program not found!", i);
                    }

                    return true;
                }
            }
            else
            {
                Console.WriteLine("No script called " + Name + " exists!");
                return false;
            }
        }
    }
}
