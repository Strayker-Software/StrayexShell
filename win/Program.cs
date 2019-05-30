/*
    Strayex Shell for Windows
    Copyright © 2019 Daniel Strayker Nowak
    All rights reserved
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace strayex_shell_win
{
    class Program
    {
        public static string Path = Directory.GetCurrentDirectory();
        public static Process[] App_list = Process.GetProcesses();

        static void Cmd_interpret(string[] input)
        {
            // First index is command, higher indexes are arguments,
            // If user proviede args for commands, that don't need them, shell will ignore them,

            string[] apps = Directory.GetFiles(Path);

            // Commands:
            if (input[0] == "hello")
            {
                // Say hi to user :)
                Console.WriteLine("Hello user! :D");
                return;
            }
            else if (input[0] == "clear")
            {
                // Clear console,
                Console.Clear();
                return;
            }
            else if (input[0] == "echo") // Write something in console, if no args are given, shell will write empty line,
            {
                int arr_length = input.Length - 1; // Subtract the command form array;

                // Writes args on screen:
                for (int i = 1; i <= arr_length; i++)
                {
                    if(arr_length > 1)
                    {
                        Console.Write(input[i]);
                        Console.Write(' ');
                    }
                    else Console.Write(input[i]);
                }
                Console.Write('\n'); // End of Line,
                return;
            }
            else if(input[0] == "cd")
            {
                // "cd" takes only one parameter and checks, if it exists!
                if (input[1] != null && Directory.Exists(input[1])) Path = input[1];
                else Console.WriteLine("Can't change directory, wrong argument!");
                return;
            }
            else if(input[0] == "help")
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
            else if (input[0] == "exit") return;

            // Executable binaries:
            for(int i = 0; i < apps.Length; i++)
            {
                if (Path + '\\' + input[0] == apps[i])
                {
                    Process apk = Process.Start(apps[i]);

                    // Shell will have to wait, while app will start:

                    /* TODO
                    for(int a = 0; a < App_list.Length;)
                    {
                        if (apk.ProcessName != App_list[a].ProcessName)
                        {
                            a++;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }
                    }
                    
                    int a = 0;
                    while ((a < App_list.Length) && (apk.ProcessName != App_list[a].ProcessName))
                    {
                        if (apk.ProcessName != App_list[a].ProcessName)
                        {
                            a++;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }
                    }

                    */
                    return;
                }
            }

            // Write info if no command or program found:
            Console.WriteLine("Command or program not found!");
        }

        static void Main(string[] args)
        {
            // Standard shell's header:
            Console.WriteLine("Strayex Shell for Windows v1.0.0");
            Console.WriteLine("Copyright (c) 2019 Daniel Strayker Nowak");
            Console.WriteLine("All rights reserved");

            // Command routine:
            string temp = "";
            while(temp != "exit")
            {
                Console.Write(Path + "> ");
                temp = Console.ReadLine();
                string[] cmd = temp.Split(' ');
                Cmd_interpret(cmd);
            }
        }
    }
}
