/*
    Implementation was found here: http://rosettacode.org/wiki/Luhn_test_of_credit_card_numbers#C.23
*/

namespace DeveloperAdventures.OffTheShelf.CreditCard
{
    using System;
    using System.Linq;

    public class CreditCardLogic
    {
        static Func<char, int> charToInt = c => c - '0';

        static Func<int, int> doubleDigit = n => (n * 2).ToString().Select(charToInt).Sum();

        static Func<int, bool> isOddIndex = index => index % 2 == 0;

        public static bool LuhnCheck(string creditCardNumber)
        {
            var checkSum = creditCardNumber
                .Select(charToInt)
                .Reverse()
                .Select((digit, index) => isOddIndex(index) ? digit : doubleDigit(digit))
                .Sum();

            return checkSum % 10 == 0;
        }
    }
}