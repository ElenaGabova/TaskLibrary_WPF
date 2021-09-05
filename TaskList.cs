using System;
using System.Collections.Generic;
using System.Linq;
using TaskContext;

namespace TaskLibrary
{

    //Описание класса и методов в файле README.md
    public class TaskList
    {
        public DateTime CreationDate { get; set; } = DateTime.Today;
        private List<Task> Tasks { get; set; } = new List<Task>();

        private bool IsUsingDatabase { get; set; } = true;

        private TasksContext Db { get; set; } = new TasksContext();
        TaskList()
        {
            if (IsUsingDatabase)
            {
                var tasks = Db.Tasks.Where(t => t.CreationDate == CreationDate);

                foreach (var task in tasks)
                    Tasks.Add(task);
            }
        }
        public void InitTaskLibrary(bool isUsingDatabase = true)
        {
            IsUsingDatabase = isUsingDatabase;
        }

        public List<Task> GetTasksList()
        {
            return Tasks;
        }
        public void CreateTask(string taskName = "")
        {
            taskName = taskName.Trim();

            if (!string.IsNullOrEmpty(taskName))
            {
                var task = new Task(taskName);
                Tasks.Add(task);
                Db.Tasks.Add(task);

                if (IsUsingDatabase)
                    Db.SaveChanges();
            }
        }

        public void DeleteTask(int taskNumber = 0)
        {
            if (taskNumber > 0 && taskNumber <= Tasks.Count)
            {
                Task task = Tasks[taskNumber - 1];

                if (IsUsingDatabase)
                {
                    Tasks.Remove(task);
                    Db.Tasks.Remove(task);
                    Db.SaveChanges();
                }
            }
        }
        public void EditTask(int taskNumber = 0, string title = "")
        {
            if (taskNumber > 0 && taskNumber <= Tasks.Count)
            {
                if (!string.IsNullOrEmpty(title) &&
                    !title.Equals(Tasks[taskNumber - 1].Title))

                    Tasks[taskNumber - 1].Title = title;

                if (IsUsingDatabase)
                    Db.SaveChanges();
            }
        }

        public void SetTaskStatusDone(int taskNumber = 0)
        {
            if (taskNumber > 0 && taskNumber <= Tasks.Count)
            {
                var task = Tasks[taskNumber - 1];
                task.Status = Status.Done;
                Db.SaveChanges();
            }
        }

        public void RefreshTasks()
        {
            Tasks.Clear();
            if (IsUsingDatabase)
            {
                Db.Tasks.RemoveRange(Db.Tasks.Where(t => t.CreationDate == CreationDate));
                Db.SaveChanges();
            }
        }
    }
}
