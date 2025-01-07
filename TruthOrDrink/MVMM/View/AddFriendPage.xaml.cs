using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class AddFriendPage : ContentPage
    {
        public AddFriendPage()
        {
            InitializeComponent();
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendPage());
        }
    }
}
