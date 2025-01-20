using System;

namespace MSOPracticum
{
    // Observer interface based on https://refactoring.guru/design-patterns/observer/csharp/example
    public interface IObserver
    {
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        public string state { get; set; }

        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();
    }
}
