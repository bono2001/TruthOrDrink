namespace TruthOrDrink.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int Rounds { get; set; }
        public string Difficulty { get; set; }
        public List<Player> Players { get; set; }
        public List<Question> Questions { get; set; }
    }
}
