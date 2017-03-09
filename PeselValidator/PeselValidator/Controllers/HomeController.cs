using PeselValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PeselValidator.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(PersonModel personResponse, string checkCorrectnessOfDateAndID, string checkDateAndGender)
        {
            if (personResponse == null)
            {
                throw new ArgumentNullException(nameof(personResponse));
            }
            else if (ModelState.IsValid)
            {
                int[] IdentityNumberArray = personResponse.IdentityNumber.Select(x => int.Parse(x.ToString())).ToArray();
                IdentityHelper helper = new IdentityHelper(IdentityNumberArray);

                DateTime dateTimeFromIdentity = helper.GetDate();
                string genderFromIdentity = helper.GetGender();

                if (!String.IsNullOrEmpty(checkCorrectnessOfDateAndID))
                {

                    if (ValidateIdentityNumber(helper))
                    {
                        PersonModel newPersonModel = new PersonModel();
                        newPersonModel.DateOfBirth = dateTimeFromIdentity.ToLongDateString();
                        newPersonModel.Gender = genderFromIdentity;
                        newPersonModel.Name = personResponse.Name;
                        return View("InformationDisplay",newPersonModel);
                    }
                    return View("WrongInformation");
                }
               else if (!String.IsNullOrEmpty(checkDateAndGender))
                {
                    if (CompareDates(dateTimeFromIdentity, DateTime.Parse(personResponse.DateOfBirth)) && CompareGender(genderFromIdentity, personResponse.Gender))
                    {
                        return View("DateAndGenderEquals");
                    }
                    return View("DateAndGenderNotEquals");
                }
                return View();
            }
            else
            {
                return View();
            }
        }




        private bool CompareGender(string genderFromIdentityNumber, string genderFromResponse)
        {
            if (string.Equals(genderFromIdentityNumber, genderFromResponse))
            {
                return true;
            }
            return false;

        }

        private bool CompareDates(DateTime dateFromIdentityNumber, DateTime dateFromResponse)
        {
            if (DateTime.Equals(dateFromIdentityNumber, dateFromResponse))
            {
                return true;
            }
            return false;
        }

        private bool ValidateIdentityNumber(IdentityHelper helper)
        {

            if (helper.ValidateIdentity())
            {
                return true;
            }
            return false;
        }

        private ViewResult CheckCorrectnessDateAndID(PersonModel guestResponse)
        {
            throw new NotImplementedException();
        }
    }
}