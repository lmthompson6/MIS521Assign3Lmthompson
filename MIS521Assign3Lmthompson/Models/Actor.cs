using Microsoft.Build.Framework;

namespace MIS521Assign3Lmthompson.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string IMDB { get; set; }
        public byte[]? Image { get; set; }

    }
}
