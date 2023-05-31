using System.ComponentModel.DataAnnotations;
namespace VoziMe.Models {
    public class Firma {
        public Firma() { }
        [Key]
        public int id { get; set; }
        public string ime { get; set; }
        public string lozinka { get; set; }
        public string mailAdresa { get; set; }
        public string brojTelefona { get; set; }
        public string adresa { get; set; }
        public string odgovornaOsoba { get; set; }
    }
}
