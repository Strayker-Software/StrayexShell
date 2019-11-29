using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StrayexShellWindows
{
    public class ShellCommand
    {
        private string Command { get; set; } // Holds command,
        private string[] Args { get; set; } // Holds arguments,
        private ShellAddons Addon { get; set; } // Handler for helpful shell functions,

        // Constructor:
        public ShellCommand(string Command, string[] Args)
        {
            if(Command != null && Command != "" && Args != null)
            {
                this.Command = Command;
                this.Args = Args;
            }
        }

        // Is command a call to execute a script?
        private bool IsScript()
        {
            if(Command.StartsWith("."))
            {
                var ScriptName = Command.Substring(1);

                var Exec = new Script(ScriptName, Program.ShellPath);

                if (Exec.ExecuteScript()) return true;
                else return false;
            }

            return false;
        }

        // Is command a call to execute a binary file?
        private bool IsExecutable()
        {
            string[] Apps = Directory.GetFiles(Program.ShellPath);

            for (int i = 0; i < Apps.Length; i++)
            {
                if (Program.ShellPath + '\\' + Command == Apps[i])
                {
                    // Start given process:

                    var apk = new Process();
                    apk.StartInfo.FileName = Apps[i];

                    // If there's input, add it to process:
                    string Temp = "";
                    for (int j = 0; j < Args.Length; j++) Temp += Args[j];
                    apk.StartInfo.Arguments = Temp;

                    // Redirect streams to shell:
                    apk.StartInfo.RedirectStandardError = true;
                    apk.StartInfo.RedirectStandardInput = true;
                    apk.StartInfo.RedirectStandardOutput = true;
                    apk.StartInfo.UseShellExecute = false;

                    try
                    {
                        apk.Start();
                    }
                    catch (Exception a)
                    {
                        // If there's error, print it:
                        Console.WriteLine("Error trying execute given command: " + a.Message);
                        return false;
                    }

                    apk.WaitForExit();

                    // If there's output, print it:
                    string output = apk.StandardOutput.ReadToEnd();
                    if (output != "") Console.Write(output);

                    return true;
                }
            }

            return false;
        }

        // Is command a call to open a file?
        private bool IsFileToOpen()
        {
            // First, check input data:
            if (Addon.CheckCompatibility())
            {
                // Second, check file to run:
                if (Addon.DiscussFile(Command)) return true;
            }
            else throw new InvalidOperationException();

            return false;
        }

        // More complex commands are in functions:
        private bool Echo()
        {
            // Writes args on screen:

            // Check if user want to turn on or off the command:
            if (Args[0] == "/on")
            {
                // User want to turn on echo!
                Program.IfEcho = true;
                return true;
            }
            else if (Args[0] == "/off")
            {
                // User want to turn off echo!
                Program.IfEcho = false;
                return true;
            }
            else if (Args[0] == "/help") // Check if user want help for printing in shell:
            {
                Console.WriteLine("Strayex Shell Echo Command");
                Console.WriteLine("This command prints out on the console the given args!");
                Console.WriteLine("- echo - prints empty line,");
                Console.WriteLine("- echo <arg> ... - prints given args on screen, separated by spaces,");
                Console.WriteLine("- echo $<varname> ... - prints, in given place, value from shell variable, can be mobile,");
                Console.WriteLine("- echo /<on><off> - switch on or off printing command,");
                return true;
            }

            // Is echo on? Check it out!
            if (!Program.IfEcho)
            {
                // No, it's not on. So printing on screen now is denied!
                Console.WriteLine("Echo command off!");
                return true;
            }
            // Yes! Echo is on! Let's print args!

            // Prepare string:
            string ArgsString = "";
            for (int i = 0; i < Args.Length; i++)
            {
                if (Args[i] != null && Args[i + 1] != null) ArgsString = ArgsString + Args[i] + " ";
                else if (Args[i] != null && Args[i + 1] == null) ArgsString = ArgsString + Args[i] + "\0";
                else if (Args[0] == null)
                {
                    Console.WriteLine();
                    return true;
                }
            }

            // Check every letter of string:
            for (int i = 0; i < ArgsString.Length; i++)
            {
                // Check if argument is variable, then print it's value:
                if (ArgsString[i] == '$')
                {
                    string VarName = "";
                    var VarNameBuilder = new StringBuilder();

                    // Take var name from input:
                    int a = 0;
                    for (int k = i + 1; (ArgsString[k] != ' ') && (ArgsString[k] != '\0'); k++)
                    {
                        VarNameBuilder.Length++;
                        VarNameBuilder[a] = ArgsString[k];
                        VarName = VarNameBuilder.ToString();
                        a++;
                    }

                    // Look for variable in shell:
                    for (int j = 0; j < Program.Vars.Length; j++)
                    {
                        if (Program.Vars[j] != null && Program.Vars[j].GetName() == VarName)
                        {
                            // Here it is! Let's check it's type:
                            if (Program.Vars[j].CheckType() == "str") Console.Write(Program.Vars[j].GetVarString());
                            else Console.Write(Program.Vars[j].GetVarInt());

                            // Also, we have to skip the call of var in arg:
                            ArgsString = ArgsString.Remove(i, VarName.Length);
                        }
                    }
                }
                else
                {
                    // There's no var, print it:
                    Console.Write(ArgsString[i]);
                }
            }
            if (Args.Length > 1) Console.WriteLine();

            return true;
        }

        private bool Color()
        {
            // Change colors of active shell session:

            if (Args[0] == null)
            {
                Console.WriteLine("No arguments given! Type `color help` for info!");
                return true;
            }

            if (Args[0] == "help")
            {
                Console.WriteLine("Strayex Shell Color Config");
                Console.WriteLine("This command changes colors of terminal.");
                Console.WriteLine("Use `color reset` to reload default terminal settings!");
                Console.WriteLine("Available colors set:");
                Console.WriteLine("- Background - Black, Blue, Green, White,");
                Console.WriteLine("- Font - Black, Blue, Green, White,");
                Console.WriteLine("Use `color <background> <font>` to change settings.");
                Console.WriteLine("If you will provide one argument, Strayex Shell will interpret it as background color change!");
                return true;
            }
            else if (Args[0] == "reset")
            {
                Console.ResetColor();
                Console.WriteLine("Reset of color settings!");
                return true;
            }

            // If user wants rainbow in shell...
            if (Args[2] != null)
            {
                Console.WriteLine("Too many colors given!");
                return false;
            }
            else if (Args[0] != null && Args[1] != null)
            { // If background and font color are provided:
              // Check background color:
                switch (Args[0].ToLower())
                {
                    case "black":
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;

                    case "blue":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;

                    case "green":
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;

                    case "white":
                        Console.BackgroundColor = ConsoleColor.White;
                        break;

                    default:
                        Console.WriteLine("Can't determine color: " + Args[0]);
                        break;
                }

                // Check font color:
                switch (Args[1].ToLower())
                {
                    case "black":
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;

                    case "blue":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;

                    case "green":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;

                    case "white":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    default:
                        Console.WriteLine("Can't determine color: " + Args[0]);
                        break;
                }
            }
            else if (Args[0] != null)
            { // Check background color only:
                switch (Args[0].ToLower())
                {
                    case "black":
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;

                    case "blue":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;

                    case "green":
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;

                    case "white":
                        Console.BackgroundColor = ConsoleColor.White;
                        break;

                    default:
                        Console.WriteLine("Can't determine color: " + Args[0]);
                        break;
                }
            }

            return true;
        }

        private bool Set()
        {
            if (Args[0] == "help")
            { // Show help:
                Console.WriteLine("Strayex Shell variables command");
                Console.WriteLine("- set help - shows this hints,");
                Console.WriteLine("- set <name> <value> - creates new var in active shell session,");
                Console.WriteLine("- set ! <name> - deletes created var,");
                Console.WriteLine("- set list - shows list of all vars in active shell session,");
                Console.WriteLine("Shell recognise two types of vars - string and integer.");
                Console.WriteLine("When declaring integer var before <value> data place dot '.'.");
                Console.WriteLine("For example: declaring string var: 'set SomeText Hello!',");
                Console.WriteLine("declaring integer var: 'set SomeInt .123'");
                return true;
            }
            else if (Args[0] == null)
            { // None args:
                Console.WriteLine("No arguments! Check 'set help' for instructions!");
                return true;
            }
            else if (Args[0] == "!" && Args[1] != null)
            { // Deletes var:
                bool IfFound = false;

                for (int i = 0; i < Program.Vars.Length; i++)
                {
                    // Be sure if there's any var:
                    if (Program.Vars[i] != null)
                    {
                        // Check if it's var shell is looking for:
                        if (Args[1] == Program.Vars[i].GetName())
                        {
                            IfFound = true;
                            for (int a = i; a < Program.Vars.Length - 1; a++)
                            {
                                Program.Vars[a] = Program.Vars[a + 1];
                            }
                            return true;
                        }
                    }
                }

                if (!IfFound)
                {
                    Console.WriteLine("No variable named " + Args[1] + " found!");
                    return false;
                }

                return true;
            }
            else if (Args[0] != null && Args[1] != null)
            { // Sets new var:
                ShellVariable a;

                // There can't be more than one var with given name!
                for (int i = 0; i < Program.Vars.Length; i++)
                {
                    if (Program.Vars[i] != null && Program.Vars[i].GetName() == Args[0])
                    {
                        Console.WriteLine("Error - there is variable called " + Args[0] + "! Delete it first!");
                        return false;
                    }
                    else i++;
                }

                // If value is integer:
                if (Args[1].StartsWith("."))
                {
                    Args[1] = Args[1].Substring(1);
                    a = new ShellVariable(Args[0], Convert.ToInt32(Args[1]));
                }
                else
                { // If value is string:
                    a = new ShellVariable(Args[0], Args[1]);
                }

                // Save created var in shell:
                for (int i = 0; i < Program.Vars.Length; i++)
                {
                    if (Program.Vars[i] == null)
                    {
                        Program.Vars[i] = a;
                        return true;
                    }
                }
            }
            else if (Args[0] == "list")
            { // Shows list of vars:
              // Checks if there are any vars in active shell session:
                if (Program.Vars[0] != null)
                {
                    Console.WriteLine("Active shell session variables:");
                    Console.WriteLine("| Name | Value | Type |");
                    for (int i = 0; i < Program.Vars.Length; i++)
                    {
                        if (Program.Vars[i] != null)
                        {
                            if (Program.Vars[i].CheckType() == "str")
                            {
                                Console.WriteLine(Program.Vars[i].GetName() + " - " + Program.Vars[i].GetVarString() + " - String");
                            }
                            else
                            {
                                Console.WriteLine(Program.Vars[i].GetName() + " - " + Program.Vars[i].GetVarInt() + " - Integer");
                            }
                        }
                    }

                    return true;
                }
                else
                { // Nothing here!
                    Console.WriteLine("There are no variables in active shell session!");
                    return true;
                }
            }
            else Console.WriteLine("Wrong command arguments! Check 'set help'!");

            return false;
        }

        // Is command built in the shell?
        public bool Interpret()
        {
            // Build-in command?
            switch (Command)
            {
                case "help":
                    Console.WriteLine();
                    Console.WriteLine("Strayex Shell Command list:");
                    Console.WriteLine("- help - shows this list,");
                    Console.WriteLine("- hello - make shell to say hello to you,");
                    Console.WriteLine("- clear - clears consol's screen,");
                    Console.WriteLine("- echo - write information on screen,");
                    Console.WriteLine("- cd - changes active directory,");
                    Console.WriteLine("- color - change colors of active shell session,");
                    Console.WriteLine("- set - shows list of shell variables and sets new,");
                    Console.WriteLine("- exit - close shell,");
                    Console.WriteLine();
                    return true;

                case "hello":
                    // Say hi to user :)
                    Console.WriteLine("Hello user! :D");
                    return true;

                case "clear":
                    // Clear console,
                    Console.Clear();
                    return true;

                case "echo":
                    if (Echo()) return true;
                    else return false;

                case "cd":
                    // "cd" takes only one parameter and checks, if it exists in file system!
                    if ((Args != null) && Directory.Exists(Args[0])) Program.ShellPath = Args[0];
                    else
                    {
                        Console.WriteLine("Can't change directory, wrong argument!");
                        return true;
                    }
                    Console.Title = Program.ShellPath + " - Strayex Shell";
                    return true;

                case "color":
                    if (Color()) return true;
                    else return false;

                case "set":
                    if (Set()) return true;
                    else return false;

                case "exit":
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }

            // Script?
            if (IsScript())
            {
                Console.WriteLine("Script {0} executed!", Command.Substring(1));
                return true;
            }

            // File to open?
            try
            {
                IsFileToOpen();
            }
            catch (Exception a)
            {
                if(Equals(a, new InvalidOperationException()))
                {
                    Console.WriteLine("Can't execute third-party app: " + a.Message);
                    return false;
                }
            }

            // Executable binary?
            if (IsExecutable())
            {
                Console.WriteLine("Program {0} executed!", Command);
                return true;
            }

            return false;
        }
    }
}
