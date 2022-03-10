using CodeChallenge.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockDataController : ControllerBase
    {
        private readonly IQuoteRepository _quoteRepository;

        public MockDataController(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }
        // Get api/Quotes/Mode
        /// <summary>
        /// Allows user to switch between SmallDB and LargeDB at runtime.
        /// </summary>
        /// <remarks>
        /// # Testing Only
        /// Set smallFile to true for SmallDB or false for largeDB
        /// </remarks>
        /// <param name="smallFile"></param>
        /// <returns></returns>
        [HttpGet("mode/{smallFile}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Mode(bool smallFile)
        {
            _quoteRepository.Mode(smallFile);
            return Ok();
        }
    }
}