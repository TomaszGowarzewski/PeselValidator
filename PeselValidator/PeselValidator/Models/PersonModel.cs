using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeselValidator.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "Proszę podać Imię.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać PESEL.")]
        [RegularExpression(@"^(\d{11})",ErrorMessage = "Proszę podać PESEL w prawidłowym formacie (11 cyfrowy ciąg)")]
        public string IdentityNumber { get; set; }

        [RegularExpression(@"(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)", ErrorMessage ="proszę podać datę w formacie dd/mm/rrrr")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Proszę podać płeć.")]
        public string Gender { get; set; }

    }
}