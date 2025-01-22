using MSOPracticum;

namespace MSOPracticumUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Presenter presenter = new Presenter();
            Application.Run(new Form1(presenter));
            
        }
    }
}
