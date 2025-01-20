using System;

namespace MSOPracticumPresenter
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

        void ExecuteResponse(string response);
    }

    public class UIObserver : Component, IObserver
    {
        Presenter presenter = Presenter.GetPresenter();
        public void Update(ISubject subject)
        {
            string[] message = subject.state.Split(",");
            if (message[0] != "Run") return;
            subject.ExecuteResponse("Parse");
        }
    }

    public class ParserObserver : Component, IObserver
    {
        Presenter presenter = Presenter.GetPresenter();

        public void Update(ISubject subject)
        {
            string[] message = subject.state.Split(",");
            if (message[0] != "Run") return;
            subject.ExecuteResponse("Run");
        }
    }
}
