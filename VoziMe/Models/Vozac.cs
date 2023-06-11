using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models
{
    public class Vozac : Osoba
    {
        public Vozac() { }

        [Required(ErrorMessage = "Broj vozačke dozvole je obavezan.")]
        public int brojVozackeDozvole { get; set; }

        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int ocjena { get; set; }
            
        public double xkord { get; set; }
        public double ykord { get; set; }
    }
}
