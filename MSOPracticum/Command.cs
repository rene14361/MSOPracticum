using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOPracticum
{
    public class Command
    {
        public Command() { }
        public List<string> comp;

        public bool IsValid(string comp)
        {
            var v = 0;
            if (comp.Split(" ").Count() <= 1 || comp.Split().Count() > 3)
            {
                return false;
            }
            if (comp.Split(" ")[0] == "Move" || comp.Split(" ")[0] == "Repeat")
            {
                v = int.Parse(comp.Split(" ")[1]);
            }
            if (comp != null) 
            {
                if (comp == "Turn left" || comp == "Turn right")
                { return true; }
                else if (comp.Split(" ")[0] == "Move")
                { return true; }
                else if (comp.Split().Count() < 3)
                { return false; }
                else if (comp.Split(" ")[0] == "Repeat"
                         && comp.Split(" ")[2] == "times"
                         && int.TryParse(comp.Split(" ")[1], result: out v))
                { return true; }
                else { return false; }
            }
            else { return false; }
        }
    }
}
