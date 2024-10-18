namespace MSOPracticum
{
    public class Character
    {
        public Point position = new Point(0,0);
        public string direction = "east";

        private Character()
        {

        }

        private static readonly Character _character = new Character();

        public static Character GetCharacter()
        {
            return _character;
        }
    }
}
