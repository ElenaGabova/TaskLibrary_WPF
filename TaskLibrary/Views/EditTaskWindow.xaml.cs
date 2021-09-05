using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskLibrary;

namespace TaskLibrary
{
    /// <summary>
    /// Логика взаимодействия для BD_Window1.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public TaskSettings Task { get; private set; }

        public EditTaskWindow(TaskSettings t)
        {
            InitializeComponent();
            Task = t;
            this.DataContext = Task;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
