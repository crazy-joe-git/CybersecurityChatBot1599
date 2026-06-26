using System;
using System.Collections.Generic;

namespace CybersecurityChatBot1599
{
    // Step 3.2 - QuizQuestion model class as required by the POE guide
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; } // E.g., "A", "B", "C", "D" or "True", "False"
        public string Explanation { get; set; }  // Shown immediately after answering
        public bool IsTrueFalse { get; set; }
        public string Difficulty { get; set; }   // Easy, Medium, Hard tracking
    }

    // Step 3.1 - QuizManager core engine class
    public class QuizManager
    {
        private List<QuizQuestion> _questions;
        private int _currentIndex = 0;
        private int score = 0;

        // Constructor: Populating the bank with 12 distinct, multi-tier questions
        public QuizManager()
        {
            _questions = new List<QuizQuestion>
            {
                // ==================== EASY QUESTIONS ====================
                new QuizQuestion
                {
                    Question = "What should you do if you receive an unexpected email asking for your account password?",
                    Options = new List<string> { "A) Reply directly with your current password", "B) Delete the email immediately", "C) Report the email to your security team as phishing", "D) Ignore it and check back later" },
                    CorrectAnswer = "C",
                    Explanation = "Correct! Reporting suspicious emails immediately helps prevent organizational scams and alerts administrators.",
                    IsTrueFalse = false,
                    Difficulty = "Easy"
                },
                new QuizQuestion
                {
                    Question = "Using the same strong password across multiple different accounts is a secure cybersecurity practice.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Explanation = "Correct! Reusing passwords exposes you to credential stuffing attacks. If one site is breached, attackers try that password on all your other accounts.",
                    IsTrueFalse = true,
                    Difficulty = "Easy"
                },
                new QuizQuestion
                {
                    Question = "When browsing websites, what does the 'S' stand for in the 'HTTPS' prefix?",
                    Options = new List<string> { "A) Standard", "B) Secure", "C) System", "D) Speed" },
                    CorrectAnswer = "B",
                    Explanation = "Correct! The 'S' stands for Secure, meaning the connection between your web browser and the server is encrypted.",
                    IsTrueFalse = false,
                    Difficulty = "Easy"
                },
                new QuizQuestion
                {
                    Question = "Phishing attacks can only happen through email communication.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Explanation = "Correct! Phishing can happen over SMS (Smishing), phone calls (Vishing), or even direct messages on social media platforms.",
                    IsTrueFalse = true,
                    Difficulty = "Easy"
                },

                // ==================== MEDIUM QUESTIONS ====================
                new QuizQuestion
                {
                    Question = "Which of the following is generally considered the LEAST secure method of Multi-Factor Authentication (MFA)?",
                    Options = new List<string> { "A) SMS-based text message verification codes", "B) Hardware security keys (e.g., YubiKey)", "C) Mobile Authenticator Apps (e.g., Google Authenticator)", "D) Biometric authentication (Fingerprint / FaceID)" },
                    CorrectAnswer = "A",
                    Explanation = "Correct! SMS verification can be intercepted via SIM-swapping or network routing exploits, making it less secure than app or hardware alternatives.",
                    IsTrueFalse = false,
                    Difficulty = "Medium"
                },
                new QuizQuestion
                {
                    Question = "Social engineering attacks rely entirely on technical software vulnerabilities rather than psychological manipulation.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Explanation = "Correct! Social engineering specifically exploits human psychology—such as trust, fear, or urgency—to trick users into breaking security protocols.",
                    IsTrueFalse = true,
                    Difficulty = "Medium"
                },
                new QuizQuestion
                {
                    Question = "If your machine is infected with Ransomware, paying the threat actors ensures your files will be safely decrypted.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Explanation = "Correct! Paying cybercriminals does not guarantee decryption. Many victims receive corrupted keys or are immediately targeted again for more money.",
                    IsTrueFalse = true,
                    Difficulty = "Medium"
                },
                new QuizQuestion
                {
                    Question = "When adjusting your privacy settings on public social platforms, what is the safest data exposure baseline?",
                    Options = new List<string> { "A) Public (Visible to everyone online)", "B) Friends of Friends", "C) Private / Friends Only", "D) Custom shared with all network networks" },
                    CorrectAnswer = "C",
                    Explanation = "Correct! Keeping profiles restricted to explicitly verified friends minimizes the public footprint accessible to bad actors for profiling.",
                    IsTrueFalse = false,
                    Difficulty = "Medium"
                },

                // ==================== HARD QUESTIONS ====================
                new QuizQuestion
                {
                    Question = "What does the industry-standard '3-2-1 backup strategy' explicitly recommend?",
                    Options = new List<string> { "A) 3 active versions, on 2 external cloud networks, monitored by 1 admin", "B) 3 copies of data, across 2 different media types, with 1 copy stored completely off-site", "C) 3 encrypted drives, running on 2 localized PCs, backed up 1 time per week", "D) 3 separate server clusters, using 2 network switches, inside 1 data facility" },
                    CorrectAnswer = "B",
                    Explanation = "Correct! The 3-2-1 rule provides robust redundancy: 3 total copies, 2 different storage types (e.g., NAS and cloud), and 1 safely isolated off-site.",
                    IsTrueFalse = false,
                    Difficulty = "Hard"
                },
                new QuizQuestion
                {
                    Question = "An attacker crafts a highly tailored spear-phishing attack specifically targeting executive board members using leaked internal metrics. This variant is known as:",
                    Options = new List<string> { "A) Pharming", "B) Baiting", "C) Whaling", "D) Vishing" },
                    CorrectAnswer = "C",
                    Explanation = "Correct! Whaling is a specific subset of spear-phishing engineered exclusively to catch high-profile corporate 'whales' like CEOs and CFOs.",
                    IsTrueFalse = false,
                    Difficulty = "Hard"
                },
                new QuizQuestion
                {
                    Question = "Which automated cryptographic attack variant attempts to gain access by sequentially trying lists of pre-compiled leaked words and common dictionary passwords?",
                    Options = new List<string> { "A) Brute Force Attack", "B) Dictionary Attack", "C) Man-in-the-Middle Attack", "D) Replay Attack" },
                    CorrectAnswer = "B",
                    Explanation = "Correct! A dictionary attack streamlines brute-forcing by specifically cycling through known words and compromised real-world string patterns.",
                    IsTrueFalse = false,
                    Difficulty = "Hard"
                },
                new QuizQuestion
                {
                    Question = "An unauthorized actor closely shadows a physical employee through a secure security gate without scanning credentials. What is this physical breach tactic called?",
                    Options = new List<string> { "A) Pretexting", "B) Baiting", "C) Tailgating / Piggybacking", "D) Shoulder Surfing" },
                    CorrectAnswer = "C",
                    Explanation = "Correct! Tailgating involves bypassing physical authorization parameters by slipping in directly behind a valid credential holder.",
                    IsTrueFalse = false,
                    Difficulty = "Hard"
                }
            };
        }

        // POE requirement methods
        public QuizQuestion GetCurrentQuestion()
        {
            if (_currentIndex >= 0 && _currentIndex < _questions.Count)
            {
                return _questions[_currentIndex];
            }
            return null;
        }

        public bool SubmitAnswer(string answer)
        {
            QuizQuestion current = GetCurrentQuestion();
            if (current == null) return false;

            bool isCorrect = string.Equals(current.CorrectAnswer.Trim(), answer.Trim(), StringComparison.OrdinalIgnoreCase);

            if (isCorrect)
            {
                score++;
            }

            _currentIndex++;
            return isCorrect;
        }

        public string GetFeedback(bool correct)
        {
            int completedIndex = _currentIndex - 1;
            if (completedIndex >= 0 && completedIndex < _questions.Count)
            {
                return _questions[completedIndex].Explanation;
            }
            return "No explanation available.";
        }

        public bool IsFinished()
        {
            return _currentIndex >= _questions.Count;
        }

        public string GetFinalScore()
        {
            return score + " out of " + _questions.Count;
        }

        public string GetFinalMessage()
        {
            double percentage = ((double)score / _questions.Count) * 100;
            if (percentage >= 75)
            {
                return "Excellent! You have a highly advanced awareness of cybersecurity defensive principles. Great job!";
            }
            else if (percentage >= 50)
            {
                return "Good effort! You understand the foundational elements, but keep learning to better safeguard your digital assets.";
            }
            else
            {
                return "Training recommended. Cyber threats are evolving; take time to review defensive frameworks and passphrases.";
            }
        }

        public void ResetQuiz()
        {
            _currentIndex = 0;
            score = 0;
        }

        // Standardized helper properties compatible across all compiler levels
        public int GetCurrentNumber()
        {
            return _currentIndex + 1;
        }

        public int GetTotalCount()
        {
            return _questions.Count;
        }
    }
}