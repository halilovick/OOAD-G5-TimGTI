using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models {
    public class Vozac : Osoba {
        public Vozac() { }
        public int brojVozackeDozvole { get; set; }
        public int ocjena { get; set; }
    }
}
