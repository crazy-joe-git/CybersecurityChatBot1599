using System;
using Microsoft.EntityFrameworkCore;

namespace CybersecurityChatBot1599
{
   
    public class DbTask
    {
        public int Id { get; set; } // Automatically acts as the primary key
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Reminder { get; set; } = string.Empty; 
        public bool IsComplete { get; set; } = false;
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

   
    public class Log
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

   
    //The database control center. Manages connection sessions and coordinates data reading/writing.
    
    public class ApplicationDbContext : DbContext
    {
        // Exposes our database tables as queryable collections to the rest of the app
        public DbSet<DbTask> Tasks { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ApplicationDbContext()
        {
            
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite("Data Source=database.db");

          
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}