using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSOPracticum;

namespace MSOtest
{
    [TestClass]
    public class UnitTest1
    {
        // note: character is a singleton and therefore the test methods have a lasting change on the character's position, expected corrected answers should be considered with that in mind
        // also, tests are executed in alphabetical order in Visual Studio's test explorer, and in order of definition outside of it. Therefore, we made it so both of these orders are the same by including numbers after Test in the method names

        [TestMethod]
        public void Test1_ExecuteCommand()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Move 2");
            nesting.Add(0);
            Command command = new Command(commands, nesting);
            command.ExecuteCommands();
            Point correctPosition = new Point(2, 0); // from (0,0) move east to (2,0)
            Assert.AreEqual(correctPosition.X, command.chara.position.X);
        }

        [TestMethod]
        public void Test2_Move()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Move 2");
            nesting.Add(0);
            Command command = new Command(commands, nesting);
            command.Move(commands[0]);
            Point correctPosition = new Point(4, 0); // from (2,0) move east to (4,0)
            Assert.AreEqual(correctPosition.X, command.chara.position.X);
        }

        [TestMethod]
        public void Test3_TurnLeft()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Turn left");
            nesting.Add(0);
            Command command = new Command(commands, nesting);
            command.TurnLeft();
            string correctDirection = "north"; // from "east" to "north"
            Assert.AreEqual(correctDirection, command.chara.direction);
        }

        [TestMethod]
        public void Test4_Repeat()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Repeat 2 times");
            commands.Add("Move 2");
            nesting.Add(0);
            nesting.Add(1);
            Command command = new Command(commands, nesting);
            Point correctPosition = new Point(4, -4); // from (4,0) move north to (4, -4)
            command.Repeat(commands[0], 0, nesting[0]);
            Assert.AreEqual(correctPosition.X, command.chara.position.X);
        }

        [TestMethod]
        public void Test5_TurnRight()
        {
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            commands.Add("Turn right");
            nesting.Add(0);
            Command command = new Command(commands, nesting);
            command.TurnRight();
            string correctDirection = "east"; // from "north" to "east"
            Assert.AreEqual(correctDirection, command.chara.direction);
        }
    }
}