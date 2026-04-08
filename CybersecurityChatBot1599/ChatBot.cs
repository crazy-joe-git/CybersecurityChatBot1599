using System;

//Handles chatbot logic and interaction
public class ChatBot
{
    private string userName;

    //Starts the chatbot program
    public void Start()
    {
        UIHelper.DisplayHeader();
        AskUserName();
      }

    // Prompts user for their name and validates input
    private void AskUserName()
        {
            do
            {
                Console.Write("\nEnter your name: ");
                userName = Console.ReadLine();

                // Check if input is empty or only spaces
                if (string.IsNullOrWhiteSpace(userName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name cannot be empty. Please try again.");
                    Console.ResetColor();
                }

            } while (string.IsNullOrWhiteSpace(userName));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome, {userName}! Let's learn about cybersecurity.");
            Console.ResetColor();
        }

}