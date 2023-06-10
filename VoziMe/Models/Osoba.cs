using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models
{
    public abstract class Osoba
    {
        public Osoba() { }

        [Key]
        public int id { get; set; }

        public Spol spol { get; set; }

        [Required(ErrorMessage = "Datum rođenja je obavezan.")]
        [Display(Name = "Datum rođenja")]
        [DataType(DataType.Date)]
        public DateTime datumRodjenja { get; set; }

        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ime mora sadržavati između 3 i 50 karaktera.")]
        [RegularExpression(@"[a-zA-Z\s]+", ErrorMessage = "Ime može sadržavati samo slova.")]
        public string ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Prezime mora sadržavati između 3 i 50 karaktera.")]
        [RegularExpression(@"[a-zA-Z\s]+", ErrorMessage = "Prezime može sadržavati samo slova.")]
        public string prezime { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Korisničko ime mora sadržavati između 5 i 20 karaktera.")]
        public string korisnickoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [MinLength(8, ErrorMessage = "Lozinka mora sadržavati najmanje 8 karaktera.")]
        public string lozinka { get; set; }

        [Required(ErrorMessage = "Mail adresa je obavezna.")]
        [EmailAddress(ErrorMessage = "Mail adresa nije validna.")]
        public string mailAdresa { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna.")]
        public string adresa { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Broj telefona može sadržavati samo brojeve.")]
        public string brojTelefona { get; set; }
    }
}
