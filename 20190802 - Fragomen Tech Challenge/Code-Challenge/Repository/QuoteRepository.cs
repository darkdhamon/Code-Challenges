using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CodeChallenge.Model;
using Newtonsoft.Json;

namespace CodeChallenge.Repository
{
    public class QuoteRepository :IQuoteRepository
    {
        private static string _path = "data/ShortDb.json";
        private static ICollection<Quote> _quotes;
        private static DateTime lastLoad;


        private static ICollection<Quote> Quotes
        {
            get
            {
                if (!File.Exists(_path)) return new List<Quote>();
                if (lastLoad < File.GetLastWriteTime(_path)) _quotes = null;
                if (_quotes!=null)return _quotes;
                var contents = File.ReadAllText(_path);
                lastLoad = File.GetLastWriteTime(_path);
                return (_quotes = JsonConvert.DeserializeObject<ICollection<Quote>>(contents));
            }
        }


        public ICollection<Quote> GetAll()
        {
            return Quotes;
        }

        public ICollection<Quote> Page(int page, int size)
        {
            return Quotes.Skip(page * size).Take(size).ToList();
        }


        public Quote GetById(long id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public long Create(Quote q) 
        {
            ICollection<Quote> contents = GetAll();
            q.Id = contents.Any() ? contents.Select(x=>x.Id).Max() + 1 : 0;
            contents.Add(q);
            File.WriteAllText(_path, JsonConvert.SerializeObject(contents));
            lastLoad = File.GetLastWriteTime(_path);
            return q.Id;
        }

        public void Update(long id, Quote q)
        {
            ICollection<Quote> contents = GetAll();
            Quote found = contents.FirstOrDefault(x => x.Id == id);
            if (found == null) return;
            found.Author = q.Author;
            found.Text = q.Text;
            File.WriteAllText(_path, JsonConvert.SerializeObject(contents));
            lastLoad = File.GetLastWriteTime(_path);
        }

        public void Delete(long id)
        {
            ICollection<Quote> contents = GetAll();
            File.WriteAllText(_path, JsonConvert.SerializeObject(contents.Select(x => x.Id != id)));            
            lastLoad = File.GetLastWriteTime(_path);
        }

        public void Mode(bool smallFile)
        {
            _path = smallFile ? "data/ShortDb.json" : "data/LargeDb.json";
            _quotes = null;
        }
    }
}