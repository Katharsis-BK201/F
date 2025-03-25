using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace F
{
    public partial class EditUserPage : ContentPage
    {
        private User _user;
        private DatabaseHelper _databaseHelper;

        public EditUserPage(User user)
        {
            InitializeComponent();
            _user = user;
            _databaseHelper = new DatabaseHelper();

            // Pre-fill the Entry fields with the user's existing data
            NameEntry.Text = _user.Name;
            EmailEntry.Text = _user.Email;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text))
            {
                await DisplayAlert("Error", "Please enter both name and email.", "OK");
                // return;
            }

            // Update user properties
            _user.Name = NameEntry.Text;
            _user.Email = EmailEntry.Text;

            try
            {
                // Update the user in the database
                await _databaseHelper.UpdateUserAsync(_user);

                await DisplayAlert("Success", "User updated successfully.", "OK");

                // Navigate back to the previous page
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update user: {ex.Message}", "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Go back to the previous page
        }
    }
}
