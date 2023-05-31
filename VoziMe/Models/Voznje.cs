using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoziMe.Models {
    public class Voznje {
        public Voznje() { }
        [Key]
        public int id;
        [ForeignKey("Vozac")]
        public int vozacId { get; set; }
        [ForeignKey("Klijent")]
        public int korisnikId { get; set; }
        [ForeignKey("Firma")]
        public int firmaId { get; set; }
        [ForeignKey("Vozilo")]
        public int voziloId { get; set; }
        public DateAndTime vrijeme { get; set; }
        public int ocjena { get; set; }
        public decimal cijena { get; set; }
        public string adresaPolazista { get; set; }
        public string adresaDolazista { get; set; }

        public Firma Firma { get; set; }
        public Vozilo Vozilo { get; set; }
        public Klijent Klijent { get; set; }
        public Vozac Vozac { get; set; }
    }
}
