using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using TruthOrDrink.Models; 

namespace TruthOrDrink
{
    public partial class QuestionPage : ContentPage
    {
        public ObservableCollection<Question> Questions { get; set; }

        public QuestionPage()
        {
            InitializeComponent();

            // Voeg nepvragen toe aan de Questions-verzameling
            Questions = new ObservableCollection<Question>
            {
                new Question { Text = "Wat is je grootste geheim?", Category = "Cat 1" },
                new Question { Text = "Wat is het meest gênante dat je ooit hebt gedaan?", Category = "Cat 2" },
                new Question { Text = "Wie vind je stiekem het aantrekkelijkst?", Category = "Cat 3" },
                new Question { Text = "Wat is de grootste leugen die je ooit hebt verteld?", Category = "Cat 1" },
                new Question { Text = "Wat is je vreemdste gewoonte?", Category = "Cat 2" }
            };

            // Stel de BindingContext in
            BindingContext = this;
        }

        private async void OnAddQuestionClicked(object sender, EventArgs e)
        {
            // Navigeer naar de AddQuestionPage
            await Navigation.PushAsync(new AddQuestionPage());
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            // Navigeer terug naar de vorige pagina
            await Navigation.PopAsync();
        }
    }
}
