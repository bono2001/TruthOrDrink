using System.Collections.ObjectModel;
using TruthOrDrink.Models;

namespace TruthOrDrink.ViewModels
{
    public class ScorePageViewModel
    {
        public ObservableCollection<Player> Players { get; set; } = new();

        public ScorePageViewModel(List<Player> players)
        {
            // Voeg de lijst van spelers toe aan de ObservableCollection
            Players = new ObservableCollection<Player>(players);
        }
    }
}
