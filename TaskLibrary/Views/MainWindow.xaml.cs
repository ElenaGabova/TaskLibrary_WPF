using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskLibrary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskSettings> taskList1 = new ObservableCollection<TaskSettings>();

        WorkTaskList workTasks = new WorkTaskList();
        DoneTaskList doneTasks = new DoneTaskList();
        ProjectList  projectList = new ProjectList();
   
        private bool isInputTextIsProjectName = false;

        public MainWindow()
        {
            InitializeComponent();

            InitilazeTaskLists();
            SetFisrtTextIntoTextBox(isColorGray: true, isAddTask: true);
        }

        private void InitilazeTaskLists()
        {
            workTasks = new WorkTaskList();
            doneTasks = new DoneTaskList();
            lboxWorkTasks.ItemsSource = workTasks.Tasks;
            lboxDoneTasks.ItemsSource = doneTasks.Tasks;
            cboxProject.ItemsSource   = projectList.Projects;
        }
        private void SetFisrtTextIntoTextBox(bool isColorGray = true, bool isAddTask = true, bool isAddProject = false)
        {
            if (isColorGray)
            {
                string dopInputText = isAddTask ? "задачи" : "проекта";
                string inputText = string.Format("Введите название {0}, и нажмите кнопку Enter", dopInputText);
                txtTaskAndProjectName.Foreground = new SolidColorBrush(Colors.Gray);
                txtTaskAndProjectName.Text = inputText;
            }
            else
            {
                txtTaskAndProjectName.Foreground = new SolidColorBrush(Colors.Red);
                txtTaskAndProjectName.Text = "";
            }
        }

        private void TxtTaskAndProjectName_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string inputText = txtTaskAndProjectName.Text;
                if (!string.IsNullOrEmpty(inputText))
                {
                    if (isInputTextIsProjectName)
                    {
                        projectList.CreateProject(inputText);
                        isInputTextIsProjectName = false;
                    }
                    else
                    {
                        var projectName = cboxProject.SelectedItem as ProjectSettings;

                        if (projectName != null)
                        {
                            inputText = string.Format("{0}. {1}", projectName.Title,
                                                                  inputText);
                        }

                        cboxProject.SelectedIndex = -1;
                        workTasks.CreateTask(inputText);

                        SetFisrtTextIntoTextBox(isColorGray: true, isAddTask: true);
                    }
                }
            }

        }

        private void ShowDoneTasksButton_Click(object sender, RoutedEventArgs e)
        {
            if (btnDoneTasksShow.Content.ToString().Equals("Показать выполненные задачи"))
            {
                lboxDoneTasks.Visibility = Visibility.Visible;
                btnDoneTasksShow.Content = "Скрыть выполненные задачи";
            }
            else
            {
                lboxDoneTasks.Visibility = Visibility.Hidden;
                btnDoneTasksShow.Content = "Показать выполненные задачи";
            }
        }

        private void BtnProjectAdd_Click(object sender, RoutedEventArgs e)
        {
            isInputTextIsProjectName = true;
            SetFisrtTextIntoTextBox(isColorGray: true, isAddTask: false, isAddProject: true);
        }

        private void CopyWorkTask(object sender, EventArgs e)
        {
            workTasks.CopyTask(lboxWorkTasks.SelectedIndex);
        }

        private void DeleteWorkTask(object sender, EventArgs e)
        {
            if (lboxWorkTasks.SelectedItem != null)
                workTasks.Tasks.Remove(lboxWorkTasks.SelectedItem as TaskSettings);
        }

        private void RenameWorkTask(object sender, EventArgs e)
        {
            if (lboxWorkTasks.SelectedItem == null) return;
            // получаем выделенный объект
            TaskSettings task = workTasks.Tasks[lboxWorkTasks.SelectedIndex];

            EditTaskWindow taskWindow = new EditTaskWindow(new TaskSettings
            {
                Id = task.Id,
                Title = task.Title,
                CreationDate = task.CreationDate,
                Status = task.Status
            });

            if (taskWindow.ShowDialog() == true)
            {
                workTasks.EditTask(lboxWorkTasks.SelectedIndex, taskWindow.Task.Title);
            }
        }

        private void RemoveWorkTasks(object sender, EventArgs e)
        {
            workTasks.RemoveTasks();
            InitilazeTaskLists();
        }

        private void MoveUpMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = lboxWorkTasks.SelectedIndex;

            if (lboxWorkTasks.SelectedIndex > 0)
                workTasks.MoveTaskInList(selectedIndex, selectedIndex - 1);
        }

        private void MoveDownMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = lboxWorkTasks.SelectedIndex;

            if (selectedIndex < lboxWorkTasks.Items.Count - 1)
                workTasks.MoveTaskInList(selectedIndex, selectedIndex + 1);
        }

        private void LboxWorkTasks_Checked(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            string taskName = checkBox.Content.ToString();
            int currentIndex = workTasks.GetTaskIndexByName(taskName);
            workTasks.SetTaskStatusDone(currentIndex);
            InitilazeTaskLists();
        }

        private void DeleteDoneTask(object sender, EventArgs e)
        {
            if (lboxDoneTasks.SelectedItem == null) return;
                doneTasks.DeleteTask(lboxDoneTasks.SelectedItem as TaskSettings);
        }

        private void LboxDoneTasks_Unchecked(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            var textBlock = checkBox.Content as TextBlock;
            string taskName = textBlock.Text;
            int currentIndex = doneTasks.GetTaskIndexByName(taskName);
            doneTasks.SetTaskStatusWork(currentIndex);
            InitilazeTaskLists();
        }

        private void TxtTaskAndProjectName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetFisrtTextIntoTextBox(isColorGray: false);
        }
    }
}
