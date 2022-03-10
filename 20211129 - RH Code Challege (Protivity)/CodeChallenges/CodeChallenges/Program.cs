using System;
using CodeChallenges.Challenges;

namespace CodeChallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("Letter Combo 23 test:");

            foreach (var letterCombination in LetterCombinationsOfAPhoneNumber.LetterCombinations("23"))
            {
                Console.WriteLine(letterCombination);
            }

            Console.WriteLine("Goat Latin Test:");
            Console.WriteLine(GoatLatin.ToGoatLatin("I speak Goat  Latin"));
        }
    }
}
