using Microsoft.VisualBasic;

namespace VoziMe.Models {
    public class Klijent : Osoba {
        int id;
        Spol spol;
        DateAndTime datumRodjenja;
        string adresa;
        int ocjena;
        string brojTelefona;
    }
}
