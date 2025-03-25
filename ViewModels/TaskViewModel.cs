using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using F.Models;
using F.Views;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace F.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private string _taskName  = string.Empty;
        private string _description  = string.Empty;

        private TodoTask _selectedTask;

        public string TaskName
        {
            get => _taskName;
            set
            {
                if (_taskName != value)
                {
                    _taskName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TodoTask> Tasks { get; } = new();
        public ICommand AddTaskCommand { get; }

        public ICommand NavigateToDetailCommand { get; }

        public ICommand SaveTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public TodoTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged();

                    if (_selectedTask != null)
                    {
                        NavigateToDetail(_selectedTask);
                    }
                }
            }
        }

        public TaskViewModel()
        {
            AddTaskCommand = new Command(AddTask);
            NavigateToDetailCommand = new Command<TodoTask>(NavigateToDetail);
            SaveTaskCommand = new Command(SaveTask);
            DeleteTaskCommand = new Command(async () => await ConfirmAndDeleteTask());
        }
        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(TaskName) && !string.IsNullOrWhiteSpace(Description))
            {
                Tasks.Add(new TodoTask { TaskName = TaskName, Description = Description });
                TaskName = string.Empty;
                Description = string.Empty;
            }  
        }

        private async void NavigateToDetail(TodoTask task)
        {
            if (task != null)
            {
                Console.WriteLine($"Navigating to TaskDetailPage for: {task.TaskName}");

                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TaskDetailPage(task, this));
                }
                else
                {
                    Console.WriteLine("Error: Application.Cuurent.MainPage is null.");
                }
            }
        }

        private void SaveTask()
        {
            Application.Current.MainPage.DisplayAlert("Succes", "Task saved succesfully!", "Ok");
        }

        private async Task ConfirmAndDeleteTask()
        {
            if (SelectedTask != null)
            {
                bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                   "Confirm Delete",
                   "Are you sure you want to delete this task?",
                   "Yes", 
                   "No"
                );

                if (isConfirmed)
                {
                    Tasks.Remove(SelectedTask);
                    SelectedTask = null;
                    await Application.Current.MainPage.DisplayAlert("Deleted", "Task deleted successfully.", "ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No task selected for deletion.", "ok");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged = null!;
       protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {   
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
