using System.ComponentModel.DataAnnotations;
namespace VoziMe.Models {
    public class Firma {
        public Firma() { }
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ime mora sadržavati između 3 i 50 karaktera.")]
        [RegularExpression(@"[a-zA-Z\s]+", ErrorMessage = "Ime može sadržavati samo slova.")]
        public string ime { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [MinLength(8, ErrorMessage = "Lozinka mora sadržavati najmanje 8 karaktera.")]
        public string lozinka { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Broj telefona može sadržavati samo brojeve.")]
        public string brojTelefona { get; set; }
        public string mailAdresa { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna.")]
        public string adresa { get; set; }

        [Required(ErrorMessage = "Odgovorna osoba je obavezna.")]
        public string odgovornaOsoba { get; set; }
    }
}
