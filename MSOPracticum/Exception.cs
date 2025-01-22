namespace MSOPracticum
{
    [Serializable]
    public class OutsideGridException : Exception
    {
        public OutsideGridException(string message)
            : base(message)
        {
        }

        public OutsideGridException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class BlockedCellException : Exception
    {
        public BlockedCellException(string message)
            : base(message)
        {
        }

        public BlockedCellException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
