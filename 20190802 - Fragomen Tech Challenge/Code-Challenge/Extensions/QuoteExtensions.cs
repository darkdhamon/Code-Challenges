
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Model;

namespace CodeChallenge.Extensions
{
    public static class QuoteExtensions
    {
        public static int Length(this Quote quote)
        {
            return quote.Text.Length;
        }
    }
}
