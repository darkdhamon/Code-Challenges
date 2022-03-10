using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CodeChallenge.Extensions;
using Microsoft.AspNetCore.Mvc;
using CodeChallenge.Model;
using CodeChallenge.Repository;
using CodeChallenge.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly ILogger _logger;


        public QuotesController(IQuoteRepository quoteRepository, ILogger<QuotesController> logger)
        {
            _quoteRepository = quoteRepository;
            _logger = logger;
        }

        // GET api/quotes
        /// <summary>
        /// Get the full list of quotes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Quote>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_quoteRepository.GetAll());
            }
            catch (Exception e)
            {
                _logger.LogTrace(e,e.Message);
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Get a page of quotes 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("Page/{page}/{size}")]
        [ProducesResponseType(typeof(IEnumerable<Quote>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Page(int page, int size = 10)
        {
            try
            {
                return Ok(_quoteRepository.Page(page, size));
            }
            catch (Exception e)
            {
                _logger.LogTrace(e,e.Message);
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/quotes/5
        /// <summary>
        /// Get Quote by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Quote),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                var quote = _quoteRepository.GetById(id);
                if (quote == null) return NotFound();
                return Ok(quote);
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST api/quotes
        /// <summary>
        /// Create a new Quote
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(long),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Quote user)
        {
            try
            {
                var id = _quoteRepository.Create(user);
                return Ok(id);
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, e.Message);
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }

        }

        // PUT api/quotes/5
        /// <summary>
        /// Update an existing quote by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long id, [FromBody] Quote user)
        {
            try
            {
                if(_quoteRepository.GetById(id)==null) return NotFound();
                
                _quoteRepository.Update(id, user);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, e.Message);
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
            
        }

        // DELETE api/quotes/5
        /// <summary>
        /// Delete Quote by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(long id)
        {
            try
            {
                _quoteRepository.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/Quotes/PairByLength/40
        /// <summary>
        /// Returns a list of unique pairs where combined text length is less than specified amount.
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        [HttpGet("PairByLength/{maxLength}")]
        [Produces(typeof(QuotePairResult))]
        public IActionResult PairByLengthResult(int maxLength)
        {
            try
            {
                var quoteList = _quoteRepository.GetAll();
                var quoteCounts = new Dictionary<Quote, int>();
                foreach (var quote in quoteList)
                {
                    var length = quote.Length();
                    if (length < maxLength)
                    {
                        quoteCounts.Add(quote, length);
                    }
                }

                var quotePairs = new List<QuotePair>();
                while (quoteCounts.Count > 0)
                {
                    var currentQuote = quoteCounts.Keys.First();
                    var currentLength = quoteCounts[currentQuote];
                    quoteCounts.Remove(currentQuote);
                    foreach (var quoteCount in quoteCounts)
                    {
                        if (currentLength + quoteCount.Value <= maxLength)
                        {
                            quotePairs.Add(new QuotePair
                            {
                                One = currentQuote,
                                Two = quoteCount.Key
                            });
                        }
                    }
                }

                return Ok(new QuotePairResult(quotePairs));
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, e.Message);
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
            
        }

        //// GET api/Quotes/PairByLengthCount/40
        ///// <summary>
        ///// Returns only the count of unique pairs where combined text length is less than specified amount.
        ///// </summary>
        ///// <param name="maxLength"></param>
        ///// <returns></returns>
        //[HttpGet("PairByLengthCount/{maxLength}")]
        //[Produces(typeof(int))]
        //public IActionResult PairByLengthCount(int maxLength)
        //{
        //    try
        //    {
        //        var quoteList = _quoteRepository.GetAll();
        //        var quoteCounts = new Dictionary<Quote, int>();
        //        foreach (var quote in quoteList)
        //        {
        //            var length = quote.Length();
        //            if (length < maxLength)
        //            {
        //                quoteCounts.Add(quote, length);
        //            }
        //        }

        //        var quotePairs = new List<QuotePair>();
        //        while (quoteCounts.Count > 0)
        //        {
        //            var currentQuote = quoteCounts.Keys.First();
        //            var currentLength = quoteCounts[currentQuote];
        //            quoteCounts.Remove(currentQuote);
        //            foreach (var quoteCount in quoteCounts)
        //            {
        //                if (currentLength + quoteCount.Value <= maxLength)
        //                {
        //                    quotePairs.Add(new QuotePair
        //                    {
        //                        One = currentQuote,
        //                        Two = quoteCount.Key
        //                    });
        //                }
        //            }
        //        }

        //        return Ok(new QuotePairResult(quotePairs));
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogTrace(e, e.Message);
        //        return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
        //    }
            
        //}

        
    }
}
