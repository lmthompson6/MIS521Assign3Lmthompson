namespace MIS521Assign3Lmthompson.Models
{
    public class ActorTweetsVM
    {
        public Actor Actor { get; set; }
        public List<Tweet> Tweets { get; set; }
        public double ScoreAvg { get; set; }
    }
}
