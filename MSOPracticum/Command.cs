namespace MSOPracticum
{
    public class Command
    {
        private List<string> commandList = new List<string>();
        private List<int> commandNestingLevels = new List<int>();
        public Character chara = Character.GetCharacter();
        private string trace = "";

        public Command(List<string> commandList, List<int> commandNestingLevels)
        {
            this.commandList = commandList;
            this.commandNestingLevels = commandNestingLevels;
        }
        
        public void ExecuteCommands()
        { 
            for (int i = 0; i < commandList.Count; i++)
            {
                RunCommand(commandList[i], i, commandNestingLevels[i]);
                trace = trace + commandList[i] + "; ";
            }
            Console.WriteLine(trace);
            Console.WriteLine("End state " + (chara.position.X, chara.position.Y) + " facing " + chara.direction);
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
            for (int i = 0; i < int.Parse(cmd.Split(" ")[1]); i++)
            {
                commandCount = 0;
                currentCommandCount = 1;
                foreach (int j in commandNestingLevels.Where(n => n > currentNestinglevel && commandList.Count > currentCommand + currentCommandCount))
                {
                    RunCommand(commandList[currentCommand + currentCommandCount], currentCommand + currentCommandCount, currentNestinglevel);
                    trace = trace + commandList[i] + ", ";
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
    }
}
