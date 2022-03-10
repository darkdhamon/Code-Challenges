using System.Collections.Generic;
using CodeChallenge.Model;

namespace CodeChallenge.Repository
{
    public interface IQuoteRepository
    {
        ICollection<Quote> GetAll();
        ICollection<Quote> Page(int page, int size);
        Quote GetById(long id);
        long Create(Quote q);
        void Update(long id, Quote q);
        void Delete(long id);
        void Mode(bool smallFile);
    }
}