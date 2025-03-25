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

}