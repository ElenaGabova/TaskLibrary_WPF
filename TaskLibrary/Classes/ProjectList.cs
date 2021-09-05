using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary;
using System.Collections.ObjectModel;

namespace TaskLibrary
{
    public class ProjectList
    {
        public DatabaseContext Db { get;  set; } = new DatabaseContext();

        protected ObservableCollection<ProjectSettings> projects;

        public ObservableCollection<ProjectSettings> Projects
        {
            get
            {
                return projects;
            }

            private set
            {
                projects = value;
            }
        }

        public ProjectList()
        {
            //IQueryable<ProjectSettings> projectsQuiriable = Db.Project.Where(t => t.Id != 1);

            //foreach (var project in projectsQuiriable)
            //  Projects.Add(project);
        }


        public DbSet<ProjectSettings> GetDatabaseContext()
        {
            return Db.Project;
        }

        public void CreateProject(string projectName = "")
        {
            projectName = projectName.Trim();

            if (!string.IsNullOrEmpty(projectName))
            {
                ProjectSettings project= new ProjectSettings(projectName);

                Db.Project.Add(project);
                Db.SaveChanges();
            }
        }
    }
}
