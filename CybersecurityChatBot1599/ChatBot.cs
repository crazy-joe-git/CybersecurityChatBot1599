using System;
using System.Media;

//Handles chatbot logic and interaction
public class ChatBot
{
    private string userName = string.Empty;

    //Starts the chatbot program
    public void Start()
    {
        PlayVoiceGreeting(); //Play welcome message

        UIHelper.DisplayHeader();

        AskUserName();

        OperateChatbot(); //Start the chat loop
    }

    // Prompts user for their name and validates input
    private void AskUserName()
    {
        do
        {
            Console.Write("\nEnter your name: ");
            userName = Console.ReadLine() ?? string.Empty;

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

    private void OperateChatbot()
    {
        while (true)
        {
            // Chatbot logic
            Console.Write("\nYou: ");
            string userInput = (Console.ReadLine() ?? string.Empty).ToLowerInvariant();

            if (userInput == "exit")
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nThank you for the chatting! Stay safe online!");
                Console.ResetColor();
                break;
            }

            Respond(userInput);
        }
    }
    // Handles chatbot responses
    private void Respond(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            UIHelper.TypeText("Bot: Please enter something.");
        }
        else if (userInput.Contains("how are you"))
        {
            UIHelper.TypeText($"Bot: I'm functioning perfectly, {userName}!");
        }
        else if (userInput.Contains("purpose"))
        {
            UIHelper.TypeText("Bot: As a Cybersecurity Awareness Assistant, my purpose is to guide users through realistic scenarios where they may encounter cyber threats and help them make safe, informed decisions. I simulate everyday situations such as suspicious emails, unsafe links, or requests " +
                                "for personal information, allowing users to learn by actively engaging rather than passively reading. Through these interactions, I provide clear explanations, immediate feedback, and practical advice on topics like phishing, password security, and iden" +
                                "tifying online scams. My goal is not only to inform but to build users’ confidence and critical thinking skills, " +
                                "enabling them to recognize risks and protect themselves effectively in the digital world..");
        }
        else if (userInput.Contains("password"))
        {
            UIHelper.TypeText("Bot: Passwords are one of the most important ways to protect your personal information online, as they act like a lock that keeps your accounts secure. " +
                "A weak password makes it easy for hackers to guess or break into your accounts using automated tools that try thousands of combinations in seconds. " +
                "Simple passwords such as “password123,” your name, or your date of birth are very risky because they are easy to predict." +
                "\r\n\r\nTo stay safe, you should create strong passwords that are long and include a mix of uppercase letters, lowercase letters, numbers, and special characters. " +
                "For example, instead of using a single word, you can use a passphrase like “BlueCar!RunsFast92” which is much harder to guess but still easy to remember. " +
                "It is also very important not to reuse the same password across multiple accounts. If one account gets hacked, all your other accounts using the same password could also be accessed." +
                "\r\n\r\nIn addition, you should never share your password with anyone, even friends, and avoid writing it down in places where others can find it. " +
                "Using tools like password managers can help you store your passwords securely. I also recommend changing your passwords regularly, especially if you suspect that your account may have been compromised. " +
                "Treat your password like a secret key — the stronger and more protected it is, the safer your digital life will be.");
        }
        else if (userInput.Contains("phishing"))
        {
            UIHelper.TypeText("Bot: Phishing emails are carefully designed fake messages created by cybercriminals to trick you into giving away sensitive information such as passwords, banking details, or personal data. " +
                "These emails often pretend to come from trusted organisations like your bank, a delivery company, or even your school. They may use logos, official-looking language, and email addresses that look almost real to gain your trust." +
                " A common trick used in phishing emails is creating a sense of urgency or fear, for example saying “Your account has been locked” or “You must verify your details immediately.” This pressure is meant to make you act quickly without thinking." +
                "\r\n\r\nAs your Cybersecurity Awareness Assistant, I advise you to slow down and carefully examine any email before taking action. Check the sender’s email address closely, as fake emails often have small spelling differences. Look out for poor grammar, unusual requests, or links that do not match the official website." +
                " Never click on links or download attachments from emails you do not trust, as they may install harmful software or take you to fake websites designed to steal your information. If you are unsure, it is always safer to contact the company directly using their official website or phone number instead of replying to the email." +
                " Remember, real organisations will never pressure you to share sensitive information urgently.");
        }
        else if (userInput.Contains("safe browsing") || userInput.Contains("suspicious links"))
        {
            UIHelper.TypeText("Bot::Suspicious links are one of the most common ways cybercriminals trick people into visiting harmful websites. " +
                "These links may appear in emails, text messages, social media posts, or pop-up ads. At first glance, they may look safe or familiar, but they often lead to fake websites that are designed to steal your personal information or infect your device with malware." +
                "\r\n\r\nOne common trick is to slightly change the spelling of a well-known website, such as replacing letters or adding extra words (for example, “faceb00k-login.com” instead of the real site). Some links are also shortened using tools, which makes it difficult to see the actual destination. " +
                "Others may promise rewards, prizes, or urgent actions to get you to click quickly without thinking.\r\n\r\nAs your Cybersecurity Awareness Assistant, I advise you to always pause before clicking on any link. If possible, hover over the link to see the full web address and check if it matches the official website. " +
                "Be cautious of links that look unusual, contain random characters, or come from unknown sources. Avoid clicking on links that create urgency or seem too good to be true, such as winning a prize you did not enter for. Instead of clicking a link, it is safer to manually type the official website address into your browser. " +
                "By being careful and observant, you can prevent hackers from gaining access to your personal information.");
        }
        else
        {
            UIHelper.TypeText("Bot: I don’t understand that. Could you rephrase?");
        }
    }

    // Plays a welcome voice message
    private void PlayVoiceGreeting()
    {
        string soundFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"greeting.wav");
        if (File.Exists(soundFilePath))
        {
            using (SoundPlayer player = new SoundPlayer(soundFilePath))
            {
                player.PlaySync(); // Play the sound synchronously
            }
        }
        else
        {
            //Output error message if the sound file is missing
            Console.WriteLine($"[ERROR] greeting.wav not found at: {soundFilePath}");

            UIHelper.TypeText("[System] Welcome voice file missing. Text-only mopde active.");
        }

    }
}