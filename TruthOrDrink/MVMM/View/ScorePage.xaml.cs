using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using TruthOrDrink.Models;
using TruthOrDrink.ViewModels;

namespace TruthOrDrink
{
    public partial class ScorePage : ContentPage
    {
        public ScorePage(List<Player> players)
        {
            InitializeComponent();
            BindingContext = new ScorePageViewModel(players);
        }




        private async void OnBackToStartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());
        }

        private async void OnShareClicked(object sender, EventArgs e)
        {
            if (BindingContext is ScorePageViewModel vm)
            {
                var players = vm.Players.ToList(); // Haal de spelers op vanuit de ViewModel
                await Navigation.PushAsync(new SharePage(players));
            }
        }

    }
}
