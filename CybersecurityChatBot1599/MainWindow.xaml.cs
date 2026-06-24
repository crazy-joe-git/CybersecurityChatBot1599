using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CybersecurityChatBot1599
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot _bot;
        public ObservableCollection<ChatMessage> Messages { get; set; }

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
    }
}