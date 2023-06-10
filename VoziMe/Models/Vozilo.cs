using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models
{
    public class Vozilo
    {
        public Vozilo() { }

        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Proizvođač je obavezan.")]
        public Proizvodjac proizvodjac { get; set; }

        [Required(ErrorMessage = "Model je obavezan.")]
        public string model { get; set; }

        [Required(ErrorMessage = "Godina proizvodnje je obavezna.")]
        public int godinaProizvodnje { get; set; }

        [Required(ErrorMessage = "Registarska oznaka je obavezna.")]
        public string registarskaOznaka { get; set; }

        [Required(ErrorMessage = "Boja je obavezna.")]
        public Boja boja { get; set; }

        [Required(ErrorMessage = "Broj sjedišta je obavezan.")]
        [Range(1, int.MaxValue, ErrorMessage = "Broj sjedišta mora biti veći od 0.")]
        public int brojSjedista { get; set; }
    }
}
