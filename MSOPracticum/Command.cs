namespace MSOPracticum
{
    public class Command : IComponent
    {
        private Presenter mediator { get; set; }
        private List<string> commandList = new List<string>();
        private List<int> commandNestingLevels = new List<int>();
        public Character chara = Character.GetCharacter();
        private string trace = "";

        public Command(Presenter presenter, List<string> commandList, List<int> commandNestingLevels)
        {
            this.mediator = presenter;
            mediator.CommandComponent = this;
            this.commandList = commandList;
            this.commandNestingLevels = commandNestingLevels;
        }

        public void Receive(string message)
        {
            
        }

        public void ExecuteCommands()
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                RunCommand(commandList[i], i, commandNestingLevels[i]);
                trace = trace + commandList[i] + "; ";
            }
            string traceOutput = "Command trace:\r\n" + trace;
            string endState = "End state " + (chara.position.X, chara.position.Y) + " facing " + chara.direction;
            mediator.Notify(this, traceOutput);
            mediator.Notify(this, endState);
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
    }
}
