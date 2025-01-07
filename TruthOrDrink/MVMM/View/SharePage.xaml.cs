using Microsoft.Maui.Controls;
using System;

namespace TruthOrDrink
{
    public partial class SharePage : ContentPage
    {
        public SharePage()
        {
            InitializeComponent();
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new ScorePage());
        }

    }
}
