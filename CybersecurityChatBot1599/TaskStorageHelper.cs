using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatBot1599
{
    //Handles all direct CRUD (Create, Read, Update, Delete) database operations for tasks.
    public class TaskStorageHelper
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

       
        // READ: Fetches all tracked security tasks currently saved inside the SQLite database.
        
        public List<DbTask> LoadTasks()
        {
            return _db.Tasks.ToList();
        }

        
        //CREATE: Builds a new task entity from raw string data and persists it directly to the database.
       
        public void AddTask(string title, string description, string reminder)
        {
            DbTask newTask = new DbTask
            {
                Title = title,
                Description = description,
                Reminder = reminder,
                IsComplete = false
            };

            _db.Tasks.Add(newTask);
            _db.SaveChanges(); // Saves the changes directly to the physical database file
        }

        
        //UPDATE: Finds a specific task by its unique primary key ID and updates its completion state.
        
        public void MarkAsComplete(int id)
        {
            DbTask task = _db.Tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                task.IsComplete = true;
                _db.Tasks.Update(task);
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// DELETE: Locates an existing task record matching the unique ID parameter and expels it from storage.
        /// </summary>
        public void DeleteTask(int id)
        {
            DbTask task = _db.Tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                _db.Tasks.Remove(task);
                _db.SaveChanges();
            }
        }
    }
}