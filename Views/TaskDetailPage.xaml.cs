using F.Models;
using F.ViewModels;
using Microsoft.Maui.Controls;

namespace F.Views
{
    public partial class TaskDetailPage : ContentPage
    {
        public TaskDetailPage(TodoTask task, TaskViewModel taskViewModel)
        {     
            InitializeComponent();
            BindingContext = new TaskDetailViewModel(task, taskViewModel);
        }
    }
}