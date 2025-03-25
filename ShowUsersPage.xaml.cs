namespace F;
using System.Diagnostics;

public partial class ShowUsersPage : ContentPage
{
    private readonly DatabaseHelper _databaseHelper = new DatabaseHelper();

    public ShowUsersPage()
    {
        InitializeComponent();
        LoadUsers();
    } 
    
    private async void LoadUsers()
    {
        var users = await _databaseHelper.GetUsersAsync();
        UsersListView.ItemsSource = users;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is User user)
            {
                await _databaseHelper.DeleteUserAsync(user);
                LoadUsers();
                await DisplayAlert("Delete", $"Delete user {user.Name}?", "OK");
            }
        }
    private async void OnEditClicked(object sender, EventArgs e)
{
    if (sender is Button button && button.BindingContext is User user)
    {
        await Navigation.PushAsync(new EditUserPage(user));
    }
    else
    {
        await DisplayAlert("Error", "Unable to edit user.", "OK");
    }
}


}