using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models {
    public class Vozilo {
        public Vozilo() { }
        [Key]
        public int id { get; set; }
        public Proizvodjac proizvodjac { get; set; }
        public string model { get; set; }
        public int godinaProizvodnje { get; set; }
        public int registarskaOznaka { get; set; }
        public Boja boja { get; set; }
        public int brojSjedista { get; set; }
    }
}
