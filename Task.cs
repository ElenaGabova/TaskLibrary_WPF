using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskLibrary
{
    public class Task
    {
        //Свойства
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "int")]
        public Status Status { get; set; } = Status.New;

        //Конструкторы
        public Task() { }

        public Task(string title) : this(title, DateTime.Today, Status.New) { }
        public Task(string title, Status status) : this(title, DateTime.Today, status) { }
        public Task(string title, DateTime creationDate) : this(title, creationDate, Status.New) { }
        public Task(string title, DateTime creationDate, Status status)
        {
            Title = title;
            CreationDate = creationDate;
            Status = status;
        }
    }

}
