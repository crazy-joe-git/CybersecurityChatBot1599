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

        // Quiz engine state tracking fields
        private List<MatrixQuizQuestion> _quizQuestions = new List<MatrixQuizQuestion>();
        private int _currentQuestionIndex = 0;
        private int _quizScore = 0;

        public MainWindow()
        {
            InitializeComponent();
            _bot = new ChatBot();

            // Fire the startup audio greeting immediately when the UI framework loads
            _bot.PlayStartupAudio();

            Messages = new ObservableCollection<ChatMessage>();
            ChatItemsControl.ItemsSource = Messages;

            // Fetch the ASCII art from UIHelper and combine it with the welcome message
            string welcomeText = UIHelper.GetHeaderBanner() +
                                 "\nWelcome to CYBERSECURITY AWARENESS BOT.\n" +
                                 "Please type your name to activate E-Bot.";

            // Render the consolidated banner and greeting inside the first message bubble
            Messages.Add(new ChatMessage
            {
                MessageText = welcomeText,
                IsUser = false
            });

            // Securely initialize the isolated training engine after chat setups complete
            InitializeQuizEngine();
        }

        // ==========================================
        // MODULE: CORE CHAT INTERACTIONS
        // ==========================================
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

            // Render User Purple Pill Bubble
            Messages.Add(new ChatMessage { MessageText = rawInput, IsUser = true });
            InputBox.Clear();

            // Process AI Response
            string systemOutput = _bot.GetResponse(rawInput);

            // Render E-Bot Matte Gray Response Bubble
            Messages.Add(new ChatMessage { MessageText = systemOutput, IsUser = false });

            // Trace View Tracking Downward
            ChatScrollViewer.ScrollToEnd();
        }

        // ==========================================
        // MODULE: INTERACTIVE QUIZ ENGINE
        // ==========================================
        private void InitializeQuizEngine()
        {
            LoadQuizData();
            DisplayCurrentQuestion();
        }

        private void LoadQuizData()
        {
            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "An attacker is attempting to flood a server with SYN packets to exhaust system resources. What type of attack is this?",
                Options = new string[] { "SQL Injection", "SYN Flood DDoS", "Man-in-the-Middle", "Phishing" },
                CorrectOptionIndex = 1,
                Explanation = "Correct. A SYN flood exploits the TCP three-way handshake sequence by leaving connections half-open."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "Which cryptographic protocol is widely used to secure web browser communications via HTTPS?",
                Options = new string[] { "FTP", "TLS", "WEP", "Telnet" },
                CorrectOptionIndex = 1,
                Explanation = "Correct. Transport Layer Security (TLS) succeeds SSL to secure communications over a computer network."
            });

            _quizQuestions.Add(new MatrixQuizQuestion
            {
                QuestionText = "What does the security principle of 'Least Privilege' dictate?",
                Options = new string[] { "Users get maximum root access", "Users get zero network visibility", "Users hold only the minimum access necessary to perform tasks", "All accounts share a single token" },
                CorrectOptionIndex = 2,
                Explanation = "Correct. Limiting access privileges to only what is necessary minimizes potential damage from compromised credentials."
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
                TxtFeedback.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10B981"));
                BtnNext.Visibility = Visibility.Collapsed;
            }
            else
            {
                TxtQuestion.Text = "TRAINING COMPLETE. All modules processed.";
                SetOptionButtonsEnabled(false);
                BtnOpt0.Content = "-"; BtnOpt1.Content = "-"; BtnOpt2.Content = "-"; BtnOpt3.Content = "-";
                TxtFeedback.Text = "Final system rating verified. Integrity check: pass.";
                TxtFeedback.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10B981"));
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
                    TxtFeedback.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10B981"));
                }
                else
                {
                    TxtFeedback.Text = $"CRITICAL_ERROR: Incorrect selection. Correct answer was: {current.Options[current.CorrectOptionIndex]}";
                    TxtFeedback.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8338EC"));
                }

                BtnNext.Visibility = Visibility.Visible;
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentQuestionIndex++;
            DisplayCurrentQuestion();
        }

        // ==========================================
        // MODULE: TASK ASSISTANT INTERFACES
        // ==========================================
        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTaskTitle.Text;
            string description = txtTaskDesc.Text;
            string reminder = txtTaskReminder.Text;

            if (string.IsNullOrWhiteSpace(title)) return;

            // UI cleanup processing placeholder
            txtTaskTitle.Clear();
            txtTaskDesc.Clear();
            txtTaskReminder.Clear();
        }

        private void btnMarkComplete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgTasks.SelectedItem;
            if (selectedItem == null) return;
            // Target database completion updates can be placed here
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgTasks.SelectedItem;
            if (selectedItem == null) return;
            // Target database record deletion logic can be placed here
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