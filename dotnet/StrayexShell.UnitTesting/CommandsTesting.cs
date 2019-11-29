using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StrayexShellWindows.UnitTesting
{
    [TestClass]
    public class StrayexShellCommandsTesting
    {
        [TestMethod]
        public void StrayexShell_Help_WriteHelp()
        {
            var obj = new ShellCommand("help", new string[50]);

            if (obj.Interpret()) return;
            else throw new InvalidOperationException("Command Help not working!");
        }

        [TestMethod]
        public void StrayexShell_Hello_WriteHello()
        {
            var obj = new ShellCommand("hello", new string[50]);

            if (obj.Interpret()) return;
            else throw new InvalidOperationException("Command Hello not working!");
        }

        [TestMethod]
        public void StrayexShell_Clear_ClearConsole()
        {
            var obj = new ShellCommand("clear", new string[50]);

            if (obj.Interpret()) return;
            else throw new InvalidOperationException("Command Clear not working!");
        }

        [TestMethod]
        public void StrayexShell_Echo_WriteEmptyLine()
        {
            var obj = new ShellCommand("echo", new string[50]);

            if (obj.Interpret()) return;
            else throw new InvalidOperationException("Command Echo not working or turned off!");
        }

        [TestMethod]
        public void StrayexShell_ChangeDirectory_ChangeDirectoryToMainPartition()
        {
            string[] Args = new string[50];
            Args[0] = "C:\\";
            var obj = new ShellCommand("cd", Args);

            if (obj.Interpret()) return;
            else throw new InvalidOperationException("Command CD not working or IO Exception!");
        }

        [TestMethod]
        public void StrayexShell_Color_ColorTerminalInMatrixStyle()
        {
            string[] Args = new string[50];
            Args[0] = "Black";
            Args[1] = "Green";
            var obj = new ShellCommand("color", Args);

            if (obj.Interpret()) return;
            else throw new InvalidOperationException("Command Color not working!");
        }

        [TestMethod]
        public void StrayexShell_Set_DefineNewVar()
        {
            string[] Args = new string[50];
            Args[0] = "TestString1";
            Args[1] = "Hello!";
            var obj = new ShellCommand("set", Args);

            if (obj.Interpret()) return;
            else throw new InvalidOperationException("Command Set not working!");
        }
    }
}
