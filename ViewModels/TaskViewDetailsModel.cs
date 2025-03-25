using System.Windows.Input;
using F.Models;
using Microsoft.Maui.Controls;

namespace F.ViewModels
{
    public class TaskDetailViewModel
    {
        private readonly TaskViewModel _taskViewModel;

        public TodoTask Task {get; }
        public ICommand SaveTaskCommand {get; }
        public ICommand DeleteTaskCommand {get ;}

        public TaskDetailViewModel(TodoTask task, TaskViewModel taskViewModel)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
            _taskViewModel = taskViewModel ?? throw new ArgumentNullException(nameof(taskViewModel));

            SaveTaskCommand = new Command(SaveTask);
            DeleteTaskCommand = new Command(DeleteTask);
        }

        private async void SaveTask()
        {
            Console.WriteLine("Task Saved");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

         private async void DeleteTask()
{
    if (Application.Current?.MainPage == null)
    {
        Console.WriteLine("Error: Application.Current.MainPage is null.");
        return;
    }

    bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
        "Confirm Delete",
        "Are you sure you want to delete this task?",
        "Yes", 
        "No"
    );

    if (isConfirmed)
    {
        if (_taskViewModel?.Tasks.Contains(Task) == true)
        {
            _taskViewModel.Tasks.Remove(Task);
            Console.WriteLine("Task Deleted");
        }
        else
        {
            Console.WriteLine("Error: Task not found in ViewModel.");
        }
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}   
    }   

}