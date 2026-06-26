using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatBot1599
{
    
    public class ActivityLogger
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        
        public void LogAction(string description)
        {
            Log newLog = new Log
            {
                Description = description
            };

            _db.Logs.Add(newLog);
            _db.SaveChanges();
        }

        
        public List<Log> GetRecentLogs(int count = 5)
        {
            
            return _db.Logs.OrderByDescending(l => l.Id).Take(count).AsEnumerable().Reverse().ToList();
        }

        
        public List<Log> GetAllLogs()
        {
            return _db.Logs.ToList();
        }
    }
}