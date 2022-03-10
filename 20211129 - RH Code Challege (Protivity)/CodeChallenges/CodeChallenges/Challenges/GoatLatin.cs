using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeChallenges.Challenges
{
    public class GoatLatin
    {
        private static char[] Vowels = new[] { 'a', 'e', 'i', 'o', 'u' , 'A', 'E', 'I', 'O', 'U' };
        public static string ToGoatLatin(string sentence)
        {
            if (!(Regex.IsMatch(sentence, "^[a-z,A-Z,\\s]{1,150}$" ) && Regex.IsMatch(sentence, "^\\S") && Regex.IsMatch(sentence, "\\S$") && !Regex.IsMatch(sentence, "\\s{2}")))
            {
                throw new ArgumentException("Sentence must use english characters and not have trailing, preceding or consecutive whitespace characters and contain between 1 and 150 characters inclusively");
            }

            var goatSentence = "";

            var words = sentence.Split(" ");
            var count = 1;
            foreach (var word in words)
            {
                var firstLetter = word[0];
                if (Vowels.Contains(firstLetter))
                {
                    goatSentence += $"{(string.IsNullOrEmpty(goatSentence) ? string.Empty: " ")}{word}ma";
                }
                else
                {
                    goatSentence += $"{(string.IsNullOrEmpty(goatSentence) ? string.Empty : " ")}{word.Substring(1)}{firstLetter}ma";
                }

                for (var i = 0; i < count; i++)
                {
                    goatSentence += "a";
                }

                count++;

            }

            return goatSentence;
        }
    }
}
