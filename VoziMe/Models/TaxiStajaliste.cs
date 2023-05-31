using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace VoziMe.Models {
    public class TaxiStajaliste {
        public TaxiStajaliste() { }
        [Key]
        public int id { get; set; }
        public Stajaliste ime { get; set; }
        public string adresa { get; set; }
        public int brojMjesta { get; set; }
    }
}
