using System;

//Handles UI elements like headers and styling
public static class UIHelper
{
    //Displays the chatbot header
    public static void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine(@"
            ███████╗       ██████╗     ██████╗   ████████╗
            ██╔════╝       ██╔══██╗   ██     ██  ╚══██╔══╝
            █████╗  ████╗  ██████╔╝   ██     ██     ██║   
            ██╔══╝  ╚═══╝  ██╔══██╗   ██     ██     ██║   
            ███████╗       ██████╔╝    ██████╔╝     ██║   
            ╚══════╝       ╚═════╝     ╚═════╝      ╚═╝   
                                            
                            E-Bot
                    CYBERSECURITY AWARENESS BOT
                ");

        Console.ResetColor();

        Console.WriteLine("========================================");
        Console.WriteLine(" Learn about staying safe online!");
        Console.WriteLine(" Type 'exit' anytime to quit.");
        Console.WriteLine("========================================");

    }

    // Simulates typing effect for chatbot responses
    public static void TypeText(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            System.Threading.Thread.Sleep(20); // small delay
        }
        Console.WriteLine();
    }
}