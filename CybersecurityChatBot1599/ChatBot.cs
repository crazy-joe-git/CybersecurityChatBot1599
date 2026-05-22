using System;
using System.IO;
using System.Media;

namespace CybersecurityChatBot1599
{
    public class ChatBot
    {
        private string _userName = string.Empty;
        private bool _isNameCaptured = false;

        public string UserName => _userName;
        public bool IsNameCaptured => _isNameCaptured;

        // Decoupled logic engine processing inputs and modifying internal context
        public string ProcessUserInput(string rawInput)
        {
            string cleanInput = rawInput.Trim();
            if (string.IsNullOrWhiteSpace(cleanInput))
                return "System: Input text cannot be completely empty.";

            if (!_isNameCaptured)
            {
                _userName = cleanInput;
                _isNameCaptured = true;
                return $"E-Bot: Welcome, {_userName}! Let's learn about cybersecurity. Ask me about topics like passwords, phishing, or safe browsing!";
            }

            return EvaluateResponse(cleanInput.ToLowerInvariant());
        }

        private string EvaluateResponse(string input)
        {
            // Core matching conditions preserved from Part 1 logic
            if (input.Contains("how are you"))
                return $"E-Bot: I'm functioning perfectly, {_userName}!";

            if (input.Contains("cybersecurity"))
                return $"E-Bot: Hello, {_userName}! Cybersecurity is the practice of protecting computers, networks, and data from digital attacks.";

            if (input.Contains("purpose"))
                return $"E-Bot: As an Assistant, my purpose is to guide users through realistic cyber threat scenarios and build safe habits.";

            if (input.Contains("password"))
                return $"E-Bot: Passwords act like locks... Keep them strong, unique, and long. Avoid simple patterns like your birthday.";

            if (input.Contains("phishing"))
                return $"E-Bot: Phishing messages mimic trusted sources to steal access data... Examine sender handles carefully.";

            if (input.Contains("safe browsing") || input.Contains("suspicious links"))
                return $"E-Bot: Suspicious links guide users to dangerous cloning destinations. Always check URLs before executing a click.";

            return $"E-Bot: I don’t understand that topic rule yet. Could you please rephrase, {_userName}?";
        }

        public void PlayVoiceGreeting()
        {
            string soundFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
            if (File.Exists(soundFilePath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(soundFilePath))
                    {
                        // Swapped to Play() to keep sound streaming completely asynchronous
                        player.Play();
                    }
                }
                catch
                {
                    // Fail silently if device drivers are missing or media is corrupt
                }
            }
        }
    }
}