using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using TaskLibrary;

namespace TaskLibrary
{
    //Описание класса и методов в файле README.md
    public abstract class  TaskList
    {    
        protected ObservableCollection<TaskSettings> tasks;
        
        public DateTime CreationDate { get; set; } = DateTime.Today;

        public ObservableCollection<TaskSettings> Tasks
        {
            get
            {
                return tasks;
            }

            private set
            {
                tasks = value;
            }
        }

        protected bool IsUsingDatabase { get; set; } = true;
        public DatabaseContext Db { get; set; } = new DatabaseContext();

        public void InitTaskLibrary(bool isUsingDatabase = true)
        {
            IsUsingDatabase = isUsingDatabase;
        }
        public DbSet<TaskSettings> GetDatabaseContext()
        {
            return Db.Task;
        }
        public abstract void RemoveTasks();
        public void CreateTask(string taskName = "", int projectID = 0)
        {
            taskName = taskName.Trim();

            if (!string.IsNullOrEmpty(taskName))
            {
                TaskSettings task;
                if (projectID > 0)
                    task = new TaskSettings(taskName, projectID);
                else
                    task = new TaskSettings(taskName);

                Tasks.Add(task);
                

                if (IsUsingDatabase)
                {
                    Db.Task.Add(task);
                    Db.SaveChanges();
                }
                    
            }
        }

        public void CopyTask(int taskIndex)
        {
            if (taskIndex >= 0 && taskIndex <= Tasks.Count)
            {
                string taskName = Tasks[taskIndex].Title;

                if (!string.IsNullOrEmpty(taskName))
                {
                    var task = new TaskSettings(taskName);
                    Tasks.Add(task);
                    Db.Task.Add(task);

                    if (IsUsingDatabase)
                    {
                        Db.Entry(Tasks[taskIndex]).State = EntityState.Modified;
                        Db.SaveChanges();
                    } 
                }
            }
        }

        public void DeleteTask(TaskSettings task)
        {
            if (IsUsingDatabase)
            {
                Tasks.Remove(task);
                Db.Task.Remove(task);
                Db.SaveChanges();
            }
        }

        public void EditTask(int taskIndex, string title = "")
        {
            if (taskIndex > 0 && taskIndex <= Tasks.Count)
            {
                if (!string.IsNullOrEmpty(title) &&
                    !title.Equals(Tasks[taskIndex].Title))

                    Tasks[taskIndex].Title = title;

                if (IsUsingDatabase)
                {
                    Db.Entry(Tasks[taskIndex]).State = EntityState.Modified;
                    Db.SaveChanges();
                } 
            }
        }
        
       public void MoveTaskInList(int taskIndex, int newTaskIndex)
       {
            var currentTask     = Tasks[taskIndex];
            Tasks[taskIndex]    = Tasks[newTaskIndex];
            Tasks[newTaskIndex] = currentTask;
            if (IsUsingDatabase)
            {
                Db.Entry(Tasks[taskIndex]).State = EntityState.Modified;
                Db.Entry(Tasks[newTaskIndex]).State = EntityState.Modified;
                Db.SaveChanges();
            }
        }

        public void CheckTaskAsModified(int taskIndex)
        {
            if (IsUsingDatabase)
            {
                TaskSettings task = Tasks[taskIndex];
                Db.Entry(task).State = EntityState.Modified;
                Db.SaveChanges();
            }
        }

        public int GetTaskIndexByName(string taskName)
        {
            TaskSettings currentTask = tasks.Where(t => t.Title == taskName).FirstOrDefault();
            return Tasks.IndexOf(currentTask);
        }
    }
}
