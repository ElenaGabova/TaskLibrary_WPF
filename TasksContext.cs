using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskContext
{
    public class TasksContext : DbContext
    {
        public DbSet<TaskLibrary.Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TaskLibrary.db");
            SQLitePCL.Batteries.Init();
        }

    }
}
