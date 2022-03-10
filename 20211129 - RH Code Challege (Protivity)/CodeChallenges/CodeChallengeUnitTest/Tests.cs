using System;
using System.Linq;
using CodeChallenges.Challenges;
using NUnit.Framework;

namespace CodeChallengeUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("I speak Goat Latin", "Imaa peaksmaaa oatGmaaaa atinLmaaaaa")]
        [TestCase("The quick brown fox jumped over the lazy dog", "heTmaa uickqmaaa rownbmaaaa oxfmaaaaa umpedjmaaaaaa overmaaaaaaa hetmaaaaaaaa azylmaaaaaaaaa ogdmaaaaaaaaaa")]
        public void TestGoatLatinSuccess(string sentence, string expectedOutput)
        {
            var result = GoatLatin.ToGoatLatin(sentence);
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        // consecutive whitespace
        [TestCase("I speak Goat  Latin")]
        // Leading Whitespace
        [TestCase(" I speak Goat Latin")]
        // Trailing Whitespace
        [TestCase("I speak Goat Latin ")]
        // Non English letters
        [TestCase("I speak Goat 1Latin")]
        // empty string
        [TestCase("")]
        // more than 150 characters
        [TestCase("qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzcxvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuioasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm")]

        public void TestGoatLatinConstraints(string sentence)
        {
            Assert.Throws<ArgumentException>(() => GoatLatin.ToGoatLatin(sentence));
        }

        [Test]
        [TestCase("23", new [] {"ad","ae","af","bd","be","bf","cd","ce","cf"})]
        [TestCase("", new string[0])]
        [TestCase("2", new[] { "a", "b", "c" })]
        public void TestLetterComboSuccess(string digits, string[] expectedResult)
        {
            var result = LetterCombinationsOfAPhoneNumber.LetterCombinations(digits);
            Assert.AreEqual(expectedResult.Length, result.Count );
            foreach (var expectedCombo in expectedResult)
            {
                Assert.Contains(expectedCombo, result.ToArray());
            }
        }

        [Test]
        // More than 4 digits
        [TestCase("23456")]
        // digit 1
        [TestCase("1234")]
        // digit 0
        [TestCase("0234")]
        // non numeric
        [TestCase("abc")]
        public void TestLetterComboConstraints(string digits)
        {
            Assert.Throws<ArgumentException>(() => LetterCombinationsOfAPhoneNumber.LetterCombinations(digits));
        }
    }
}