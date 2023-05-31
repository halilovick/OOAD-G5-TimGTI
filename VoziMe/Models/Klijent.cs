using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models {
    public class Klijent : Osoba {
        public Klijent(){ }
        public int ocjena { get; set; }
    }
}
