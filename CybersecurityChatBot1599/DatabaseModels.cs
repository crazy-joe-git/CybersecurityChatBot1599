using System;
using Microsoft.EntityFrameworkCore;

namespace CybersecurityChatBot1599
{
    /// <summary>
    /// Represents a cybersecurity task. 
    /// Named 'DbTask' to explicitly avoid a naming collision with the built-in C# 'System.Threading.Tasks.Task' class.
    /// </summary>
    public class DbTask
    {
        public int Id { get; set; } // Automatically acts as the primary key (auto-incrementing)
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Reminder { get; set; } = string.Empty; // Holds textual reminder rules (e.g., "In 2 days")
        public bool IsComplete { get; set; } = false;
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// Represents an audit trail entry tracking actions performed by the user and chatbot.
    /// </summary>
    public class Log
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// The database control center. Manages connection sessions and coordinates data reading/writing.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        // Exposes our database tables as queryable collections to the rest of the app
        public DbSet<DbTask> Tasks { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ApplicationDbContext()
        {
            // Verifies if 'database.db' exists at startup. If missing, it instantly generates 
            // the database file and executes the underlying schema tables automatically.
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configures the context to use a local, self-contained SQLite file
            optionsBuilder.UseSqlite("Data Source=database.db");

            // Enables proxy capabilities to automatically resolve relationship tracking
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}