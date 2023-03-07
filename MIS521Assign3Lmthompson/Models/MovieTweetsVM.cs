namespace MIS521Assign3Lmthompson.Models
{
    public class MovieTweetsVM
    {
        public Movie Movie { get; set; }
        public List<Tweet> Tweets { get; set; }
        public double ScoreAvg { get; set; }
    }
}
