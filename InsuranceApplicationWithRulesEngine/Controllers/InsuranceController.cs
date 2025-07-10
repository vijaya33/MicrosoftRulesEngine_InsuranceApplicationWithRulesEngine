using InsuranceApplicationWithRulesEngine.Models;
using InsuranceApplicationWithRulesEngine.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApplicationWithRulesEngine.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public InsuranceController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpPost("quote")]
        public async Task<ActionResult<QuoteResult>> GetQuote([FromBody] Customer customer)
        {
            var result = await _quoteService.GetQuoteAsync(customer);
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }
    }


}
