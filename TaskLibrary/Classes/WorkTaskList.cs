using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

using TaskLibrary;

namespace TaskLibrary
{
    public class WorkTaskList : TaskList
    {
        public WorkTaskList()
        {
            tasks = new ObservableCollection<TaskSettings>();
            if (IsUsingDatabase)
            {
                var tasks = Db.Task.Where(t => t.Status == Status.New);

                foreach (var task in tasks)
                    Tasks.Add(task);
            }
        }

        public override void RemoveTasks()
        {
            Db.Task.RemoveRange(Db.Task.Where(t => t.Status == Status.New));
            Db.SaveChanges();
        }

        public void SetTaskStatusDone(int taskIndex)
        {
            if (taskIndex >= 0 && taskIndex <= Tasks.Count)
            {
                var task = Tasks[taskIndex];
                task.Status = Status.Done;

                if (IsUsingDatabase)
                {
                    Db.Entry(task).State = EntityState.Modified;
                    Db.SaveChanges();
                }
            }
        }
    }
}
