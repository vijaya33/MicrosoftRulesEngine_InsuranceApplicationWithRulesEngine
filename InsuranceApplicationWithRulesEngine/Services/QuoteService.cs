using InsuranceApplicationWithRulesEngine.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using RulesEngine.Models;
using System.Configuration;


namespace InsuranceApplicationWithRulesEngine.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly RulesEngine.RulesEngine _rulesEngine;
        private readonly IConfiguration _configuration;
      //  private InsuranceApplicationWithRulesEngine.Data.InsuranceDbContext _dbContext;
        public QuoteService(RulesEngine.RulesEngine rulesEngine)
        {
            _rulesEngine = rulesEngine;        
            
        }

        /*
        public QuoteService(InsuranceApplicationWithRulesEngine dbContext, RulesEngine.RulesEngine rulesEngine, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _rulesEngine = rulesEngine;
            _configuration = configuration;
        }
        */

        public QuoteService(RulesEngine.RulesEngine rulesEngine, IConfiguration configuration)
        {            
            _rulesEngine = rulesEngine;
            _configuration = configuration;
        }

        public async Task<QuoteResult> GetQuoteAsync(Customer customer)
        {
           
            var inputs = new RuleParameter("input", customer);
            var resultList = await _rulesEngine.ExecuteAllRulesAsync("InsurancePricing", inputs);

            var matched = resultList.FirstOrDefault(r => r.IsSuccess);
            if (matched != null)
            {
                dynamic context = matched.ActionResult?.Output;
                decimal basePrice = context.BasePrice;
                decimal multiplier = context.Multiplier;

                decimal price = basePrice * (decimal)multiplier;
                if (customer.IsFamilyPlan)
                    price *= customer.FamilyMembers;

                return new QuoteResult
                {
                    Plan = customer.PlanType,
                    Price = price,
                    Message = "Quote generated successfully"
                };
            }

            return new QuoteResult
            {
                Plan = customer.PlanType,
                Price = 0,
                Message = "No matching rule found"
            };
        }

        public async Task<QuoteResult> GetQuoteViaProcedureAsync(int customerId, string planType)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("usp_CalculateQuote", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CustomerId", customerId);
            command.Parameters.AddWithValue("@PlanType", planType);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (reader.Read())
            {
                return new QuoteResult
                {
                    Plan = planType,
                    Price = reader.GetDecimal(reader.GetOrdinal("FinalPrice")),
                    Message = "Quote calculated using stored procedure."
                };
            }

            return new QuoteResult
            {
                Plan = planType,
                Price = 0,
                Message = "No quote generated from stored procedure."
            };
        }
    }
}
