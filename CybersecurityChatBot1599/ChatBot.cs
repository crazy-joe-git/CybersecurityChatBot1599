using System;
using System.Collections.Generic;

namespace CybersecurityChatBot1599
{
    public class ChatBot
    {
        // Day 2 States
        private string _userName = "";
        private string _lastDiscussedTopic = "";
        private readonly Dictionary<string, List<string>> _knowledgeBase;
        private readonly Dictionary<string, int> _historyTracker;
        private readonly Random _randomProvider;

        // Day 4 States: Multi-Stage Simulation Engine
        private bool _isSimulationActive = false;
        private int _currentScenarioStage = 0;

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
            // Optional voice greetings placeholder
        }

        public string ProcessUserInput(string rawInput)
        {
            string cleanInput = rawInput.Trim();
            string lowerInput = cleanInput.ToLower();

            // 1. Capture Identity Name Registration
            if (string.IsNullOrEmpty(_userName))
            {
                _userName = cleanInput;
                return $"Access Granted, Agent {_userName}! Ask me about 'passwords', 'phishing', or 'scams' to begin core modules. Alternatively, type **SIMULATE** to launch a live-fire security test!";
            }

            // 2. Intercept for Active Live-Fire Simulations
            if (_isSimulationActive)
            {
                return EvaluateSimulationChoice(lowerInput);
            }

            // 3. Check for Simulation Initialization Trigger
            if (lowerInput == "simulate" || lowerInput == "test")
            {
                _isSimulationActive = true;
                _currentScenarioStage = 1; // Set to Stage 1
                return "🚨 **LIVE-FIRE THREAT SIMULATION: STAGE 1** 🚨\n\n" +
                       "**Scenario:** You receive an urgent email from 'IT-Support-Matrix' claiming your work password expires in 10 minutes. It includes a link: `http://update-your-matrix-credentials.com`.\n\n" +
                       "What do you do?\n" +
                       "👉 Type **1** to click the link and change it quickly.\n" +
                       "👉 Type **2** to report the message to your security team.";
            }

            // 4. Default Knowledge Retrieval Processing Pipeline
            foreach (var keyword in _knowledgeBase.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    _lastDiscussedTopic = keyword;
                    return GetRotatedTip(keyword);
                }
            }

            return $"Understood, Agent {_userName}. I filed that under our active observation logs. Type **SIMULATE** if you want to test your reflexes against a live hacker drill!";
        }

        private string EvaluateSimulationChoice(string choice)
        {
            // ==========================================
            // EVALUATING STAGE 1: PHISHING EMAIL LINK
            // ==========================================
            if (_currentScenarioStage == 1)
            {
                if (choice == "1")
                {
                    _isSimulationActive = false; // Reset State
                    _currentScenarioStage = 0;
                    return "❌ **SIMULATION FAILED AT STAGE 1** ❌\n\n" +
                           "You clicked the phishing link! The domain was fake and used urgency to bypass your judgment. Your system has been compromised by dummy malware. Type **SIMULATE** to retry.";
                }
                if (choice == "2")
                {
                    _currentScenarioStage = 2; // Advance State Machine to Stage 2!
                    return "⚡ **STAGE 1 PASSED! Great Reflexes.** ⚡\n\n" +
                           "Reporting the email lets IT block the threat corporate-wide. But the attacker isn't stopping...\n\n" +
                           "🚨 **LIVE-FIRE THREAT SIMULATION: STAGE 2** 🚨\n\n" +
                           "**Scenario:** Later that afternoon, you walk into the corporate building elevator and spot an unlabelled USB flash drive sitting on the floor. A handwritten sticky note on it reads: *'Executive Salary Review 2026'*.\n\n" +
                           "What do you do?\n" +
                           "👉 Type **1** to plug it into your workstation privately to figure out who dropped it so you can return it.\n" +
                           "👉 Type **2** to hand it over to the physical security desk without plugging it into anything.";
                }

                return "⚠️ Invalid matrix selection. Please enter **1** or **2** to resolve Threat Simulation Stage 1.";
            }

            // ==========================================
            // EVALUATING STAGE 2: PHYSICAL USB THREAT
            // ==========================================
            if (_currentScenarioStage == 2)
            {
                _isSimulationActive = false; // Simulation finishes after this evaluation (Pass or Fail)
                _currentScenarioStage = 0;

                if (choice == "1")
                {
                    return "❌ **SIMULATION FAILED AT STAGE 2** ❌\n\n" +
                           "Oh no! You plugged a mystery USB drive into a corporate network terminal! Attackers intentionally leave tempting drives in public spaces ('USB Drop Attacks'). The moment it was connected, it simulated a malicious keyboard attack to steal network tokens. Better luck next time!";
                }
                if (choice == "2")
                {
                    return "🏆 **COMPLETION SUCCESSFUL: PERFECT SECURITY SCORE!** 🏆\n\n" +
                           "Sensational work, Agent! By turning the drive in clean, you avoided a dangerous hardware-level exploit and kept your environment safe. You have successfully navigated the entire threat matrix!";
                }

                _isSimulationActive = true; // Restore state if invalid response
                _currentScenarioStage = 2;
                return "⚠️ Invalid matrix selection. Please enter **1** or **2** to resolve Threat Simulation Stage 2.";
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