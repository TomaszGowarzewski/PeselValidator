using System;
using System.Collections.Generic;
using System.Text;

namespace PeselValidator.Controllers
{
    internal class IdentityHelper
    {
        private int[] identityNumberArray;

        public IdentityHelper(int[] identityNumberArray)
        {
            if (identityNumberArray.Length != 11)
            {
                throw new ArgumentException(nameof(identityNumberArray));
            }
            this.identityNumberArray = identityNumberArray;
        }

        public bool ValidateIdentity()
        {
          return ( GetSumDigit() == identityNumberArray[10]);         
        }

        public DateTime GetDate()
        {

            int day = identityNumberArray[4] * 10 + identityNumberArray[5];
            int year = 0;
            int month = 0;

            if (identityNumberArray[2] == 0 || identityNumberArray[2] == 1)
            {
                month = identityNumberArray[2] * 10 + identityNumberArray[3];
                year = 1900;
            }
            else if (identityNumberArray[2] == 2 || identityNumberArray[2] == 3)
            {
                month = identityNumberArray[2] * 10 + identityNumberArray[3] - 20;
                year = 2000;
            }
            else if (identityNumberArray[2] == 4 || identityNumberArray[2] == 5)
            {
                month = identityNumberArray[2] * 10 + identityNumberArray[3] - 40;
                year = 2100;
            }
            else if (identityNumberArray[2] == 6 || identityNumberArray[2] == 7)
            {
                month = identityNumberArray[2] * 10 + identityNumberArray[3] - 60;
                year = 2200;
            }
            else if (identityNumberArray[2] == 8 || identityNumberArray[2] == 9)
            {
                month = identityNumberArray[2] * 10 + identityNumberArray[3] - 80;
                year = 1800;
            }
            year += identityNumberArray[0] * 10 + identityNumberArray[1];

            return new DateTime(year, month, day);
           
        }

        public string GetGender()
        {
            if (identityNumberArray[9] % 2 == 0)
            {
                return "Female";
            }
            else
            {
                return "Male";
            }
        }

        private int GetSumDigit()
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, };

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += (weights[i] * identityNumberArray[i]);

            }
            sum %= 10;
            sum = 10 - sum;
            return sum;
        }
    }
}