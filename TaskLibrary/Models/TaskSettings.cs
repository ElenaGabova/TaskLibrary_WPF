
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using TaskLibrary;

namespace TaskLibrary
{
    [Table("Task")]
    public class TaskSettings : INotifyPropertyChanged
    {
        //Свойства
        private int id;
        private string title;
        private DateTime creationDate;
        private Status status { get; set; }
        private int projectID { get; set; }

        //поля
        [Key]
        public int Id { get; set; }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set
            {
                creationDate = value;
                OnPropertyChanged("CreationDate");
            }
        }

        [Column(TypeName = "int")]
        public Status Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public int ProjectID
        {
            get { return projectID; }
            set
            {
                projectID = value;
                OnPropertyChanged("ProjectID");
            }
        }

        //Конструкторы
        public TaskSettings() { }

        public TaskSettings(string title) : this(title, DateTime.Today, Status.New, 0) { }
        public TaskSettings(string title, int ProjectID) : this(title, DateTime.Today, Status.New, ProjectID) { }
        public TaskSettings(string title, Status status) : this(title, DateTime.Today, status, 0) { }
        public TaskSettings(string title, DateTime creationDate) : this(title, creationDate, Status.New, 0) { }
        public TaskSettings(string title, DateTime creationDate, Status status) : this(title, creationDate, Status.New, 0) { }
        public TaskSettings(string title, DateTime creationDate, Status status, int ProjectID)
        {
            Title        = title;
            CreationDate = creationDate;
            Status       = status;
            ProjectID    = projectID;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
