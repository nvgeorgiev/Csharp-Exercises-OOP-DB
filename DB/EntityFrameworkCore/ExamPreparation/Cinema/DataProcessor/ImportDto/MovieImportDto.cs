namespace Cinema.DataProcessor.ImportDto
{
    using Cinema.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MovieImportDto
    {
        // What we need in the Movie DTO:
        //  {
        //    "Title": "Little Big Man",
        //    "Genre": "Western",
        //    "Duration": "01:58:00",
        //    "Rating": 28,
        //    "Director": "Duffie Abrahamson"
        //  },

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Director { get; set; }
    }
}
