using System.Media;
using System;
using System.Collections.Generic;

namespace CybersecurityChatBot1599
{
    public class ChatBot
    {
        // Day 2 Core States
        private string _userName = "";
        private string _lastDiscussedTopic = "";
        private readonly Dictionary<string, List<string>> _knowledgeBase;
        private readonly Dictionary<string, int> _historyTracker;
        private readonly Random _randomProvider;

        // Day 4 Matrix States
        private bool _isSimulationActive = false;
        private int _currentScenarioStage = 0;

        // 🌟 Day 5 Final States: Performance Scorecard Metrics
        private int _simulationsPassed = 0;
        private int _simulationsFailed = 0;
        private int _totalQuestionsAsked = 0;

        public ChatBot()
        {
            _randomProvider = new Random();
            _historyTracker = new Dictionary<string, int>();

            _knowledgeBase = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", new List<string> { "Use a sentence instead of a word! 'MyDogLovesTacos2026!' is incredibly strong.", "Never reuse passwords across accounts. A single breach could compromise everything." } },
                { "phishing", new List<string> { "Look out for urgent language like 'Your account is locked!' Scammers rely on panic.", "Always inspect the sender's domain. 'support@paypal-secure-update.com' is fake." } },
                { "scam", new List<string> { "If an offer sounds too good to be true, it's a trap. Crypto giveaways are always fake.", "No legitimate company will ever demand payment via gift cards or crypto." } }
            };
        }

        public void PlayVoiceGreeting()
        {
            try
            {
                // Tells the app to look for the file inside the running application folder
                string audioPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                using (SoundPlayer player = new SoundPlayer(audioPath))
                {
                    player.Play(); // Plays the file smoothly without freezing the UI window
                }
            }
            catch (Exception)
            {
                // Defensive safeguard: Prevents a crash if the file is missing or misplaced
            }
        }

        public string ProcessUserInput(string rawInput)
        {
            string cleanInput = rawInput.Trim();
            string lowerInput = cleanInput.ToLower();

            // 1. Core Profile Name Setup
            if (string.IsNullOrEmpty(_userName))
            {
                _userName = cleanInput;
                return $"Access Granted, Agent {_userName}!\n\nType **/help** to view all available terminal commands, or type **SIMULATE** to begin your live-fire evaluation framework.";
            }

            // 🌟 2. Day 5 System Command Matrix Interception
            if (lowerInput.StartsWith("/"))
            {
                return HandleSystemCommands(lowerInput);
            }

            // 3. Simulation Overrides
            if (_isSimulationActive)
            {
                return EvaluateSimulationChoice(lowerInput);
            }

            // 4. Initialization Configuration Trigger
            if (lowerInput == "simulate" || lowerInput == "test")
            {
                _isSimulationActive = true;
                _currentScenarioStage = 1;
                return "🚨 **LIVE-FIRE THREAT SIMULATION: STAGE 1** 🚨\n\n" +
                       "**Scenario:** You receive an urgent email from 'IT-Support-Matrix' claiming your work password expires in 10 minutes. It includes a link: `http://update-your-matrix-credentials.com`.\n\n" +
                       "What do you do?\n" +
                       "👉 Type **1** to click the link and change it quickly.\n" +
                       "👉 Type **2** to report the message to your security team.";
            }

            // 5. Base Module Information Processing
            foreach (var keyword in _knowledgeBase.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    _totalQuestionsAsked++;
                    _lastDiscussedTopic = keyword;
                    return GetRotatedTip(keyword);
                }
            }

