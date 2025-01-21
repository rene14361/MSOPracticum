namespace MSOPracticum
{
    public class Presenter : IMediator
    {
        public IComponent UIComponent { get; set; }
        public IComponent ParserComponent { get; set; }
        public IComponent CommandComponent { get; set; }

        public Presenter()
        {

        }

        public void Notify(IComponent sender, string message)
        {
            if (sender == UIComponent)
            {
                string[] splitMessage = message.Split("|");
                switch (splitMessage[0])
                {
                    // create parser and execute it
                    case "Parse":
                        Parser parser = new Parser(this);
                        int metrics = 0;
                        int.TryParse(splitMessage[1], out metrics);
                        int mode = splitMessage[2] switch
                        {
                            "Custom" or "Basic" or "Advanced" or "Expert" => 3,
                            _ => 4
                        };
                        if (mode == 3) parser.state = splitMessage[3];
                        else parser.state = splitMessage[2];
                        parser.ExecuteParser(mode, metrics);
                        break;

                    // return example commands that corresponds to selection
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
                }
            }
            else if (sender == ParserComponent)
            {
                UIComponent.Receive("Metrics|" + message);
            }
            else if (sender == CommandComponent)
            {
                UIComponent.Receive("Commands|" + message);
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
