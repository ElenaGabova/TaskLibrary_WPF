using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TaskLibrary;

namespace TaskLibrary
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {  
            Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS " + nameof(Task) + "  ([Id] INTEGER PRIMARY KEY, [Title] TEXT, [CreationDate] DATETIME, [Status] INTEGER NOT NULL, [ProjectID] INTEGER NULL)");        
            Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS " + nameof(Project) + " ([Id] INTEGER PRIMARY KEY, [Title] STRING);"); 
        }

        public DbSet<TaskSettings> Task { get; set; }
        public DbSet<ProjectSettings> Project { get; set; }

    }
}
