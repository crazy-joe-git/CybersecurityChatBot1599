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

    //Prompts user for their name 
    public void AskUserName()
    {
        Console.Write("\nPlease enter your name: ");
        userName = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine($"\nWelcome, {userName}! Let's learn about some cybersecurity tips.");

        Console.ResetColor();

    }

}