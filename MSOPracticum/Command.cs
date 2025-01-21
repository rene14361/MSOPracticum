namespace MSOPracticum
{
    public class Command : IComponent
    {
        private Presenter mediator { get; set; }
        private List<string> commandList = new List<string>();
        private List<int> commandNestingLevels = new List<int>();
        public Character chara { get; set; }
        private string trace = "";
        private bool exerciseMode = false;

        public Command(Presenter presenter, List<string> commandList, List<int> commandNestingLevels)
        {
            this.mediator = presenter;
            this.chara = new Character(mediator);
            mediator.CommandComponent = this;
            this.commandList = commandList;
            this.commandNestingLevels = commandNestingLevels;
        }

        public void Receive(string message)
        {
            if (message == "Exercise") exerciseMode = true;
        }

        public void ExecuteCommands()
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                RunCommand(commandList[i], i, commandNestingLevels[i]);
                trace = trace + commandList[i] + "; ";
            }

            Point gridPosition = CalculateGridPosition(chara.position.X, chara.position.Y);
            string traceOutput = "Command trace:\r\n" + trace;
            string endState = "End state is " + (chara.position.X, chara.position.Y) + " facing " + chara.direction;
            string moduloPosition = "\r\nGrid position with modulo 5 is " + (gridPosition.X, gridPosition.Y);
            mediator.Notify(this, traceOutput);
            mediator.Notify(this, endState + moduloPosition);
            Console.WriteLine(traceOutput);
            Console.WriteLine(endState);
        }

        private void RunCommand(string cmd, int currentCommand, int currentNestingLevel)
        {
            if (cmd.Split(" ")[0] == "Repeat")
            {
                Repeat(cmd, currentCommand, currentNestingLevel); return;
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

        public void Repeat(string cmd, int currentCommand, int currentNestinglevel)
        {
            int commandCount = 0;
            int currentCommandCount = 1;
            bool WallAhead = false;
            bool GridEdge = false;
            if (cmd.Split(" ")[1] == "WallAhead")
            {
                while (WallAhead =! true)
                {
                    if (IsWall(NextBlock()))
                    {
                        WallAhead = true; break;
                    }
                    commandCount = 0;
                    currentCommandCount = 1;
                    foreach (int j in commandNestingLevels.Where(n => n > currentNestinglevel && commandList.Count > currentCommand + currentCommandCount))
                    {
                        RunCommand(commandList[currentCommand + currentCommandCount], currentCommand + currentCommandCount, currentNestinglevel);
                        trace = trace + commandList[currentCommand + currentCommandCount] + ", ";
                        commandCount++;
                        currentCommandCount++;
                    }
                }
            }
            else if (cmd.Split(" ")[1] == "GridEdge")
            {
                while (GridEdge =! true)
                {
                    if (NextBlock().X < 0 || NextBlock().Y < 0 || NextBlock().X > 99 || NextBlock().Y > 99)
                    {
                        GridEdge = true; break;
                    }
                    commandCount = 0;
                    currentCommandCount = 1;
                    foreach (int j in commandNestingLevels.Where(n => n > currentNestinglevel && commandList.Count > currentCommand + currentCommandCount))
                    {
                        RunCommand(commandList[currentCommand + currentCommandCount], currentCommand + currentCommandCount, currentNestinglevel);
                        trace = trace + commandList[currentCommand + currentCommandCount] + ", ";
                        commandCount++;
                        currentCommandCount++;
                    }
                }
            }
            commandList.RemoveRange(currentCommand + 1, commandCount);
        }

        public void RepeatUntil(string cmd, int currentCommand, int currentNestinglevel)
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
                    trace = trace + commandList[i] + "; ";
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

            Point gridPosition = CalculateGridPosition(chara.position.X, chara.position.Y);
            string message;
            if (!exerciseMode) message = "Move|" + gridPosition.X.ToString() + "," + gridPosition.Y.ToString();
            else message = "Move|" + chara.position.X.ToString() + "," + chara.position.Y.ToString();
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
            if (position.X == position.Y)
            {
                return true;
            }
            else
            {
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
