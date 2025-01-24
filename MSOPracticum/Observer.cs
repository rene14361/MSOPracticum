namespace MSOPracticum
{
    // Observer interface based on https://refactoring.guru/design-patterns/observer/csharp/example
    public interface IObserver
    {
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();
    }
}
