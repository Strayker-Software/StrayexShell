/*
    Strayex Shell for Windows
    Copyright © 2019 Daniel Strayker Nowak
    All rights reserved
 */

using System;

namespace strayex_shell_win
{
    class Program
    {
        static void Cmd_interpret(string[] input)
        {
            // First index is command, higher indexes are arguments,
            // If user proviede args for commands, that don't need them, shell will ignore them,

            // Commands:
            if (input[0] == "hello") Console.WriteLine("Hello user! :D"); // Say hi to user :)
            else if (input[0] == "clear") Console.Clear(); // Clear console,
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
            }
            else if (input[0] == "exit") return;
            else Console.WriteLine("Command or program not found!");
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
                Console.Write("> ");
                temp = Console.ReadLine();
                string[] cmd = temp.Split(' ');
                Cmd_interpret(cmd);
            }
        }
    }
}
