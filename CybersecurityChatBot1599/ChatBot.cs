using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace CybersecurityChatBot1599
{
    public class ChatBot
    {
        // 1. CLASS MEMORY STATES: Track conversational progress across turns
        private string _userName = string.Empty;
        private bool _isNameCaptured = false;
        private string _lastDiscussedTopic = "general";
        private string _userFavoriteTopic = string.Empty;

        private readonly Random _randomProvider = new Random();

        // 2. DATA STRUCURES: Dictionaries for lookup performance & tracking
        private readonly Dictionary<string, List<string>> _knowledgeBase;
        private readonly Dictionary<string, int> _lastSelectedIndices = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public string UserName => _userName;
        public bool IsNameCaptured => _isNameCaptured;

        // 3. CONSTRUCTOR: Builds data matrices cleanly upon startup
        public ChatBot()
        {
            _knowledgeBase = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "password", new List<string>
                    {
                        "Make sure to use strong, unique passwords for each account. Avoid using personal details.",
                        "Consider using passphrases like 'BlueCar!RunsFast92' which are complex for algorithms but easy to remember.",
                        "Never reuse a password across different accounts. If one gets breached, your entire identity is compromised."
                    }
                },
                {
                    "phishing", new List<string>
                    {
                        "Be cautious of emails asking for personal details. Scammers often disguise themselves as trusted brands.",
                        "Check the sender's email domain address carefully. Phishing emails usually contain subtle spelling typos.",
                        "Look out for urgent language like 'Your account is locked!' Scammers use fear to make you act without thinking."
                    }
                },
                {
                    "scam", new List<string>
                    {
                        "Online scams often promise unexpected rewards. If an offer looks too good to be true, it always is.",
                        "Verify urgent requests for money through an official, independent communication channel before paying.",
                        "Cybercriminals create cloning pages to harvest credentials. Always check the browser address bar for valid HTTPS headers."
                    }
                },
                {
                    "privacy", new List<string>
                    {
                        "Protecting your privacy means auditing your social profiles. Keep sensitive personal data hidden from the public.",
                        "Review application tracking permissions on your mobile devices. Turn off background location access when unnecessary.",
                        "Data privacy is a crucial part of staying safe online. Regularly review the privacy settings across your accounts."
                    }
                }
            };
        }

        // 4. MAIN INTERACTION PIPELINE (Called directly by your UI Window layer)
        public string ProcessUserInput(string rawInput)
        {
            string cleanInput = rawInput.Trim();
            if (string.IsNullOrWhiteSpace(cleanInput))
                return "System: Input text cannot be completely empty.";

            // Profile Initialization
            if (!_isNameCaptured)
            {
                _userName = cleanInput;
                _isNameCaptured = true;
                return $"E-Bot: Welcome, {_userName}! Let's learn about cybersecurity. Ask me about passwords, phishing, scams, or privacy!";
            }

            string lowerInput = cleanInput.ToLowerInvariant();

            // Sentiment Processing Interception
            string? sentimentPrefix = EvaluateSentiment(lowerInput);
            if (sentimentPrefix != null)
            {
                string targetedTopic = "general";
                if (lowerInput.Contains("scam")) targetedTopic = "scam";
                else if (lowerInput.Contains("password")) targetedTopic = "password";
                else if (lowerInput.Contains("privacy")) targetedTopic = "privacy";
                else if (lowerInput.Contains("phishing")) targetedTopic = "phishing";

                _lastDiscussedTopic = targetedTopic;
                return $"{sentimentPrefix} {GetTopicTip(targetedTopic)}";
            }

            // Conversational Continuity 
            if (lowerInput.Contains("explain more") || lowerInput.Contains("tell me more") || lowerInput.Contains("give me another tip"))
            {
                return $"E-Bot: Expanding on our current topic thread... {GetTopicTip(_lastDiscussedTopic)}";
            }

            // Core Keyword Matching Engine
            if (lowerInput.Contains("password"))
            {
                _lastDiscussedTopic = "password";
                return $"E-Bot: {_userName}, {GetTopicTip("password")}";
            }
            if (lowerInput.Contains("phishing"))
            {
                _lastDiscussedTopic = "phishing";
                return $"E-Bot: {GetTopicTip("phishing")}";
            }
            if (lowerInput.Contains("scam"))
            {
                _lastDiscussedTopic = "scam";
                return $"E-Bot: {GetTopicTip("scam")}";
            }
            if (lowerInput.Contains("privacy") || lowerInput.Contains("interested in privacy"))
            {
                _lastDiscussedTopic = "privacy";

                if (string.IsNullOrEmpty(_userFavoriteTopic))
                {
                    _userFavoriteTopic = "privacy";
                    return $"E-Bot: Great! I'll remember that you're interested in privacy. It's a crucial part of staying safe online. {GetTopicTip("privacy")}";
                }
                return $"E-Bot: As someone interested in privacy, you might want to review this: {GetTopicTip("privacy")}";
            }

            // Basic Informational Fallbacks
            if (lowerInput.Contains("how are you"))
                return $"E-Bot: I'm functioning perfectly, {_userName}!";

            if (lowerInput.Contains("cybersecurity"))
                return $"E-Bot: Hello, {_userName}! Cybersecurity is the ongoing practice of shielding networks, systems, and programs from digital attacks.";

            return "E-Bot: I'm not sure I understand. Can you try asking about passwords, phishing, scams, or privacy?";
        }

        // 5. HELPER METHODS: Isolated tasks that support processing workflows
        private string? EvaluateSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid"))
            {
                return "E-Bot: It's completely understandable to feel worried about online threats. Scammers can be very convincing, but awareness is your best shield.";
            }
            if (input.Contains("frustrated") || input.Contains("annoyed") || input.Contains("confused"))
            {
                return "E-Bot: Digital security can feel incredibly frustrating and overwhelming when terms get complex. Let's simplify this step-by-step.";
            }
            if (input.Contains("curious") || input.Contains("eager") || input.Contains("want to learn"))
            {
                return "E-Bot: I love that proactive curiosity! Developing an analytical mindset is exactly how you hunt down flaws and keep threats away.";
            }
            return null;
        }

        private string GetTopicTip(string topic)
        {
            if (_knowledgeBase.TryGetValue(topic, out List<string>? tips) && tips.Count > 0)
            {
                if (tips.Count == 1) return tips[0];

                int lastIndex = _lastSelectedIndices.ContainsKey(topic) ? _lastSelectedIndices[topic] : -1;
                int randomIndex;

                // Smart Forced Re-roll Loop: Ensures consecutive duplicate protection
                do
                {
                    randomIndex = _randomProvider.Next(tips.Count);
                } while (randomIndex == lastIndex);

                _lastSelectedIndices[topic] = randomIndex;
                return tips[randomIndex];
            }

            return "Always verify security certificates, avoid open public Wi-Fi networks when accessing sensitive accounts, and keep software updated.";
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
                        player.Play();
                    }
                }
                catch { /* Fail silently if system audio hardware is missing */ }
            }
        }
    }
}