using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models {
    public class Klijent : Osoba {
        public Klijent(){ }
        [Key]
        public int id { get; set; }
        public int ocjena { get; set; }
    }
}
