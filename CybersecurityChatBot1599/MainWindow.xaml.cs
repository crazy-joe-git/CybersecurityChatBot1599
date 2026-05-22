using System;
using System.Windows;
using System.Windows.Input;

namespace CybersecurityChatBot1599
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot _botEngine;

        public MainWindow()
        {
            InitializeComponent();
            _botEngine = new ChatBot();

            LoadInitialDashboardState();
        }

        private void LoadInitialDashboardState()
        {
            ChatDisplayBox.Text = UIHelper.GetHeaderBanner();
            ChatDisplayBox.AppendText("\nSystem: Please type your name in the input block below to initialize access.\n");

            _botEngine.PlayVoiceGreeting();
        }

        private void ExecuteMessageSubmission()
        {
            string input = InputField.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            InputField.Clear();

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Stay secure online. Closing communication channel.", "E-Bot Offline", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
                return;
            }

            ChatDisplayBox.AppendText($"\nYou: {input}\n");

            string reply = _botEngine.ProcessUserInput(input);

            ChatDisplayBox.AppendText($"\n{reply}\n");
            ChatDisplayBox.ScrollToEnd();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteMessageSubmission();
        }

        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ExecuteMessageSubmission();
            }
        }
    }
}