using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace VoziMe.Models
{
    public class TaxiStajaliste
    {
        public TaxiStajaliste() { }

        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Ime stajališta je obavezno.")]
        public Stajaliste ime { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna.")]
        public string adresa { get; set; }

        [Required(ErrorMessage = "Broj mjesta je obavezan.")]
        [Range(1, int.MaxValue, ErrorMessage = "Broj mjesta mora biti veći od 0.")]
        public int brojMjesta { get; set; }
    }
}
