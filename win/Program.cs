/*
    Strayex Shell for Windows
    Copyright © 2019 Daniel Strayker Nowak
    All rights reserved
 */

using System;
using System.Diagnostics;
using System.IO;

namespace strayex_shell_win
{
    class Program
    {
        public static string ShellPath = Directory.GetCurrentDirectory(); // Directory to work,
        public static string Cmd = ""; // Command,
        public static string Args = ""; // Arguments,
        
        static string DiscussFile(string FileName)
        { // TODO: Function to recognise files
            int i = 0;
            for (; FileName[i] != '.'; i++);
            // abc.exe 3

            string Extenstion = FileName.Substring(i + 1); // Gets extension name,

            // Determine, witch file is it:
            if (Extenstion == "exe") return "apk";
            else if (Extenstion == "txt") return "text";
            else if (Extenstion == "png") return "image";
            else return "0";
        }

        static void Cmd_interpret()
        {
            // First index is command, higher indexes are arguments,
            // If user proviede args for commands, that don't need them, shell will ignore them,

            string[] apps = Directory.GetFiles(ShellPath);

            // Commands:
            if (Cmd == "hello")
            {
                // Say hi to user :)
                Console.WriteLine("Hello user! :D");
                return;
            }
            else if (Cmd == "clear")
            {
                // Clear console,
                Console.Clear();
                return;
            }
            else if (Cmd == "echo") // Write something in console, if no args are given, shell will write empty line,
            {
                // Writes args on screen:
                Console.WriteLine(Args);
                return;
            }
            else if (Cmd == "cd")
            {
                // "cd" takes only one parameter and checks, if it exists in file system!
                if ((Args != null) && Directory.Exists(Args)) ShellPath = Args;
                else Console.WriteLine("Can't change directory, wrong argument!");
                Console.Title = ShellPath + " - Strayex Shell";
                return;
            }
            else if (Cmd == "help")
            {
                Console.WriteLine();
                Console.WriteLine("Strayex Shell Command list:");
                Console.WriteLine("- help - shows this list,");
                Console.WriteLine("- hello - make shell to say hello to you,");
                Console.WriteLine("- clear - clears consol's screen,");
                Console.WriteLine("- echo - write information on screen,");
                Console.WriteLine("- cd - changes active directory,");
                Console.WriteLine("- exit - close shell,");
                Console.WriteLine();
                return;
            }
            else if (Cmd == "exit") return;
            else if (Cmd == "") return;

            // File to open in third-party app:
            if (DiscussFile(Cmd) == "text")
            {
                Process.Start("notepad.exe", ShellPath + '\\' + Cmd);
                return;
            }

            // Executable binaries:
            for(int i = 0; i < apps.Length; i++)
            {
                if (ShellPath + '\\' + Cmd == apps[i])
                {
                    // Start given process:

                    var apk = new Process();
                    apk.StartInfo.FileName = apps[i];
                    // If there's input, add it to process:
                    apk.StartInfo.Arguments = Args;
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
                        return;
                    }

                    apk.WaitForExit();

                    // If there's output, print it:
                    string output = apk.StandardOutput.ReadToEnd();
                    if (output != "") Console.Write(output);

                    return;
                }
            }

            // Write info if no command or program found:
            Console.WriteLine("Command or program not found!");
        }

        static void Main(string[] args)
        {
            Console.Title = ShellPath + " - Strayex Shell";
            // Standard shell's header:
            Console.WriteLine("Strayex Shell for Windows v1.0.0");
            Console.WriteLine("Copyright (c) 2019 Daniel Strayker Nowak");
            Console.WriteLine("All rights reserved");

            // Command routine:
            string temp = "";

            // While shell still execute:
            while(temp != "exit")
            {
                // Set title of window:
                Console.Title = ShellPath + " - Strayex Shell";
                // Write line for command input:
                Console.Write(ShellPath + "> ");
                // Wait for command:
                temp = Console.ReadLine();
                // Split args and command into array:
                string[] help = temp.Split(' ');
                // First element of array is always command name!
                Cmd = help[0];

                // Prepare args string to add to program:
                for (int a = 1; a < help.Length; a++)
                {
                    if(help.Length - 1 > 1)
                    {
                        Args = Args + help[a] + ' ';
                    }
                    else
                    {
                        Args = Args + help[a];
                    }
                }

                // Interpret the command:
                Cmd_interpret();

                // Clear values of already executed command:
                Cmd = "";
                Args = "";
            }
        }
    }
}
