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
        public static string Patha = Directory.GetCurrentDirectory();
        public static Process[] App_list = Process.GetProcesses();
        public static string Cmd = "";
        public static string Args = "";

        static void Cmd_interpret()
        {
            // First index is command, higher indexes are arguments,
            // If user proviede args for commands, that don't need them, shell will ignore them,

            string[] apps = Directory.GetFiles(Patha);

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
                if ((Args != null) && Directory.Exists(Args)) Patha = Args;
                else Console.WriteLine("Can't change directory, wrong argument!");
                Console.Title = Patha + " - Strayex Shell";
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

            // Executable binaries:
            for(int i = 0; i < apps.Length; i++)
            {
                if (Patha + '\\' + Cmd == apps[i])
                { // TODO
                    // Start given process:
                    var apk = Process.Start(apps[i], Args);
                    //apk.StartInfo.RedirectStandardOutput = true;
                    //apk.StartInfo.RedirectStandardInput = true;
                    //apk.StartInfo.RedirectStandardError = true;
                    //apk.StartInfo.FileName = apps[i];
                    //apk.StartInfo.Arguments = Args;
                    //apk.StartInfo.UseShellExecute = false;

                    /*
                    try
                    {
                        apk.Start();
                    }
                    catch (Exception)
                    {
                        apk = Process.Start(apps[i]);
                    }
                    */

                    // If there's output, print it:
                    //string output = apk.StandardOutput.ReadToEnd();
                    //Console.Write(output);

                    // If there's error, print it:
                    //string err = apk.StandardError.ReadToEnd();
                    //Console.Write(err);

                    // And wait, while app will exit:
                    while (apk.HasExited == false) ;

                    return;
                }
            }

            // Write info if no command or program found:
            Console.WriteLine("Command or program not found!");
        }

        static void Main(string[] args)
        {
            Console.Title = Patha + " - Strayex Shell";
            // Standard shell's header:
            Console.WriteLine("Strayex Shell for Windows v1.0.0");
            Console.WriteLine("Copyright (c) 2019 Daniel Strayker Nowak");
            Console.WriteLine("All rights reserved");

            // Command routine:
            string temp = "";
            while(temp != "exit")
            {
                Console.Title = Patha + " - Strayex Shell";
                Console.Write(Patha + "> ");
                temp = Console.ReadLine();
                string[] help = temp.Split(' ');
                Cmd = help[0];
                for (int a = 1; a < help.Length; a++)
                {
                    Args = Args + help[a] + ' ';
                }
                Cmd_interpret();
                Cmd = "";
                Args = "";
            }
        }
    }
}
