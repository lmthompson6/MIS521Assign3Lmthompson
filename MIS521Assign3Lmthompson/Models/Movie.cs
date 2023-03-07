using Microsoft.Build.Framework;

namespace MIS521Assign3Lmthompson.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string IMDB { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string YearOfRelease { get; set; }
        public byte[]? Poster { get; set; }



    }
}
