using InsuranceApplicationWithRulesEngine.Models;
using System.Threading.Tasks;

namespace InsuranceApplicationWithRulesEngine.Services
{
   
    public interface IQuoteService
    {
        /// <summary>
        /// Calculates the insurance quote using in-memory rules engine.
        /// </summary>
        /// <param name="customer">Customer details for the quote.</param>
        /// <returns>QuoteResult with price and message.</returns>
        Task<QuoteResult> GetQuoteAsync(Customer customer);

        /// <summary>
        /// Calculates the quote using a SQL Server stored procedure.
        /// </summary>
        /// <param name="customerId">Customer ID already stored in the DB.</param>
        /// <param name="planType">The plan type selected by the customer (e.g., Basic, Premium).</param>
        /// <returns>QuoteResult with final price and message.</returns>
        Task<QuoteResult> GetQuoteViaProcedureAsync(int customerId, string planType);
    }
}

