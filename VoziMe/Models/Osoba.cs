using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models {
    public abstract class Osoba {
        public Osoba() { }
        [Key]
        public int id { get; set; }
        public Spol spol { get; set; }
        public DateTime datumRodjenja { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }
        public string mailAdresa { get; set; }
        public string adresa { get; set; }
        public string brojTelefona { get; set; }
    }
}
