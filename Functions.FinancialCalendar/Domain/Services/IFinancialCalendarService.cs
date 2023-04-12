using Domain.Models;

namespace Domain.Services
{
    public interface IFinancialCalendarService
    {
        List<FinancialCalendar> GetFinancialCalendars(DateOnly startDate, int pageNo, int calendarSet, int startMonthOfFinancialYear);
    }
}
