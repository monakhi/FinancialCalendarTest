using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class FinancialCalendar
    {
        public int FinancialYearStart { get; set; }
        public int FinancialYearEnd { get; set; }
        public int DaysInYear
        {
            get
            {
                return Months.Select(m => m.DaysInMonth).Sum();
            }
        }
        public List<FinancialMonth> Months { get; set; }
    }
}
