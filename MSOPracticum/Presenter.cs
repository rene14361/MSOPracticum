using MSOPracticumPresenter;

namespace MSOPracticum
{
    public class Presenter : IMediator
    {
        public IComponent UIComponent { get; set; }
        public IComponent ParserComponent { get; set; }
        private Presenter()
        {

        }

        private static readonly Presenter _presenter = new Presenter();

        public static Presenter GetPresenter()
        {
            return _presenter;
        }

        public void Notify(IComponent sender, string message)
        {
            if (sender == UIComponent)
            {
                string[] splitMessage = message.Split(",");
                switch (splitMessage[0])
                {
                    // create parser and execute it
                    case "Parse":
                        Parser parser = new Parser();
                        int metrics = 0;
                        int.TryParse(splitMessage[1], out metrics);
                        int mode = splitMessage[2] switch
                        {
                            "Custom" or "Basic" or "Advanced" or "Expert" => 3,
                            _ => 4
                        };
                        parser.state = splitMessage[3];
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
                        else sender.Receive("Input," + Example.ReturnExample(n));
                        break;

                }
            }
            else if (sender == ParserComponent && message == "")
            {

            }
        }
    }

    public interface IMediator
    {
        public void Notify(IComponent sender, string message);
    }

    public interface IComponent
    {
        public Presenter mediator { get; set; }

        public void Receive(string message)
        {

        }
    }
}
