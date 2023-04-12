using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class FinancialMonth
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int PeriodNumber { get; set; }
        public int DaysInMonth { get; set; }
    }
}
