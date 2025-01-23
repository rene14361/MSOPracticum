namespace MSOPracticum
{
    public class Command : IComponent
    {
        private Presenter mediator { get; set; }
        private List<string> commandList = new List<string>();
        private List<int> commandNestingLevels = new List<int>();
        public Character chara { get; set; }
        private string trace = "";
        private bool caughtException = false;
        private bool exerciseMode = false;
        private bool[,] exerciseGrid = new bool[5, 5];
        private Point exerciseGoal = new Point(0, 0);
        public bool usingUI = true; // only exists to support running the old console program by setting it to false, we're not sure whether backwards compatibility is required, if it's not then pretend this doesn't exist

        public Command(Presenter presenter, List<string> commandList, List<int> commandNestingLevels, bool[,] exerciseGrid, Point exerciseGoal)
        {
            this.mediator = presenter;
            this.chara = new Character(mediator);
            mediator.CommandComponent = this;
            this.commandList = commandList;
            this.commandNestingLevels = commandNestingLevels;
            this.exerciseGrid = exerciseGrid;
            this.exerciseGoal = exerciseGoal;
        }

        public void Receive(string message)
        {
            if (message == "Mode") exerciseMode = true;
        }

        public void ExecuteCommands()
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                RunCommand(commandList[i], i, commandNestingLevels[i]);
                if (caughtException) return;
                trace = trace + commandList[i] + "; ";
            }

            Point gridPosition = CalculateGridPosition(chara.position.X, chara.position.Y);
            string traceOutput = "Command trace:\r\n" + trace;
            string endState = "End state is " + (chara.position.X, chara.position.Y) + " facing " + chara.direction;
            string finalPosition;
            if (!exerciseMode) finalPosition = "\r\nGrid position with modulo 5 is " + (gridPosition.X, gridPosition.Y);
            else 
            {
                bool goalReached = (chara.position.X == exerciseGoal.X && chara.position.Y == exerciseGoal.Y);
                finalPosition = goalReached ? $"\r\nYou reached the goal, congratulations!" : "\r\nThe character is not at the goal position, please try again.";
            } 
            Console.WriteLine(traceOutput);
            Console.WriteLine(endState);
            if (!usingUI) return;
            mediator.Notify(this, traceOutput);
            mediator.Notify(this, endState + finalPosition);
        }

        private void RunCommand(string cmd, int currentCommand, int currentNestingLevel)
        {
            // if not in exercise mode, just pick the command
            if (!exerciseMode) { PickCommand(cmd, currentCommand, currentNestingLevel); return; }
            // else throw exceptions
            try
            {
                PickCommand(cmd, currentCommand, currentNestingLevel);
                if (chara.position.X >= 5 || chara.position.X < 0 || chara.position.Y >= 5 || chara.position.Y < 0) throw new OutsideGridException($"The character went outside of the exercise grid at {(chara.position.X, chara.position.Y)}!");
                if (!exerciseGrid[chara.position.Y, chara.position.X]) throw new BlockedCellException($"The character tried to go to a position blocked by the exercise at {(chara.position.X, chara.position.Y)}!");
            }
            catch (OutsideGridException e)
            {
                caughtException = true;
                Console.WriteLine(e.Message);
                mediator.Notify(this, "Outside|" + e.Message);

            }
            catch (BlockedCellException e)
            {
                caughtException = true;
                Console.WriteLine(e.Message);
                mediator.Notify(this, "Blocked|" + e.Message);
            }
        }

        private void PickCommand(string cmd, int currentCommand, int currentNestingLevel)
        {
            if (cmd.Split(" ")[0] == "Repeat")
            {
                Repeat(cmd, currentCommand, currentNestingLevel); return;
            }
            else if (cmd.Split(" ")[0] == "RepeatUntil")
            {
                RepeatUntil(cmd, currentCommand, currentNestingLevel); return;
            }
            else if (cmd.Split(" ")[0] == "Move")
            {
                Move(cmd); return;
            }
            else if (cmd.Split(" ")[0] == "Turn" && cmd.Split(" ")[1] == "left")
            {
                TurnLeft(); return;
            }
            else if (cmd.Split(" ")[0] == "Turn" && cmd.Split(" ")[1] == "right")
            {
                TurnRight(); return;
            }
        }

        public void RepeatUntil(string cmd, int currentCommand, int currentNestinglevel)
        {
            int commandCount = 0;
            int currentCommandCount = 1;
            bool WallAhead = false;
            bool GridEdge = false;
            if (cmd.Split(" ")[1] == "WallAhead")
            {
                while (WallAhead != true)
                {
                    if (IsWall(NextBlock()))
                    {
                        WallAhead = true; break;
                    }
                    commandCount = 0;
                    currentCommandCount = 1;
                    RunCommand(commandList[currentCommand + currentCommandCount], currentCommand + currentCommandCount, currentNestinglevel);
                    if (caughtException) return;
                    trace = trace + commandList[currentCommand + currentCommandCount] + ", ";
                    commandCount++;
                    currentCommandCount++;
                }
            }
            else if (cmd.Split(" ")[1] == "GridEdge")
            {
                while (GridEdge != true)
                {
                    if (NextBlock().X < 0 || NextBlock().Y < 0 || NextBlock().X > 4 || NextBlock().Y > 4)
                    {
                        GridEdge = true; break;
                    }
                    commandCount = 0;
                    currentCommandCount = 1;
                    RunCommand(commandList[currentCommand + currentCommandCount], currentCommand + currentCommandCount, currentNestinglevel);
                    if (caughtException) return;
                    trace = trace + commandList[currentCommand + currentCommandCount] + ", ";
                    commandCount++;
                    currentCommandCount++;
                }
            }
            commandList.RemoveRange(currentCommand + 1, commandCount);
        }

        public void Repeat(string cmd, int currentCommand, int currentNestinglevel)
        {
            int commandCount = 0;
            int currentCommandCount = 1;
            for (int i = 0; i < int.Parse(cmd.Split(" ")[1]); i++)
            {
                commandCount = 0;
                currentCommandCount = 1;
                foreach (int j in commandNestingLevels.Where(n => n > currentNestinglevel && commandList.Count > currentCommand + currentCommandCount))
                {
                    RunCommand(commandList[currentCommand + currentCommandCount], currentCommand + currentCommandCount, currentNestinglevel);
                    if (caughtException) return;
                    trace = trace + commandList[currentCommand + currentCommandCount] + "; ";
                    commandCount++;
                    currentCommandCount++;
                }
            }
            commandList.RemoveRange(currentCommand + 1, commandCount);
        }

        public void Move(string cmd)
        {
            int val = int.Parse(cmd.Split(" ")[1]);

            switch (chara.direction)
            {
                case "south":
                    chara.position.Y += val; break;
                case "west":
                    chara.position.X -= val; break;
                case "north":
                    chara.position.Y -= val; break;
                default:
                    chara.position.X += val; break;
            }

            string message;
            Point gridPosition = CalculateGridPosition(chara.position.X, chara.position.Y);
            message = "Move|" + gridPosition.X.ToString() + "," + gridPosition.Y.ToString();

            if (!usingUI) return;
            mediator.Notify(chara, message);
        }

        public void TurnLeft()
        {
            chara.direction = chara.direction switch
            {
                "south" => "east",
                "west" => "south",
                "north" => "west",
                _ => "north",
            };

            if (!usingUI) return;
            mediator.Notify(chara, "Turn|" + chara.direction);
        }

        public void TurnRight()
        {
            chara.direction = chara.direction switch
            {
                "south" => "west",
                "west" => "north",
                "north" => "east",
                _ => "south",
            };

            if (!usingUI) return;
            mediator.Notify(chara, "Turn|" + chara.direction);
        }

        public Point NextBlock()
        {
            switch(chara.direction)
            {
                case "south":
                    return new Point(chara.position.X, chara.position.Y + 1);
                case "west":
                    return new Point(chara.position.X - 1, chara.position.Y);
                case "north":
                    return new Point(chara.position.X, chara.position.Y - 1);
                default:
                    return new Point(chara.position.X + 1, chara.position.Y);
            }
        }

        public bool IsWall(Point position)
        {
            // if not in exercise mode, to differentiate this from GridEdge it considers the cells next to the edge to be walls
            if (!exerciseMode)
            {
                Point gridPos = CalculateGridPosition(position.X, position.Y);
                if ((gridPos.X == 0 || gridPos.Y == 0) || (gridPos.X == 4 || gridPos.Y == 4)) return true;
                else return false;
            }
            // else in exercise mode it considers the edge of the grid AND the red cells to be walls
            else
            {
                if ((position.X < 0 || position.Y < 0 || position.X > 4 || position.Y > 4)) return true;
                if (!exerciseGrid[position.Y, position.X]) return true;
                return false;
            }
        }

        public Point CalculateGridPosition(int x, int y)
        {
            // Calculates modulo value, we have to do this because we need the modulo value for our grid system and in C# using % gives remainder not modulo
            x = x % 5; if (x < 0) x += 5;
            y = y % 5; if (y < 0) y += 5;
            return new Point(x, y);
        }
    }
}
