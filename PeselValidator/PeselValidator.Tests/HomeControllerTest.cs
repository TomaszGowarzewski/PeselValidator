using NUnit.Framework;
using PeselValidator.Controllers;
using PeselValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PeselValidator.Tests
{
    [TestFixture]
    class HomeControllerTest
    {
        HomeController controller;

        [SetUp]
        public void SetUp ()
        {
            controller = new HomeController();
        }

        [Test]
        public void Test_View()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [Test]
        public void Test_View_Person_Response_Null_Redirect_Check_Controller()
        {
            var result = controller.Index(null, "check", "check") as RedirectToRouteResult;
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [Test]
        public void Test_View_Person_Response_Null_Redirect_Check_Action()
        {
            var result = controller.Index(null, "check", "check") as RedirectToRouteResult;
            Assert.AreEqual("BadRequest", result.RouteValues["action"]);
        }

        [Test]
        public void Test_View_Identity_Number_Array_Null_Redirect_Check_Action()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = null;

            var result = controller.Index(model, "check", "check") as RedirectToRouteResult;

            Assert.AreEqual("BadRequest", result.RouteValues["action"]);
        }

        [Test]
        public void Test_View_Identity_Number_Array_Null_Redirect_Check_Controller()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = null;

            var result = controller.Index(model, "check", "check") as RedirectToRouteResult;

            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [Test]
        public void Test_View_Identity_Number_Array_Null_Redirect_Check_Message()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = null;

            var result = controller.Index(model, "check", "check") as RedirectToRouteResult;

            Assert.AreEqual("Value cannot be null.\r\nParameter name: source", result.RouteValues["Message"]);
        }


        [Test]
        public void Test_View_Validate_Identity_Number_Returns_False()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = "44050107691";

            var result = controller.Index(model, "check", "check") as ViewResult;
          
            Assert.AreEqual("", result.ViewName);
        }

        [Test]
        public void Test_View_Validate_Identity_Number_Returns_False_Check_Model_Error()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = "44050107691";

            var result = controller.Index(model, "check", "check") as ViewResult;

            Assert.IsTrue(controller.ViewData.ModelState.Count == 1,
                  "Niestety ale PESEL nie jest prawidłowy ;( ");
        }

        [Test]
        public void Test_View_Check_Identity_Number_When_Button_Clicked_Check_View_Name()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = "95050409237";
            model.DateOfBirth = "04.05.1995";
            model.Gender = "Male";
            model.Name = "Janusz";

            var result = controller.Index(model, "", "check") as ViewResult;

            Assert.AreEqual("InformationDisplay", result.ViewName);

        }

        [Test]
        public void Test_View_Compare_Date_And_Gender_When_Button_Clicked_Check_View_Name()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = "95050409237";
            model.DateOfBirth = "04.05.1995";
            model.Gender = "Male";
            model.Name = "Janusz";

            var result = controller.Index(model, "check", "") as ViewResult;

            Assert.AreEqual("DateAndGenderEquals", result.ViewName);

        }

        [Test]
        public void Test_View_Compare_Date_And_Gender_When_Button_Clicked_Check_Error_Date_Are_Not_Equal()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = "95050409237";
            model.DateOfBirth = "04.06.1995";
            model.Gender = "Male";
            model.Name = "Janusz";

            var result = controller.Index(model, "check", "") as ViewResult;

            Assert.IsTrue(controller.ViewData.ModelState.Count == 1,
                  "Niestety ale dane nie są jednakowe ;( ");
        }

        [Test]
        public void Test_View_Compare_Date_And_Gender_When_Button_Clicked_Check_Error_Gender_Are_Not_Equal()
        {
            PersonModel model = new PersonModel();
            model.IdentityNumber = "95050409237";
            model.DateOfBirth = "04.05.1995";
            model.Gender = "Female";
            model.Name = "Janusz";

            var result = controller.Index(model, "check", "") as ViewResult;

            Assert.IsTrue(controller.ViewData.ModelState.Count == 1,
                  "Niestety ale dane nie są jednakowe ;( ");
        }


        [Test]
        public void Test_Compare_Dates_Dates_Are_Equals_Expected_Success()
        {
            DateTime dateTime1 = new DateTime(2017, 03, 10);
            DateTime dateTime2 = new DateTime(2017, 03, 10);

            var result = controller.CompareDates(dateTime1, dateTime2);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Test_Compare_Dates_Dates_Are_Equals_Expected_Fail()
        {
            DateTime dateTime1 = new DateTime(2015, 03, 10);
            DateTime dateTime2 = new DateTime(2017, 03, 10);

            var result = controller.CompareDates(dateTime1, dateTime2);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Test_Compare_Dates_Dates_Are_Not_Equals_Expected_Success()
        {
            DateTime dateTime1 = new DateTime(2015, 03, 10);
            DateTime dateTime2 = new DateTime(2017, 03, 10);

            var result = controller.CompareDates(dateTime1, dateTime2);

            Assert.AreNotEqual(true, result);
        }

        [Test]
        public void Test_Compare_Gender_Gender_Are_Equals_Expected_Success()
        {
            string gender1 = "Male";
            string gender2 = "Male";

            var result = controller.CompareGender(gender1, gender2);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Test_Compare_Gender_Gender_Are_Equals_Expected_Fail()
        {
            string gender1 = "Male";
            string gender2 = "Female";

            var result = controller.CompareGender(gender1, gender2);

            Assert.AreEqual(false, result);
        }
    }
}
