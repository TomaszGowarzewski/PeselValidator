using System;
using NUnit;
using NUnit.Framework;
using PeselValidator;
using PeselValidator.Utils;

namespace PeselValidator.Tests
{
    [TestFixture]
    public class IdentityHelperTests
    {
        IdentityHelper helper;

        [SetUp]
        public void SetUp()
        {
            int[] IdentityNumber = { 9, 6, 0, 1, 0, 5, 1, 3, 3, 7, 1 };
            helper = new IdentityHelper(IdentityNumber);
        }

        [Test]
        public void Ctor_Works_Properly()
        {
            int[] IdentityNumber = { 8, 6, 0, 5, 2, 6, 0, 3, 4, 8, 4 };

            Assert.DoesNotThrow(() => helper = new IdentityHelper(IdentityNumber));
        }

        [Test]
        public void Ctor_Throws_Exception_Identity_Number_Count_10()
        {
            int[] IdentityNumber = { 8, 6, 0, 5, 2, 6, 0, 3, 4, 8 };

            Assert.That(() => helper = new IdentityHelper(IdentityNumber), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Ctor_Throws_Exception_Identity_Number_Count_12()
        {
            int[] IdentityNumber = { 8, 6, 0, 5, 2, 6, 0, 3, 4, 8, 4, 1 };

            Assert.That(() => helper = new IdentityHelper(IdentityNumber), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Ctor_Works_Properly_Identity_Equals()
        {
            int[] IdentityNumber = { 8, 6, 0, 5, 2, 6, 0, 3, 4, 8, 4 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreEqual(IdentityNumber, helper.IdentityNumberArray);
        }

        [Test]
        public void Validate_Identity_Works_Properly_Return_True()
        {
            int[] IdentityNumber = { 8, 6, 0, 5, 2, 6, 0, 3, 4, 8, 4 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreEqual(true, helper.ValidateIdentity());
        }

        [Test]
        public void Validate_Identity_Return_False()
        {
            int[] IdentityNumber = { 8, 6, 0, 5, 2, 6, 0, 3, 4, 8, 6 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreEqual(false, helper.ValidateIdentity());
        }

        [Test]
        public void Get_Date_Returns_Properly_Date()
        {
            int[] IdentityNumber = { 8,0,0,3,0,7,1,4,1,6,2 };

            DateTime dateTime = new DateTime(1980, 3, 7);         
            helper = new IdentityHelper(IdentityNumber);

            Assert.AreEqual(dateTime, helper.GetDate());
        }

        [Test]
        public void Get_Date_Does_Not_throw()
        {
            int[] IdentityNumber = { 8, 0, 0, 3, 0, 7, 1, 4, 1, 6, 2 };

            DateTime dateTime = new DateTime(1980, 3, 7);
            helper = new IdentityHelper(IdentityNumber);

            Assert.DoesNotThrow(() => helper.GetDate());
        }

        [Test]
        public void Get_Date_Throws_Exception_Day_Equals_0()
        {
            int[] IdentityNumber = { 8, 0, 0, 3, 0, 0, 1, 4, 1, 6, 2 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.That(() => helper.GetDate(), Throws.TypeOf<Exception>().With.Message.EqualTo("Wrong Date Format"));
        }

        [Test]
        public void Get_Date_Throws_Exception_Month_Equals_0()
        {
            int[] IdentityNumber = { 8, 0, 0, 3, 0, 0, 1, 4, 1, 6, 2 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.That(() => helper.GetDate(), Throws.TypeOf<Exception>().With.Message.EqualTo("Wrong Date Format"));
        }

        [Test]
        public void Get_Date_Throws_Exception_Year_Equals_0()
        {
            int[] IdentityNumber = { 0, 0, 11, 3, 0, 7, 1, 4, 1, 6, 2 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.That(() => helper.GetDate(), Throws.TypeOf<Exception>().With.Message.EqualTo("Wrong Date Format"));
        }

        [Test]
        public void Get_Date_Throws_Exception_Month_Equals_13()
        {
            int[] IdentityNumber = { 8, 0, 1, 3, 0, 0, 1, 4, 1, 6, 2 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.That(() => helper.GetDate(), Throws.TypeOf<Exception>().With.Message.EqualTo("Wrong Date Format"));
        }

        [Test]
        public void Get_Date_Throws_Exception_Day_Equals_32()
        {
            int[] IdentityNumber = { 8, 0, 0, 3, 3, 2, 1, 4, 1, 6, 2 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.That(() => helper.GetDate(), Throws.TypeOf<Exception>().With.Message.EqualTo("Wrong Date Format"));
        }

        [Test]
        public void Get_Gender_Works_Properly_Returns_Male()
        {
            int[] IdentityNumber = { 8,0,0,3,0,7,0,4,2,1,3 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreEqual("Male", helper.GetGender());
        }

        [Test]
        public void Get_Gender_Works_Properly_Returns_Female()
        {
            int[] IdentityNumber = { 8,0,0,3,0,7,1,8,4,0,1 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreEqual("Female", helper.GetGender());
        }

        [Test]
        public void Get_Gender_Not_Equals_Return_Male()
        {
            int[] IdentityNumber = { 8, 0, 0, 3, 0, 7, 0, 4, 2, 1, 3 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreNotEqual("Female", helper.GetGender());
        }

        [Test]
        public void Get_Gender_Not_Equals_Return_Female()
        {
            int[] IdentityNumber = { 8, 0, 0, 3, 0, 7, 1, 8, 4, 0, 1 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreNotEqual("Male", helper.GetGender());
        }



        [Test]
        public void Get_Sum_Digit_Works_Properly_Returns_1()
        {
            int[] IdentityNumber = { 8, 0, 0, 3, 0, 7, 1, 8, 4, 0, 1 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreEqual(1, helper.GetSumDigit());
        }

        [Test]
        public void Get_Sum_Digit_Not_Equals_Returns_7()
        {
            int[] IdentityNumber = { 8,0,0,3,0,7,0,9,4,4,7 };

            helper = new IdentityHelper(IdentityNumber);

            Assert.AreNotEqual(1, helper.GetSumDigit());
        }

    }
}
