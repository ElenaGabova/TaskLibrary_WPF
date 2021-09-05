using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TaskLibrary;
using System.Collections.ObjectModel;

namespace TaskLibrary
{
    public class DoneTaskList : TaskList
    {
        public DoneTaskList()
        {
            tasks = new ObservableCollection<TaskSettings>();
            if (IsUsingDatabase)
            {
                var tasks = Db.Task.Where(t => t.Status == Status.Done);

                foreach (var task in tasks)
                    Tasks.Add(task);
            }
        }

        public override void RemoveTasks()
        {
            Db.Task.RemoveRange(Db.Task.Where(t => t.Status == Status.Done));
            Db.SaveChanges();
        }

        public void SetTaskStatusWork(int taskIndex)
        {
            if (taskIndex >= 0 && taskIndex <= Tasks.Count)
            {
                var task = Tasks[taskIndex];

                task.Status = Status.New;
                if (IsUsingDatabase)
                {
                    Db.Entry(task).State = EntityState.Modified;
                    Db.SaveChanges();
                }
            }
        }
    }
}
