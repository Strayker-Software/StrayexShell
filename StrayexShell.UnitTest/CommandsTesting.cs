using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using strayex_shell_win;

namespace StrayexShell.UnitTest
{
    [TestClass]
    public class StrayexShellTesting
    {
        [TestMethod]
        public void StrayexShell_Help_WriteHelp()
        {
            var obj = new ShellCommand("help", new string[50]);

            if (obj.Interpret()) return;
            else throw new InvalidOperationException("Command Help not working!");
        }
    }
}
