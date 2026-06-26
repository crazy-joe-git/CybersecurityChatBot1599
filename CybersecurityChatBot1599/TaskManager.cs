using System;
using System.Collections.Generic;

namespace CybersecurityChatBot1599
{
   
   
    //Seamlessly links database updates with the automated activity audit tracking engine.
   
    public class TaskManager
    {
        private readonly TaskStorageHelper _storage = new TaskStorageHelper();
        private readonly ActivityLogger _logger = new ActivityLogger();

       
        // Requests task creation, evaluates formatting, logs the event, and responds with feedback text.

        public string AddTask(string title, string description, string reminder)
        {
            _storage.AddTask(title, description, reminder);

            // Construct an informative message detailing the reminder setup state
            string logMessage = $"Task added: '{title}'" +
                                (!string.IsNullOrWhiteSpace(reminder) ? $" (Reminder set: {reminder})" : " (No reminder set)");

            _logger.LogAction(logMessage);

            return $"Task '{title}' has been successfully recorded.";
        }

        
        public List<DbTask> GetAllTasks()
        {
            return _storage.LoadTasks();
        }

       
        public void MarkAsComplete(int id)
        {
            string taskTitle = GetTaskTitleById(id);
            _storage.MarkAsComplete(id);
            _logger.LogAction($"Task marked complete: '{taskTitle}'");
        }

        
        public void DeleteTask(int id)
        {
            string taskTitle = GetTaskTitleById(id);
            _storage.DeleteTask(id);
            _logger.LogAction($"Task deleted: '{taskTitle}'");
        }

        
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