using System;
using System.Collections.Generic;

namespace CybersecurityChatBot1599
{
    /// <summary>
    /// Acts as the central coordinator between the UI layer and the persistent data layers.
    /// Seamlessly links database updates with the automated activity audit tracking engine.
    /// </summary>
    public class TaskManager
    {
        private readonly TaskStorageHelper _storage = new TaskStorageHelper();
        private readonly ActivityLogger _logger = new ActivityLogger();

        /// <summary>
        /// Requests task creation, evaluates formatting, logs the event, and responds with feedback text.
        /// </summary>
        public string AddTask(string title, string description, string reminder)
        {
            _storage.AddTask(title, description, reminder);

            // Construct an informative message detailing the reminder setup state
            string logMessage = $"Task added: '{title}'" +
                                (!string.IsNullOrWhiteSpace(reminder) ? $" (Reminder set: {reminder})" : " (No reminder set)");

            _logger.LogAction(logMessage);

            return $"Task '{title}' has been successfully recorded.";
        }

        /// <summary>
        /// Loads all saved tasks from persistent SQLite storage to populate the interface grids.
        /// </summary>
        public List<DbTask> GetAllTasks()
        {
            return _storage.LoadTasks();
        }

        /// <summary>
        /// Updates a task's status to complete and registers an update event log entry.
        /// </summary>
        public void MarkAsComplete(int id)
        {
            string taskTitle = GetTaskTitleById(id);
            _storage.MarkAsComplete(id);
            _logger.LogAction($"Task marked complete: '{taskTitle}'");
        }

        /// <summary>
        /// Removes a task permanently from the system and generates a corresponding deletion log entry.
        /// </summary>
        public void DeleteTask(int id)
        {
            string taskTitle = GetTaskTitleById(id);
            _storage.DeleteTask(id);
            _logger.LogAction($"Task deleted: '{taskTitle}'");
        }

        /// <summary>
        /// Simple background lookup utility to capture a task heading string before entity mutations.
        /// </summary>
        private string GetTaskTitleById(int id)
        {
            var tasks = _storage.LoadTasks();
            foreach (var task in tasks)
            {
                if (task.Id == id) return task.Title;
            }
            return "Unknown System Task";
        }
    }
}