using System;
using System.Collections.Generic;
using System.Text;

namespace PeselValidator.Utils
{
    public class IdentityHelper
    {
        private int[] identityNumberArray;

        public int[] IdentityNumberArray
        {
            get
            {
                return identityNumberArray;
            }

           private set
            {
                identityNumberArray = value;
            }
        }

        public IdentityHelper(int[] identityNumberArray)
        {
            if (identityNumberArray.Length != 11)
            {
                throw new ArgumentException(nameof(identityNumberArray));
            }
            this.IdentityNumberArray = identityNumberArray;
        }

        public bool ValidateIdentity()
        {
          return ( GetSumDigit() == IdentityNumberArray[10]);         
        }

        public DateTime GetDate()
        {

            int day = IdentityNumberArray[4] * 10 + IdentityNumberArray[5];
            int year = 0;
            int month = 0;

            if (IdentityNumberArray[2] == 0 || IdentityNumberArray[2] == 1)
            {
                month = IdentityNumberArray[2] * 10 + IdentityNumberArray[3];
                year = 1900;
            }
            else if (IdentityNumberArray[2] == 2 || IdentityNumberArray[2] == 3)
            {
                month = IdentityNumberArray[2] * 10 + IdentityNumberArray[3] - 20;
                year = 2000;
            }
            else if (IdentityNumberArray[2] == 4 || IdentityNumberArray[2] == 5)
            {
                month = IdentityNumberArray[2] * 10 + IdentityNumberArray[3] - 40;
                year = 2100;
            }
            else if (IdentityNumberArray[2] == 6 || IdentityNumberArray[2] == 7)
            {
                month = IdentityNumberArray[2] * 10 + IdentityNumberArray[3] - 60;
                year = 2200;
            }
            else if (IdentityNumberArray[2] == 8 || IdentityNumberArray[2] == 9)
            {
                month = IdentityNumberArray[2] * 10 + IdentityNumberArray[3] - 80;
                year = 1800;
            }
            year += IdentityNumberArray[0] * 10 + IdentityNumberArray[1];

            if (year<1 || month>12 || month<1 || day>31 ||day<1)
            {
                throw new Exception("Wrong Date Format");
            }
            
            return new DateTime(year, month, day);
          
        }

        public string GetGender()
        {
            if (IdentityNumberArray[9] % 2 == 0)
            {
                return "Female";
            }
            else
            {
                return "Male";
            }
        }

        public int GetSumDigit()
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, };

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += (weights[i] * IdentityNumberArray[i]);
            }
            sum %= 10;
            sum = 10 - sum;
            return sum;
        }
    }
}