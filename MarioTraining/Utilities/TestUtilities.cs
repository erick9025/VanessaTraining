using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFramework.Utilities
{
    public static class TestUtilities
    {
        public static string ToOrdinal(this int number)
        {
            string extension = "th"; // Start with the most common extension.
            int last_digits = number % 100; // Examine the last 2 digits.
            // If the last digits are 11, 12, or 13, use th. Otherwise:
            if (last_digits < 11 || last_digits > 13)
            {
                // Check the last digit.
                switch (last_digits % 10)
                {
                    case 1:
                        extension = "st";
                        break;
                    case 2:
                        extension = "nd";
                        break;
                    case 3:
                        extension = "rd";
                        break;
                }
            }
            return number.ToString() + extension;
        }
    }
}
