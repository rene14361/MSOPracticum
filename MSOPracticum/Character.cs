namespace MSOPracticum
{
    public class Character : IComponent
    {
        private Presenter mediator { get; set; }
        public Point position = new Point(0,0);
        public string direction = "east";

        public Character(Presenter presenter)
        {
            this.mediator = presenter;
            mediator.CharacterComponent = this;
        }

        public void Receive(string message)
        {

        }
    }
}
