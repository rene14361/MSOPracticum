using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSOPracticum;

namespace MSOtest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_ExecuteCommand()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Move 2");
            nesting.Add(1);
            Command command = new Command(commands, nesting);
            command.ExecuteCommands();
        }
        [TestMethod]
        public void Test_Move()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Move 2");
            nesting.Add(1);
            Command command = new Command(commands, nesting);
            command.Move(commands[0]);
        }
        [TestMethod]
        public void Test_TurnLeft()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Turn left");
            nesting.Add(1);
            Command command = new Command(commands, nesting);
            command.TurnLeft();
        }
        [TestMethod]
        public void Test_TurnRight()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Turn right");
            nesting.Add(1);
            Command command = new Command(commands, nesting);
            command.TurnRight();
        }
        [TestMethod]
        public void Test_Repeat()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Repeat 2 times");
            commands.Add("Move 2");
            nesting.Add(1);
            nesting.Add(2);
            Command command = new Command(commands, nesting);
            command.Repeat(commands[0], 0, nesting[0]);
        }
    }
}