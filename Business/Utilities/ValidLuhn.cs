using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities
{
    public static class ValidLuhn
    {
        public static bool IsValidLuhn(this string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber)) return false;

            int sum = 0;
            bool shouldDouble = false;

            // Luhn Algorithm
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(cardNumber[i])) return false;

                int digit = cardNumber[i] - '0';

                if (shouldDouble)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }

                sum += digit;
                shouldDouble = !shouldDouble;
            }

            return (sum % 10 == 0);
        }
    }
}

