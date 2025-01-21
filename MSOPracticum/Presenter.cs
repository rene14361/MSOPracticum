namespace MSOPracticum
{
    public class Presenter : IMediator
    {
        public IComponent UIComponent { get; set; }
        public IComponent ParserComponent { get; set; }
        public IComponent CommandComponent { get; set; }
        public IComponent CharacterComponent { get; set; }

        public Presenter()
        {
            Parser parser = new Parser(this);
            ParserComponent = parser;
        }

        public void Notify(IComponent sender, string message)
        {
            if (sender == UIComponent)
            {
                string[] splitMessage = message.Split("|");
                switch (splitMessage[0])
                {
                    // Create parser and execute it
                    case "Parse" or "Attempt":
                        Parser parser = new Parser(this);

                        int metrics = 0;
                        int.TryParse(splitMessage[1], out metrics);

                        // Selects parsing mode 3 for text input and parsing mode 4 for custom file paths
                        int mode;
                        if (splitMessage[0] != "Attempt") mode = splitMessage[2] switch
                        {
                            "Custom" or "Basic" or "Advanced" or "Expert" => 3,
                            _ => 4
                        };
                        else break; // mode = 3; // Disabled for now, not yet implemented
                        if (mode == 3) parser.state = splitMessage[3];
                        else parser.state = splitMessage[2];
                        parser.ExecuteParser(mode, metrics);
                        break;

                    // Returns example commands that correspond to selection
                    case "Input":
                        int n = splitMessage[1] switch
                        {
                            "Basic" => 1,
                            "Advanced" => 2,
                            "Expert" => 3,
                            _ => 0
                        };
                        if (n == 0) return;
                        else sender.Receive("Input|" + Example.ReturnExample(n));
                        break;

                    // Forwards the request to the parser
                    default:
                        ParserComponent.Receive(message);
                        break;
                }
            }
            else if (sender == ParserComponent)
            {
                string[] splitMessage = message.Split("|");
                switch (splitMessage[0]) 
                {
                    default:
                        UIComponent.Receive(message);
                        break;
                }

            }
            else if (sender == CommandComponent)
            {
                UIComponent.Receive("Commands|" + message);
            }
            else if (sender == CharacterComponent)
            {
                UIComponent.Receive(message);
            }
        }
    }

    public interface IMediator
    {
        public void Notify(IComponent sender, string message);
    }

    public interface IComponent
    {
        public void Receive(string message);
    }
}
