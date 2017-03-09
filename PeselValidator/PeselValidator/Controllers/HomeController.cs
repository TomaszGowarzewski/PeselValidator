using PeselValidator.Models;
using PeselValidator.Properties;
using PeselValidator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PeselValidator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PersonModel personResponse, string checkCorrectnessOfDateAndID, string checkDateAndGender)
        {
            if (personResponse == null)
            {
                return RedirectToAction("BadRequest", "Home");
            }
            else if (ModelState.IsValid)
            {
                int[] IdentityNumberArray;
                try
                {
                    IdentityNumberArray = personResponse.IdentityNumber.Select(x => int.Parse(x.ToString())).ToArray();
                }
                catch
                {
                    return RedirectToAction("BadRequest", "Home");
                }

                IdentityHelper helper = new IdentityHelper(IdentityNumberArray);
                if (!ValidateIdentityNumber(helper))
                {
                    return ErrorWrongIdentityNumber();
                }

                DateTime dateTimeFromIdentity = helper.GetDate();
                string genderFromIdentity = helper.GetGender();

                if (String.IsNullOrEmpty(checkCorrectnessOfDateAndID))
                {
                    return CheckIdentityNumber(helper, dateTimeFromIdentity, genderFromIdentity, personResponse);

                }
                else if (String.IsNullOrEmpty(checkDateAndGender))
                {
                    return CompareDateAndGender(dateTimeFromIdentity, personResponse, genderFromIdentity);
                }
                return View();
            }
            return View();
        }

        private ActionResult ErrorWrongIdentityNumber()
        {
            ModelState.AddModelError(string.Empty,Resources.WrongIdentity);
            return View();
        }

        private ActionResult CheckIdentityNumber(IdentityHelper helper, DateTime dateTimeFromIdentity, string genderFromIdentity, PersonModel personResponse)
        {
            PersonModel newPersonModel = new PersonModel();
            newPersonModel.DateOfBirth = dateTimeFromIdentity.ToLongDateString();
            newPersonModel.Gender = genderFromIdentity;
            newPersonModel.Name = personResponse.Name;
            return View("InformationDisplay", newPersonModel);
        }

        [HttpGet]
        private ViewResult CompareDateAndGender(DateTime dateTimeFromIdentity, PersonModel personResponse, string genderFromIdentity)
        {
            if (CompareDates(dateTimeFromIdentity, DateTime.Parse(personResponse.DateOfBirth)) && CompareGender(genderFromIdentity, personResponse.Gender))
            {
                PersonModel personFromIdentity = new PersonModel();
                personFromIdentity.DateOfBirth = dateTimeFromIdentity.ToLongDateString();
                personFromIdentity.Gender = genderFromIdentity;

                EqualsViewModel model = new EqualsViewModel();
                model.PersonFromForm = personResponse;
                model.PersonFromIdentity = personFromIdentity;

                return View("DateAndGenderEquals", model);
            }
            ModelState.AddModelError(string.Empty,Resources.IdentityAndDateAreNotEquals);
            return View();
        }

        [HttpGet]
        public ContentResult BadRequest()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(Resources.BadRequest);
        }


        private bool CompareGender(string genderFromIdentityNumber, string genderFromResponse)
        {
            return string.Equals(genderFromIdentityNumber, genderFromResponse);
        }

        private bool CompareDates(DateTime dateFromIdentityNumber, DateTime dateFromResponse)
        {
            return (DateTime.Equals(dateFromIdentityNumber, dateFromResponse));
        }

        private bool ValidateIdentityNumber(IdentityHelper helper)
        {
            return helper.ValidateIdentity();
        }
    }
}