            return $"Observation logged, Agent {_userName}. Type **/help** to see what I can do, or type **SIMULATE** to test your defensive readiness scores.";
        }

        // 🌟 DAY 5 FEATURE: SYSTEM UTILITY COMMAND HANDLER
        private string HandleSystemCommands(string command)
        {
            if (command == "/help")
            {
                return "📋 **E-BOT CORE TERMINAL COMMANDS** 📋\n\n" +
                       "🔹 **SIMULATE** - Launches the multi-stage interactive threat scenario.\n" +
                       "🔹 **/status** - Displays your real-time security performance scorecard.\n" +
                       "🔹 **/topics** - Lists all cybersecurity categories built into my memory.\n" +
                       "🔹 **password, phishing, scam** - Type these keywords directly for tips.";
            }
            if (command == "/status")
            {
                string rating = (_simulationsPassed > 0 && _simulationsFailed == 0) ? "🛡️ Certified Defender" : "⚠️ Vulnerable / Needs Training";
                if (_simulationsPassed == 0 && _simulationsFailed == 0) rating = "⚪ Unrated (No tests completed)";

                return "📊 **AGENT SECURITY SCORECARD** 📊\n\n" +
                       $"👤 **Agent Identity:** {_userName}\n" +
                       $"✅ **Simulations Passed:** {_simulationsPassed}\n" +
                       $"❌ **Simulations Failed:** {_simulationsFailed}\n" +
                       $"💬 **Total Research Inquiries:** {_totalQuestionsAsked}\n" +
                       $"🎯 **Current Security Status:** {rating}";
            }
            if (command == "/topics")
            {
                return "📚 **AVAILABLE SECURITY KNOWLEDGE INTERFACES** 📚\n\n" +
                       "You can ask me questions regarding these specific keywords:\n" +
                       "🔒 **Password Security** (Type: password)\n" +
                       "🎣 **Phishing Auditing** (Type: phishing)\n" +
                       "💳 **Financial Fraud Scams** (Type: scam)";
            }

            return "❌ **Unknown System Command.** Type **/help** for a list of valid terminal operations.";
        }

        private string EvaluateSimulationChoice(string choice)
        {
            if (_currentScenarioStage == 1)
            {
                if (choice == "1")
                {
                    _isSimulationActive = false;
                    _currentScenarioStage = 0;
                    _simulationsFailed++; // Metric updated
                    return "❌ **SIMULATION FAILED AT STAGE 1** ❌\n\n" +
                           "You clicked the phishing link! Your system has been compromised by dummy malware. Type **/status** to see your scorecard updated.";
                }
                if (choice == "2")
                {
                    _currentScenarioStage = 2;
                    return "⚡ **STAGE 1 PASSED! Great Reflexes.** ⚡\n\n" +
                           "🚨 **LIVE-FIRE THREAT SIMULATION: STAGE 2** 🚨\n\n" +
                           "**Scenario:** You find an unlabelled USB flash drive sitting on the elevator floor with a sticky note reading: *'Executive Salary Review 2026'*.\n\n" +
                           "What do you do?\n" +
                           "👉 Type **1** to plug it into your workstation to inspect it.\n" +
                           "👉 Type **2** to hand it over to physical corporate security.";
                }
                return "⚠️ Invalid matrix selection. Please enter **1** or **2**.";
            }

            if (_currentScenarioStage == 2)
            {
                _isSimulationActive = false;
                _currentScenarioStage = 0;

                if (choice == "1")
                {
                    _simulationsFailed++; // Metric updated
                    return "❌ **SIMULATION FAILED AT STAGE 2** ❌\n\n" +
                           "Oh no! You plugged a mystery USB drive into a network terminal, triggering a hardware exploit. Type **/status** to view your record.";
                }
                if (choice == "2")
                {
                    _simulationsPassed++; // Metric updated
                    return "🏆 **COMPLETION SUCCESSFUL: PERFECT SECURITY SCORE!** 🏆\n\n" +
                           "Sensational work! You successfully navigated the threat matrix safely. Check your updated record with **/status**!";
                }

                _isSimulationActive = true;
                _currentScenarioStage = 2;
                return "⚠️ Invalid matrix selection. Please enter **1** or **2**.";
            }

            return "Error in simulation engine routing logs.";
        }

        private string GetRotatedTip(string category)
        {
            var tips = _knowledgeBase[category];
            if (!_historyTracker.ContainsKey(category)) _historyTracker[category] = 0;

            int targetedIndex = _historyTracker[category];
            string assignedTip = tips[targetedIndex];

            _historyTracker[category] = (targetedIndex + 1) % tips.Count;
            return $"🤖 Security Advisor Trace [{category.ToUpper()}]: {assignedTip}";
        }
    }
}