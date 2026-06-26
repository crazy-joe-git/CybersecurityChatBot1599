using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace CybersecurityChatBot1599
{
    public class ChatBot
    {
        private string _userName = "";
        private string _favoriteTopic = "";
        private int _queryCount = 0;
        private int _passedSimulations = 0;

        private string _currentTopic = "none";
        private bool _isFirstMessage = true;
        private bool _isInSimulationMode = false;

        // Keep the player as a class member variable so it does not get garbage collected
        private SoundPlayer _startupPlayer;

        // Public method to be called directly when the WPF Window launches
        public void PlayStartupAudio()
        {
            try
            {
                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                string path = Path.Combine(exeDir, "greeting.wav");

                if (File.Exists(path))
                {
                    _startupPlayer = new SoundPlayer(path);
                    _startupPlayer.Play(); // Plays asynchronously without freezing the UI
                }
            }
            catch
            {
                
            }
        }

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "I didn't catch that. Please type a message or a system command like /help.";
            }

            string cleanInput = userInput.Trim().ToLower();
            _queryCount++;

            // Capture and store the user's name
            if (_isFirstMessage)
            {
                _userName = userInput.Trim();
                _isFirstMessage = false;

                // Audio call removed from here since it now fires at app launch
                return $"Welcome, {_userName}! \r\n\r\nGreat to have you here!\r\n\r\nI'm E-Bot, and my mission is to help you become smarter and safer online.\r\n\r\nWhether you're worried about phishing, scams, fake links, or password security, I'm here to guide you with practical tips and interactive learning.\r\n\r\nWhenever you're ready:\r\n• Type HELP to see everything I can do.\r\n• Type SIMULATE to test your cybersecurity skills in a realistic scenario.\r\n\r\nLet's get started and strengthen your cyber awareness! 🛡️";
            }

            if (cleanInput.StartsWith("/"))
            {
                return HandleSlashCommands(cleanInput);
            }

            if (cleanInput == "simulate" || cleanInput == "test")
            {
                _isInSimulationMode = true;
                return "[SIMULATION MODE STARTING]\nStage 1: You receive an email from your CEO asking for immediate iTunes gift cards to close a client deal. Do you:\n[A] Reply with the details\n[B] Report it to your IT Security Team";
            }

            if (_isInSimulationMode)
            {
                return HandleSimulationLogic(cleanInput);
            }

            if (cleanInput.Contains("interested in") || cleanInput.Contains("favorite topic is"))
            {
                ExtractAndSaveFavoriteTopic(cleanInput);
            }

            string sentimentPrefix = DetectAndRespondToSentiment(cleanInput);

            bool isAskingFollowUp = cleanInput.Contains("tell me more") ||
                                    cleanInput.Contains("explain more") ||
                                    cleanInput.Contains("give me another tip") ||
                                    cleanInput.Contains("explain further");

            if (isAskingFollowUp)
            {
                if (_currentTopic == "none")
                {
                    return "I'd love to explain further! What specific topic would you like to know more about? You can ask about passwords, phishing, scams, or privacy.";
                }
                return GetFollowUpTip(_currentTopic);
            }

            string coreAnswer = "";

            if (cleanInput.Contains("password"))
            {
                _currentTopic = "password";
                coreAnswer = "Password Safety: Strong passwords are your first line of defense against cyber attacks. Always create unique passphrases that combine uppercase and lowercase letters, numbers, and special symbols. Avoid using personal information such as birthdays, names, or common words that attackers can easily guess. Most importantly, never reuse the same password across multiple platforms because a breach on one service could expose your accounts elsewhere.";
            }
            else if (cleanInput.Contains("phishing") || cleanInput.Contains("link") || cleanInput.Contains("email"))
            {
                _currentTopic = "phishing";
                coreAnswer = "Phishing Audit: Phishing attacks are designed to trick users into revealing sensitive information or downloading malicious software. Always inspect sender email addresses carefully and look for unusual spelling, grammar mistakes, or suspicious domains. If an unexpected message creates a sense of urgency, requests confidential information, or contains unfamiliar links, treat it with caution and verify its legitimacy through official channels before taking action.";
            }
            else if (cleanInput.Contains("scam") || cleanInput.Contains("money") || cleanInput.Contains("gift card"))
            {
                _currentTopic = "scam";
                coreAnswer = "Financial Fraud: Cybercriminals often use fear, urgency, or promises of rewards to manipulate victims into sending money. Legitimate technical support teams, banks, government agencies, and reputable organizations will never demand payment through cryptocurrency, wire transfers, gift cards, or other difficult-to-trace methods. Whenever a payment request seems unusual, pause, verify the source independently, and seek confirmation before proceeding.";
            }
            else if (cleanInput.Contains("privacy"))
            {
                _currentTopic = "privacy";
                coreAnswer = "Data Privacy: Protecting your personal information online is essential for reducing the risk of identity theft and social engineering attacks. Limit the amount of personal information you share publicly, including birthdays, addresses, phone numbers, and details about your daily activities. Cybercriminals often examine social media profiles to gather information that can help them answer security questions, impersonate victims, or craft convincing scams.";
            }
            else
            {
                if (!string.IsNullOrEmpty(_favoriteTopic))
                {
                    return $"I am keeping an eye on your preference for {_favoriteTopic}, {_userName}. Ask me a specific security query about it or look at your scorecard using /status.";
                }
                return $"Observation logged, {_userName}. Type \"/help\" to see what I can do, or type SIMULATE to test your defensive readiness scores.";
            }

            return $"{sentimentPrefix}{coreAnswer}";
        }

        private string DetectAndRespondToSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid") || input.Contains("anxious"))
            {
                return "Notice: It's completely understandable to feel worried about online threats. Cybercriminals often rely on confusion and fear to make people act quickly without thinking. The good news is that understanding common attack techniques can significantly reduce your risk. Let me share some practical guidance to help you stay protected:\n\n";
            }
            if (input.Contains("curious") || input.Contains("interested") || input.Contains("want to learn"))
            {
                return "Notice: Curiosity is one of the strongest cybersecurity skills you can have. People who actively learn about online threats are often better prepared to recognize and avoid them. Let's explore this topic in more detail:\n\n";
            }
            if (input.Contains("frustrated") || input.Contains("confused") || input.Contains("stuck") || input.Contains("annoyed"))
            {
                return "Notice: Cybersecurity concepts can seem complex at first because there are many technical terms and evolving threats. Don't worry—every expert started as a beginner. Let's simplify the topic and focus on the key points that matter most:\n\n";
            }
            return "";
        }

        private string GetFollowUpTip(string topic)
        {
            string personalization = string.IsNullOrEmpty(_favoriteTopic) ? "" : $" Since you are tracking {_favoriteTopic}, this aligns well with your learning goals:";

            switch (topic)
            {
                case "password":
                    return $"Expanding further on Password Security.{personalization} Consider using a reputable Password Manager to generate and securely store complex passwords. This allows you to maintain unique credentials for every account without needing to memorize them all. Enabling Multi-Factor Authentication (MFA) wherever possible provides an additional layer of protection even if a password becomes compromised.";
                case "phishing":
                    return $"Expanding further on Phishing Awareness.{personalization} Always verify domains carefully before clicking links or downloading attachments. Attackers frequently create websites and email addresses that closely resemble legitimate organizations. Hovering over links before clicking and independently visiting official websites can help you avoid credential theft and malware infections.";
                case "scam":
                    return $"Expanding further on Financial Scams.{personalization} Scammers often rely on emotional pressure, urgency, and fear to influence decision-making. If someone claims immediate payment is required to avoid legal action, account suspension, or other consequences, take a step back and verify the situation independently. Legitimate organizations provide official verification methods and rarely demand instant payment.";
                case "privacy":
                    return $"Expanding further on Privacy Protection.{personalization} Regularly review the privacy and permission settings of your applications and online accounts. Disable unnecessary access to location data, contacts, cameras, and microphones when they are not required. Reducing the amount of information available about you online helps minimize your exposure to targeted attacks and identity theft.";
                default:
                    return "I am ready when you are. Tell me more about the cybersecurity topic you would like to explore, and I can provide additional guidance, examples, and best practices.";
            }
        }

        private void ExtractAndSaveFavoriteTopic(string input)
        {
            if (input.Contains("password")) _favoriteTopic = "passwords";
            else if (input.Contains("phishing")) _favoriteTopic = "phishing security";
            else if (input.Contains("scam")) _favoriteTopic = "fraud prevention";
            else if (input.Contains("privacy")) _favoriteTopic = "data privacy";
        }

        private string HandleSlashCommands(string command)
        {
            if (command == "/help")
            {
                return "E-BOT CORE TERMINAL COMMANDS\n\n- SIMULATE - Launches the multi-stage interactive threat scenario.\n- /status - Displays your real-time security performance scorecard.\n- /topics - Lists all cybersecurity categories built into my memory.\n- password, phishing, scam, privacy - Type these keywords directly for tips.";
            }
            if (command == "/topics")
            {
                return "E-BOT KNOWLEDGE ARRAYS\n\n1. Password Architecture\n2. Phishing Domain Audits\n3. Financial Fraud Detection\n4. Social Media Data Privacy Profile";
            }
            if (command == "/status")
            {
                string topicTracking = string.IsNullOrEmpty(_favoriteTopic) ? "None Selected" : _favoriteTopic;
                string ranking = _passedSimulations >= 1 ? "Elite Field Defender" : "Novice Trainee Agent";
                return $"USER SECURITY SCORECARD\n-------------------------\nUser Identity: {_userName}\nSaved Focus Area: {topicTracking}\nTotal Queries Submitted: {_queryCount}\nPassed Drill Scenarios: {_passedSimulations}/1\nAssigned Profile Ranking: {ranking}";
            }
            return "Unknown system command string. Type /help to see valid execution flags.";
        }

        private string HandleSimulationLogic(string input)
        {
            _isInSimulationMode = false;
            if (input == "b")
            {
                _passedSimulations++;
                return "Correct! Reporting the suspicious inquiry to IT isolates the threat vector safely. Your scorecard status has been updated. Type /status to view it!";
            }
            if (input == "a")
            {
                return "Incorrect. Sending gift card codes results in an asset and data leak. The threat succeeded. Practice your defensive tips or run SIMULATE to try again.";
            }
            _isInSimulationMode = true;
            return "Invalid selection loop. Please answer with option [A] or [B].";
        }
    }
}