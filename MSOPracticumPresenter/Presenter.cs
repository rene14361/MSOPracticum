using System;
using System.Drawing;

namespace MSOPracticumPresenter;

public static class Program
{
    static void Main()
    {

    }
}

public class Presenter : IMediator
{
    UIObserver formObserver;
    ParserObserver parserObserver;
    private Presenter()
    {

    }

    private static readonly Presenter _presenter = new Presenter();

    public static Presenter GetPresenter()
    {
        return _presenter;
    }

    public void Notify(Component sender, string message)
    {
        if (sender == formObserver)
        {

        }
        else if (sender == parserObserver && message == "")
        {

        }
    }
}

public interface IMediator
{
    public void Notify(Component sender, string message);
}

public class Component
{

}