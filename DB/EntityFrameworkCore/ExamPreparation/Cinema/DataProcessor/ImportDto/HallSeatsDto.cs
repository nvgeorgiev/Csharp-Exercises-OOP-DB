namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class HallSeatsDto
    {
        // What we need in the HallSeats DTO:
        // {
        //    "Name": "Methocarbamol",
        //    "Is4Dx": false,
        //    "Is3D": true,
        //    "Seats": 52
        // },

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        
        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }

        [Range(1, int.MaxValue)]
        public int Seats { get; set; }
    }
}
