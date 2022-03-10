using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeChallenges.Challenges
{
    /// <summary>
    /// Given a string containing digits from 2-9 inclusive, return all possible letter combinations that the number could represent. Return the answer in any order.
    /// A mapping of digit to letters(just like on the telephone buttons) is given below.Note that 1 does not map to any letters.
    ///
    /// Example 1:
    /// Input: digits = "23"
    /// Output: ["ad","ae","af","bd","be","bf","cd","ce","cf"]
    ///
    /// Example 2:
    /// Input: digits = ""
    /// Output: []
    ///
    /// Example 3:
    /// Input: digits = "2"
    /// Output: ["a","b","c"]
    ///
    /// Constraints:
    ///
    /// 0 <= digits.length <= 4
    /// digits[i] is a digit in the range ['2', '9'].
    /// </summary>
    public class LetterCombinationsOfAPhoneNumber
    {
        private static string[] twoStrings = new[] {"a", "b", "c"};
        private static string[] threeStrings = new[] {"d", "e", "f"};
        private static string[] fourStrings = new[] {"g", "h", "i"};
        private static string[] fiveStrings = new[] {"j", "k", "l"};
        private static string[] sixStrings = new[] {"m", "n", "o"};
        private static string[] sevenStrings = new[] {"p", "q", "r","s"};
        private static string[] eightStrings = new[] {"t", "u", "v"};
        private static string[] nineStrings = new[] {"w", "x", "y","z"};
        

        public static IList<string> LetterCombinations(string digits)
        {
            if (string.IsNullOrEmpty(digits))
            {
                return Array.Empty<string>();
            }

            var length = digits.Length;

            if (!Regex.IsMatch(digits,"^[2-9]{0,4}$"))
            {
                throw new ArgumentException("Digits must not be greater than 4 characters long, and digits must be between 2-9 inclusively");
            }

            var results = new List<string>();
            AddNextDigit(results, digits);
            return results;
        }

        public static void AddNextDigit(IList<string> results, string digits, string currentPath = "", int index = 0)
        {
            if (index == digits.Length)
            {
                results.Add(currentPath);
                return;
            }
            string[] possibleValues;
            switch (digits[index])
            {
                case '2':
                    possibleValues = twoStrings;
                    break;
                case '3':
                    possibleValues = threeStrings;
                    break;
                case '4':
                    possibleValues = fourStrings;
                    break;
                case '5':
                    possibleValues = fiveStrings;
                    break;
                case '6':
                    possibleValues = sixStrings;
                    break;
                case '7':
                    possibleValues = sevenStrings;
                    break;
                case '8':
                    possibleValues = eightStrings;
                    break;
                case '9':
                    possibleValues = nineStrings;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            foreach (var possibleValue in possibleValues)
            {
                var tempPath = $"{currentPath}{possibleValue}";
                AddNextDigit(results,digits,tempPath,index+1);
            }
        }
    }
}
