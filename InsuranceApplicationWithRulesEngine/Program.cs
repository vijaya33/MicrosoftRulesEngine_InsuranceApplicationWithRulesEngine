using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RulesEngine.Models;


namespace InsuranceApplicationWithRulesEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var rulesJson = File.ReadAllText("Rules/pricingRules.json");
            var workflows = JsonConvert.DeserializeObject<Workflow[]>(rulesJson);
            var rulesEngine = new RulesEngine.RulesEngine(workflows, null);
            builder.Services.AddSingleton(rulesEngine);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
