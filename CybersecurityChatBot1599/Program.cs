using System;
using System.Windows;

namespace CybersecurityChatBot1599
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            MainWindow window = new MainWindow();

            app.Run(window);
        }
    }
}