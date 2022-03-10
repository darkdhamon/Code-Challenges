using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CodeChallenge.Controllers;
using CodeChallenge.Model;
using CodeChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    //TODO: Build Unit Tests
    [TestClass]
    public class QuotesControllerUnitTests
    {
        private QuotesController Controller { get; set; }

        [TestInitialize]
        public void Init()
        {
            var mockRepo = new Mock<IQuoteRepository>();
            mockRepo.Setup(repository =>  repository.Create(It.IsAny<Quote>())).Returns(1);
            mockRepo.Setup(repository => repository.GetAll()).Returns(new List<Quote>
            {
                new Quote()
                {
                    Id = 0,
                    Text = "T",
                    Author = "T"
                },
                new Quote()
                {
                    Id = 1,
                    Text = "T",
                    Author = "Te"
                },
                new Quote()
                {
                    Id = 2,
                    Text = "T",
                    Author = "Tes"
                },
                new Quote()
                {
                    Id = 3,
                    Text = "T",
                    Author = "Test"
                },
                new Quote()
                {
                    Id = 4,
                    Text = "T",
                    Author = "Test1"
                }
            });
            mockRepo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new Quote());
            mockRepo.Setup(repository => repository.Page(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Quote>
            {
                new Quote()
                {
                    Id = 0,
                    Text = "T",
                    Author = "T"
                },
                new Quote()
                {
                    Id = 1,
                    Text = "T",
                    Author = "Te"
                },
                new Quote()
                {
                    Id = 2,
                    Text = "T",
                    Author = "Tes"
                },
                new Quote()
                {
                    Id = 3,
                    Text = "T",
                    Author = "Test"
                },
                new Quote()
                {
                    Id = 4,
                    Text = "T",
                    Author = "Test1"
                }
            });
            Controller = new QuotesController(mockRepo.Object, NullLogger<QuotesController>.Instance);
        }
        
        [TestMethod]
        public async Task TestGetAllAsync()
        {
            var result = Controller.Get();
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(((HttpStatusCode)((ObjectResult) result).StatusCode),HttpStatusCode.OK);
        }
    }
}
