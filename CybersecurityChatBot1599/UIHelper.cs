using System;

//Handles UI elements like headers and styling
public static class UIHelper
{
    //Displays the chatbot header
    public static void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine(@"
           ____       _               ____        _   
          / ___| ___ | |__   ___ _ __| __ )  ___ | |_ 
         | |    / _ \| '_ \ / _ \ '__|  _ \ / _ \| __|
         | |___| (_) | |_) |  __/ |  | |_) | (_) | |_ 
          \____|\___/|_.__/ \___|_|  |____/ \___/ \__|

                CYBERSECURITY AWARENESS BOT
        ");

        Console.ResetColor();

        Console.WriteLine("========================================");
        Console.WriteLine(" Learn about staying safe online!");
        Console.WriteLine(" Type 'exit' anytime to quit.");
        Console.WriteLine("========================================");

    }
}