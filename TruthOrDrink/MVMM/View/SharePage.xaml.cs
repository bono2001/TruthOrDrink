using Microsoft.Maui.Controls;
using System;
using TruthOrDrink.Models;

namespace TruthOrDrink
{
    public partial class SharePage : ContentPage
    {
        private readonly List<Player> _players;

        public SharePage(List<Player> players)
        {
            InitializeComponent();
            _players = players; // Sla de lijst van spelers op
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            // Navigeer naar de ScorePage en geef de lijst van spelers door
            await Navigation.PushAsync(new ScorePage(_players));
        }
    }
}
