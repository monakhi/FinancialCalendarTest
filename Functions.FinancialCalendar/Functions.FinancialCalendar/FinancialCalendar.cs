using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Functions.FinancialCalendar
{
    public class FinancialCalendar
    {
        private readonly IFinancialCalendarService _financialCalendarService;
        private readonly FinancialCalendarOptions _settings;
        public FinancialCalendar(IFinancialCalendarService financialCalendarService, IOptions<FinancialCalendarOptions> options)
        {
            _financialCalendarService = financialCalendarService;
            _settings = options.Value;
        }

        [FunctionName("FinancialCalendar")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("FinancialCalendar HTTP trigger function processed a request.");

                var startDate = DateOnly.Parse(req.Query["startDate"]);
                var pageNo = Int32.Parse(req.Query["pageNo"]);

                var calendars = _financialCalendarService.GetFinancialCalendars(startDate, pageNo, _settings.CalendarSet, _settings.StartMonthOfFinancialYear);

                return new OkObjectResult(calendars);

            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception: {ex}");
                var model = new { error = "Exception in generating Calendars" };
                return new ObjectResult(model)
                {
                    StatusCode = 500
                };
            }

        }
    }
}
