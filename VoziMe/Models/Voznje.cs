using Microsoft.VisualBasic;

namespace VoziMe.Models {
    public class Voznje {
        public int id;
        public int vozacId;
        public int korisnikId;
        public int firmaId;
        public int voziloId;
        public DateAndTime vrijeme;
        public int ocjena;
        public decimal cijena;
        public string adresaPolazista;
        public string adresaDolazista;
    }
}
