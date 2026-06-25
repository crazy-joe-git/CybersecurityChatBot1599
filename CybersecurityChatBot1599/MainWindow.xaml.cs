using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace CybersecurityChatBot1599
{
    public partial class MainWindow : Window
    {
        // Explicitly named collections to avoid conflicting with existing classes
        public ObservableCollection<MatrixChatMessage> ChatMessages { get; set; }
        public ObservableCollection<MatrixSecurityTask> SecurityTasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ChatMessages = new ObservableCollection<MatrixChatMessage>();
            SecurityTasks = new ObservableCollection<MatrixSecurityTask>();

            ChatItemsControl.ItemsSource = ChatMessages;
            dgTasks.ItemsSource = SecurityTasks;

            ChatMessages.Add(new MatrixChatMessage
            {
                MessageText = "System initialized. Core Matrix Chat online.",
                IsUser = false
            });
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessChatMessage();
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessChatMessage();
        }

        private void ProcessChatMessage()
        {
            string userInput = InputBox.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            ChatMessages.Add(new MatrixChatMessage { MessageText = userInput, IsUser = true });
            InputBox.Clear();

            ChatScrollViewer.ScrollToBottom();

            ChatMessages.Add(new MatrixChatMessage
            {
                MessageText = "Message processed by core engine. NLP logic layer parsing request.",
                IsUser = false
            });
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaskTitle.Text))
            {
                MessageBox.Show("Please enter a valid task title before saving.", "Validation Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int nextId = SecurityTasks.Count + 1;

            SecurityTasks.Add(new MatrixSecurityTask
            {
                Id = nextId,
                Title = txtTaskTitle.Text,
                Description = txtTaskDesc.Text,
                Reminder = txtTaskReminder.Text,
                IsComplete = false
            });

            txtTaskTitle.Clear();
            txtTaskDesc.Clear();
            txtTaskReminder.Clear();
        }

        private void btnMarkComplete_Click(object sender, RoutedEventArgs e)
        {
            if (dgTasks.SelectedItem is MatrixSecurityTask selectedTask)
            {
                selectedTask.IsComplete = true;
                dgTasks.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a task from the table to mark as completed.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dgTasks.SelectedItem is MatrixSecurityTask selectedTask)
            {
                SecurityTasks.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Please select a task from the table to purge.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    public class MatrixChatMessage
    {
        public string MessageText { get; set; }
        public bool IsUser { get; set; }
    }

    public class MatrixSecurityTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
        public bool IsComplete { get; set; }
    }
}