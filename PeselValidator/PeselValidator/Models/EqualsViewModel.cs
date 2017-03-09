using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeselValidator.Models
{
    public class EqualsViewModel
    {
        public PersonModel PersonFromIdentity { get; set; }
        public PersonModel PersonFromForm { get; set; }
    }
}