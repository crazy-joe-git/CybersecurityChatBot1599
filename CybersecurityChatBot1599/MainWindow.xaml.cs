using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityChatBot1599
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot _bot;
        public ObservableCollection<ChatMessage> Messages { get; set; }


        private readonly TaskManager _taskManager;
        public ObservableCollection<CybersecurityChatBot1599.DbTask> TasksCollection { get; set; } // Explicitly bound to custom DbTask entity model


        private List<MatrixQuizQuestion> _quizQuestions = new List<MatrixQuizQuestion>();
        private int _currentQuestionIndex = 0;
        private int _quizScore = 0;

        public MainWindow()
        {
            InitializeComponent();
            _bot = new ChatBot();


            _bot.PlayStartupAudio();

            Messages = new ObservableCollection<ChatMessage>();
            ChatItemsControl.ItemsSource = Messages;


            string welcomeText = UIHelper.GetHeaderBanner() +
                                 "\nWelcome to CYBERSECURITY AWARENESS BOT.\n" +
                                 "Please type your name to activate E-Bot.";


            Messages.Add(new ChatMessage
            {
                MessageText = welcomeText,
                IsUser = false
            });

            _taskManager = new TaskManager();
            TasksCollection = new ObservableCollection<CybersecurityChatBot1599.DbTask>();
            dgTasks.ItemsSource = TasksCollection;

            //Load all historical tasks from the SQLite database file on startup
            LoadTasksIntoGrid();

            // Securely initialize the isolated training engine after chat setups complete
            InitializeQuizEngine();
        }


        private void LoadTasksIntoGrid()
        {
            TasksCollection.Clear();


            List<CybersecurityChatBot1599.DbTask> tasks = _taskManager.GetAllTasks();

            foreach (var task in tasks)
            {
                TasksCollection.Add(task);
            }
        }


        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteMessageExchange();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ExecuteMessageExchange();
            }
        }

        private void ExecuteMessageExchange()
        {
            string rawInput = InputBox.Text;
            if (string.IsNullOrWhiteSpace(rawInput)) return;


            Messages.Add(new ChatMessage { MessageText = rawInput, IsUser = true });
            InputBox.Clear();


            string systemOutput = _bot.GetResponse(rawInput);

            Messages.Add(new ChatMessage { MessageText = systemOutput, IsUser = false });


            // CHATBOT INTERACTION DATABASE

            LoadTasksIntoGrid();


            ChatScrollViewer.ScrollToEnd();
        }


        private void InitializeQuizEngine()
        {
            LoadQuizData();
            DisplayCurrentQuestion();
        }

        //Quiz Questions and answers.
        private void LoadQuizData()
        {
            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "You receive an email from your 'bank' claiming your account is locked and asking you to click a link to verify your password. What is this?",
                Options = new string[] { "An official safety alert", "A phishing scam", "A routine system update", "A helpful shortcut" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! Legitimate institutions will never email you links to verify passwords or sensitive security credentials. This is a classic phishing attack."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "Which of the following practices creates the strongest and most secure account password?",
                Options = new string[] { "Using your name and birth year", "Using 'password123' so it is easy to remember", "Making it long with a mix of letters, numbers, and symbols", "Reusing the same password across all websites" },
                CorrectOptionIndex = 2,
                Explanation = "Correct! Long passwords that combine uppercase letters, lowercase letters, numbers, and special symbols are the hardest for attackers to crack."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "To protect your personal online privacy, what is the safest rule to follow on social media?",
                Options = new string[] { "Share your phone number and home address with followers", "Avoid posting sensitive personal details and real-time locations", "Accept friend requests from completely random strangers", "Post pictures of your ID or passport details" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! Over-sharing on public networks can expose your private data to scammers. Always restrict personal identifiers."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "What is 'Two-Factor Authentication' (2FA)?",
                Options = new string[] { "Writing down your password in two different places", "An extra security layer that requires a password AND a temporary code sent to your phone", "Using two separate web browsers at the same time", "Changing your password twice every month" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! 2FA acts as an excellent second defense line because an attacker cannot compromise your account with just your password alone."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "An unknown caller contacts you claiming to be Microsoft Support, saying your computer has a virus and demanding remote access to fix it. What should you do?",
                Options = new string[] { "Give them your login details immediately", "Pay them with gift cards to clean the system", "Hang up immediately because tech support companies do not cold-call users", "Follow their instructions step-by-step" },
                CorrectOptionIndex = 2,
                Explanation = "Correct! Tech support scams exploit fear. Real support centers never browse for targets or call you out of the blue demanding access."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "What does a secure password manager help you do?",
                Options = new string[] { "Share your credentials publicly with friends", "Generate and safely store unique, strong passwords for all your accounts", "Auto-post updates to your social profile", "Look up other people's secret passwords" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! A password manager allows you to use highly complex, unique passwords across every site without needing to memorize them yourself."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "You click a link and land on a website where the address bar shows 'http://' instead of 'https://'. What does this imply about your privacy?",
                Options = new string[] { "The connection is fully secure and safe for credit cards", "The network data is not encrypted, meaning others could spy on what you type", "The website is premium and authorized", "Your device is immune to malware on this page" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! The 'S' in HTTPS stands for Secure. Standard HTTP means data travels in plain text and can easily be snooped on by interceptors."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "A website popup text claims you are the '1,000,000th visitor' and won a free smartphone if you type your credit card for shipping. What is this?",
                Options = new string[] { "A lucky promotional prize", "A malicious sweepstakes scam designed to steal your financial details", "A mandatory government registration form", "A harmless browser advertisement game" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! If an offer sounds too good to be true, it is almost certainly an online scam tracking your personal financial details."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "What is the primary danger of using a public Wi-Fi network (like at a local coffee shop) without a VPN?",
                Options = new string[] { "Your device battery will drain instantly", "Attackers on the same network can potentially read your internet traffic", "The internet speed will be locked to zero", "Your browser files will be deleted automatically" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! Public networks are open environments. Nearby attackers can monitor network packets and catch unencrypted login data."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "You get a text message from an unrecognized number saying a parcel delivery failed and you must update your address via a link. What should you do?",
                Options = new string[] { "Click the link and fill out the requested profile data", "Forward the link to all your friends", "Ignore or block the number, and check the official courier app directly if expecting a package", "Reply with your full identity credentials" },
                CorrectOptionIndex = 2,
                Explanation = "Correct! This is 'smishing' (SMS Phishing). Scammers use fake parcel tracking updates to trick you into entering personal details."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "When setting up a new online account, why is it important to check the privacy settings configuration panel?",
                Options = new string[] { "To make sure the website runs faster", "To control who can view your profile information and how your data is shared", "To bypass paying subscription fees", "To change the background color of the web page" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! Privacy defaults are often loose. Checking options ensures you don't inadvertently broadcast your personal data to public search engines."
            });
        }


        private void DisplayCurrentQuestion()
        {
            if (_currentQuestionIndex < _quizQuestions.Count)
            {
                var current = _quizQuestions[_currentQuestionIndex];
                TxtQuestion.Text = $"QUESTION {_currentQuestionIndex + 1}: {current.QuestionText}";

                BtnOpt0.Content = current.Options[0];
                BtnOpt1.Content = current.Options[1];
                BtnOpt2.Content = current.Options[2];
                BtnOpt3.Content = current.Options[3];

                SetOptionButtonsEnabled(true);
                TxtFeedback.Text = "Select an answer to verify integrity.";
                TxtFeedback.Foreground = Brushes.White; // Clear visual trace for next module question
                BtnNext.Visibility = Visibility.Collapsed;
            }
            else
            {
                TxtQuestion.Text = "TRAINING COMPLETE. All modules processed.";
                SetOptionButtonsEnabled(false);
                BtnOpt0.Content = "-"; BtnOpt1.Content = "-"; BtnOpt2.Content = "-"; BtnOpt3.Content = "-";
                TxtFeedback.Text = $"Final system rating verified. Total Points: {_quizScore}. Integrity check: pass.";
                TxtFeedback.Foreground = Brushes.Green;
                BtnNext.Visibility = Visibility.Collapsed;
            }
        }

        private void SetOptionButtonsEnabled(bool enabled)
        {
            BtnOpt0.IsEnabled = enabled;
            BtnOpt1.IsEnabled = enabled;
            BtnOpt2.IsEnabled = enabled;
            BtnOpt3.IsEnabled = enabled;
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag != null)
            {
                int chosenIndex = int.Parse(clickedButton.Tag.ToString()!);
                var current = _quizQuestions[_currentQuestionIndex];

                SetOptionButtonsEnabled(false);

                if (chosenIndex == current.CorrectOptionIndex)
                {
                    _quizScore += 100;
                    TxtScore.Text = $"SCORE: {_quizScore:D3}";
                    TxtFeedback.Text = current.Explanation;
                    TxtFeedback.Foreground = Brushes.Green;
                }
                else
                {
                    TxtFeedback.Text = $"CRITICAL_ERROR: Incorrect selection. Correct answer was: {current.Options[current.CorrectOptionIndex]}";
                    TxtFeedback.Foreground = Brushes.Red;
                }

                BtnNext.Visibility = Visibility.Visible;
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentQuestionIndex++;
            DisplayCurrentQuestion();
        }


        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTaskTitle.Text;
            string description = txtTaskDesc.Text;
            string reminder = txtTaskReminder.Text;

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Task title is required.", "Validation Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create: Save data to database file immediately and update backend logs
            _taskManager.AddTask(title, description, reminder);

            LoadTasksIntoGrid();


            txtTaskTitle.Clear();
            txtTaskDesc.Clear();
            txtTaskReminder.Clear();
        }

        private void btnMarkComplete_Click(object sender, RoutedEventArgs e)
        {
            //Update:Cast selection context 
            if (dgTasks.SelectedItem is CybersecurityChatBot1599.DbTask selectedTask)
            {

                _taskManager.MarkAsComplete(selectedTask.Id);


                LoadTasksIntoGrid();
            }
            else
            {
                MessageBox.Show("Please select an active task from the table view first.", "Context Bound Exception", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Delete: Cast selection context
            if (dgTasks.SelectedItem is CybersecurityChatBot1599.DbTask selectedTask)
            {

                _taskManager.DeleteTask(selectedTask.Id);


                LoadTasksIntoGrid();
            }
            else
            {
                MessageBox.Show("Please select a target row item to drop from tracking logs.", "Context Bound Exception", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    internal class MatrixQuizQuestion
    {
        public string QuestionText { get; set; } = string.Empty;
        public string[] Options { get; set; } = new string[4];
        public int CorrectOptionIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;
    }
}