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

            Messages = new ObservableCollection<ChatMessage>();
            ChatItemsControl.ItemsSource = Messages;

            _bot.PlayVoiceGreeting();

            // Initial startup instruction trigger
            Messages.Add(new ChatMessage
            {
                MessageText = "System Core Online. Please type your target name inside the control box input to initialize E-Bot core identity tracking modules.",
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

            // 1. Render User Purple Pill Bubble
            Messages.Add(new ChatMessage { MessageText = rawInput, IsUser = true });
            InputBox.Clear();

            // 2. Process AI Response
            string systemOutput = _bot.ProcessUserInput(rawInput);

            // 3. Render E-Bot Matte Gray Response Bubble
            Messages.Add(new ChatMessage { MessageText = systemOutput, IsUser = false });

            // 4. Trace View Tracking Downward
            ChatScrollViewer.ScrollToEnd();
        }
    }
}