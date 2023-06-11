using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoziMe.Models
{
    public class Vozac : Osoba
    {
        public Vozac() { }

        [ForeignKey("Firma")]
        public int firmaId { get; set; }
        [ForeignKey("Vozilo")]
        public int voziloId { get; set; }

        [Required(ErrorMessage = "Broj vozačke dozvole je obavezan.")]
        public int brojVozackeDozvole { get; set; }

        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int ocjena { get; set; }
            
        public double xkord { get; set; }
        public double ykord { get; set; }

        public Firma Firma { get; set; }
        public Vozilo Vozilo { get; set; }
    }
}
