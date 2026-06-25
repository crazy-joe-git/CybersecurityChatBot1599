using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatBot1599
{
    /// <summary>
    /// Manages system event tracking and records diagnostic records to the database.
    /// Provides capped views for user convenience as required by the assignment rubric.
    /// </summary>
    public class ActivityLogger
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// Appends a new event entry with a fresh timestamp directly to the SQLite database.
        /// </summary>
        public void LogAction(string description)
        {
            Log newLog = new Log
            {
                Description = description
            };

            _db.Logs.Add(newLog);
            _db.SaveChanges();
        }

        /// <summary>
        /// Retrieves only the most recent logs (defaults to 5) to prevent interface clutter.
        /// </summary>
        public List<Log> GetRecentLogs(int count = 5)
        {
            // Pulls logs by descending ID order to target the latest additions, takes the requested count, 
            // and reverses them back to clean chronological view
            return _db.Logs.OrderByDescending(l => l.Id).Take(count).AsEnumerable().Reverse().ToList();
        }

        /// <summary>
        /// Retrieves the entire operational audit history across the life of the application.
        /// </summary>
        public List<Log> GetAllLogs()
        {
            return _db.Logs.ToList();
        }
    }
}