using Domain.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Functions.FinancialCalendar.Startup))]

namespace Functions.FinancialCalendar
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {            

            builder.Services.AddSingleton<IFinancialCalendarService>((s) => {return new FinancialCalendarService();});            

            builder.Services.AddOptions<FinancialCalendarOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("FinancialCalendarOptions").Bind(settings);
            });            
        }
    }
}
