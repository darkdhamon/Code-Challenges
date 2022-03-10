using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Model;

namespace CodeChallenge.ViewModel
{
    public class QuotePairResult
    {
        public List<QuotePair> QuotePairs { get; }
        public int Count => QuotePairs.Count;

        public QuotePairResult(List<QuotePair> quotePairs)
        {
            QuotePairs = quotePairs;
        }
    }
}
