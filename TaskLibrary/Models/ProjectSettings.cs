
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TaskLibrary
{
    [Table("Project")]
    public class ProjectSettings : INotifyPropertyChanged
    {
        //Свойства
        private int id;
        private string title;

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
        
        //Конструкторы
        public ProjectSettings() { }
        public ProjectSettings(string title)
        {
            Title = title;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    } 
}

