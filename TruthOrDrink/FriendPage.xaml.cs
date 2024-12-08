using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class FriendPage : ContentPage
    {
        public FriendPage()
        {
            InitializeComponent();
        }

        private async void OnAddFriendClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFriendPage());
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());
        }
    }
}
