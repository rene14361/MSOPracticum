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
            Presenter presenter = new Presenter();
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            bool[,] exerciseGrid = new bool[5, 5];
            Point exerciseGoal = new Point(0, 0);
            commands.Add("Move 2");
            nesting.Add(0);
            Command command = new Command(presenter, commands, nesting, exerciseGrid, exerciseGoal);
            command.ExecuteCommands();
            Point correctPosition = new Point(2, 0); // from (0,0) move east to (2,0)
            Assert.AreEqual(correctPosition.X, command.chara.position.X);
        }

        [TestMethod]
        public void Test2_Move()
        {
            Presenter presenter = new Presenter();
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            bool[,] exerciseGrid = new bool[5, 5];
            Point exerciseGoal = new Point(0, 0);
            commands.Add("Move 2");
            nesting.Add(0);
            Command command = new Command(presenter, commands, nesting, exerciseGrid, exerciseGoal);
            command.ExecuteCommands();
            Point correctPosition = new Point(2, 0); // from (2,0) move east to (4,0)
            Assert.AreEqual(correctPosition.X, command.chara.position.X);
        }

        [TestMethod]
        public void Test3_TurnLeft()
        {
            Presenter presenter = new Presenter();
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            bool[,] exerciseGrid = new bool[5, 5];
            Point exerciseGoal = new Point(0, 0);
            commands.Add("Turn left");
            nesting.Add(0);
            Command command = new Command(presenter, commands, nesting, exerciseGrid, exerciseGoal);
            command.TurnLeft();
            string correctDirection = "north"; // from "east" to "north"
            Assert.AreEqual(correctDirection, command.chara.direction);
        }

        [TestMethod]
        public void Test4_Repeat()
        {
            Presenter presenter = new Presenter();
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            bool[,] exerciseGrid = new bool[5, 5];
            Point exerciseGoal = new Point(0, 0);
            commands.Add("Repeat 2 times");
            commands.Add("Move 2");
            nesting.Add(0);
            nesting.Add(1);
            Command command = new Command(presenter, commands, nesting, exerciseGrid, exerciseGoal);
            Point correctPosition = new Point(4, 0); // from (0,0) move eas to (4, 0)
            command.ExecuteCommands();
            Assert.AreEqual(correctPosition.X, command.chara.position.X);
        }

        [TestMethod]
        public void Test5_TurnRight()
        {
            Presenter presenter = new Presenter();
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            bool[,] exerciseGrid = new bool[5, 5];
            Point exerciseGoal = new Point(0, 0);
            commands.Add("Turn right");
            nesting.Add(0);
            Command command = new Command(presenter, commands, nesting, exerciseGrid, exerciseGoal);
            command.ExecuteCommands();
            string correctDirection = "south"; // from "east" to "south"
            Assert.AreEqual(correctDirection, command.chara.direction);
        }

        [TestMethod]
        public void Test6_RepeatUntilGridEdge()
        {
            Presenter presenter = new Presenter();
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            bool[,] exerciseGrid = new bool[5, 5];
            Point exerciseGoal = new Point(0, 0);
            commands.Add("RepeatUntil GridEdge");
            commands.Add("Move 1");
            nesting.Add(0);
            nesting.Add(1);
            Command command = new Command(presenter, commands, nesting, exerciseGrid, exerciseGoal);
            Point correctPosition = new Point(5, 0); // from (4,0) move south until (4,5) which is the grid's edge
            command.ExecuteCommands();
            Assert.AreEqual(correctPosition.X, command.chara.position.X);
        }

        [TestMethod]
        public void Test7_RepeatUntilWallAhead()
        {
            Presenter presenter = new Presenter();
            List<string> commands = new List<string>();
            List<int> nesting = new List<int>();
            bool[,] exerciseGrid = new bool[5, 5];
            Point exerciseGoal = new Point(0, 0);
            commands.Add("Move 1");
            commands.Add("Turn right");
            commands.Add("RepeatUntil WallAhead");
            commands.Add("Move 1");
            nesting.Add(0);
            nesting.Add(0);
            nesting.Add(0);
            nesting.Add(1);
            Command command = new Command(presenter, commands, nesting, exerciseGrid, exerciseGoal);
            Point correctPosition = new Point(1, 1); // from (4,0) move south until (4,3) because (4,4) is a wall
            command.ExecuteCommands();
            Assert.AreEqual(correctPosition.Y, command.chara.position.Y);
        }
    }
}