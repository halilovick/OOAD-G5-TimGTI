using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace VoziMe.Models {
    public enum Spol {
        [Display(Name = "Muškarac")]
        MUSKO,
        [Display(Name = "Žena")]
        ZENSKO
    }
}
