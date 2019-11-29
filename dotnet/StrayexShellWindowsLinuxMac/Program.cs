/*
    Strayex Shell for Windows
    Copyright © 2019 Daniel Strayker Nowak
    All rights reserved
 */

using System;
using System.IO;

namespace StrayexShellWindows
{
    public class Program
    {
        public static string ShellPath = Directory.GetCurrentDirectory(); // Directory to work,
        public static string Cmd = ""; // Command,
        public static string[] Args = new string[50]; // Arguments, max 50,
        public static ShellVariable[] Vars = new ShellVariable[100]; // Max 100 shell variables,
        public static Version Ver = new Version(1, 0, 0, 1); // Version of shell,
        public static bool IfEcho = true; // Controls Echo execution,

        // Hello! Strayex Shell starts here! :D
        static void Main(string[] args)
        {
            // Here shell is checking if there is config script, it has to be named: "ssconfig.txt":
            if (File.Exists(ShellPath + '\\' +  "ssconfig.txt"))
            { // Yes, it's here! So user of shell want to configure it, before use. Let's do it!
                // To be sure of existence of config script, Script class will check it one more time in ExecuteScript function:
                var Config = new Script("ssconfig.txt", ShellPath);

                Config.ExecuteScript();
            }
            // Shell is now configured, let's start the command routine:

            // Standard shell's header:
            Console.WriteLine();
            Console.WriteLine("Strayex Shell for Windows v{0}.{1}.{2}", Ver.Major, Ver.Minor, Ver.Revision);
            Console.WriteLine("Copyright (c) 2019 Daniel Strayker Nowak");
            Console.WriteLine("All rights reserved");
            Console.WriteLine();

            // Set title of window:
            Console.Title = ShellPath + " - Strayex Shell";

            // Command routine:
            string temp = "";

            // While shell still execute:
            while(temp != "exit")
            {
                // Write line for command input:
                Console.Write(ShellPath + "> ");
                // Wait for command:
                temp = Console.ReadLine();
                // Split args and command into array:
                string[] help = temp.Split(' ');

                // First element of array is always command name!
                Cmd = help[0];

                // If user pressed just Enter button:
                if (Cmd == "") continue;

                // Prepare args strings to add to shell:
                Args = new string[50];

                for (int a = 1; a < help.Length; a++)
                {
                    Args[a - 1] = help[a];
                }

                // Command class instance:
                var cmdc = new ShellCommand(Cmd, Args);

                // Interpret the command:
                var IfInterpreted = cmdc.Interpret();

                if(IfInterpreted == false) Console.WriteLine("Command or program not found!");

                // Cleaning the vars for new input data:
                Cmd = "";
                Args = new string[50];
            }
        }
    }
}
