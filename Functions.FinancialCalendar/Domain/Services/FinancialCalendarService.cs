using Domain.Models;

namespace Domain.Services
{
    public class FinancialCalendarService : IFinancialCalendarService
    {        
        public List<FinancialCalendar> GetFinancialCalendars(DateOnly startDate, int pageNo, int calendarSet, int startMonthOfFinancialYear)
        {
            var calendars = new List<FinancialCalendar>();

            int year = startDate.Year + (calendarSet * (pageNo - 1));

            for (int i = 0; i < calendarSet; i++)
            {
                DateOnly date = new DateOnly(year + i, startDate.Month, startDate.Day);

                var firstLastFinancialYear = GetFirstLastFinancialYear(date, startMonthOfFinancialYear);

                var calendar = new FinancialCalendar
                {
                    FinancialYearStart = firstLastFinancialYear.Item1.Year,
                    FinancialYearEnd = firstLastFinancialYear.Item2.Year,
                };

                List<FinancialMonth> dates = GetMonths(firstLastFinancialYear.Item1, firstLastFinancialYear.Item2);

                calendar.Months = dates;

                calendars.Add(calendar);
                
            }

            return calendars;

        }

        private Tuple<DateOnly, DateOnly> GetFirstLastFinancialYear(DateOnly date, int startMonthOfFinancialYear)
        {
            if (date.Month >= startMonthOfFinancialYear)
            {
                return new Tuple<DateOnly, DateOnly>(
                    new DateOnly(date.Year, startMonthOfFinancialYear, 1),
                    new DateOnly(date.Year + 1, startMonthOfFinancialYear - 1, DateTime.DaysInMonth(date.Year + 1, startMonthOfFinancialYear - 1))
                    );
            }
            else
            {
                return new Tuple<DateOnly, DateOnly>(
                    new DateOnly(date.Year - 1, startMonthOfFinancialYear, 1),
                    new DateOnly(date.Year, startMonthOfFinancialYear - 1, DateTime.DaysInMonth(date.Year, startMonthOfFinancialYear - 1))
                    );
            }
        }

        private List<FinancialMonth> GetMonths(DateOnly startDate, DateOnly endDate)
        {
            List<FinancialMonth> months = new List<FinancialMonth>();

            int monthIndex = 1;
            while (startDate <= endDate)
            {                
                int daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);

                months.Add(new FinancialMonth
                {
                    StartDate = startDate.ToString("yyyy-MM-dd"),
                    EndDate = new DateOnly(startDate.Year, startDate.Month, daysInMonth).ToString("yyyy-MM-dd"),
                    DaysInMonth = daysInMonth,
                    PeriodNumber = monthIndex
                });

                startDate = startDate.AddMonths(1);
                monthIndex++;
            }

            return months;
        }
    }
}
