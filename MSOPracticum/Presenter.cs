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
                // Forwards the request to the parser for "Parse", "Attempt", "Load" and "Exercise" and "Example"
                ParserComponent.Receive(message);
            }
            else if (sender == ParserComponent)
            {
                string[] splitMessage = message.Split("|");
                switch (splitMessage[0]) 
                {
                    // Asks the command component to turn on exercise mode
                    case "Mode":
                        CommandComponent.Receive(message);
                        break;
                    
                    // Forwards responses from the parser back to the UI.
                    default:
                        UIComponent.Receive(message);
                        break;
                }
            }
            else if (sender == CommandComponent)
            {
                // Forwards the request to the UI
                UIComponent.Receive("Commands|" + message);
            }
            else if (sender == CharacterComponent)
            {
                // Forwards the request to the UI
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
