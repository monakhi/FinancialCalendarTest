using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class FinancialCalendarTests
    {               
        private string ReadJsonContent(string fileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "json", fileName);
            return File.ReadAllText(filePath).Replace(" ", string.Empty).Replace("\n", "").Replace("\r", "");        
        }

        [TestMethod]
        [DataRow("1_StartDate_20220115_pageNo_1_calendarSet_5.json", 2022, 1, 15, 1, 5, 7)]
        [DataRow("2_StartDate_20220415_pageNo_1_calendarSet_5.json", 2022, 4, 15, 1, 5, 7)]
        [DataRow("3_StartDate_20220615_pageNo_1_calendarSet_5.json", 2022, 6, 15, 1, 5, 7)]
        [DataRow("4_StartDate_20220715_pageNo_1_calendarSet_5.json", 2022, 7, 15, 1, 5, 7)]
        [DataRow("5_StartDate_20220915_pageNo_1_calendarSet_5.json", 2022, 9, 15, 1, 5, 7)]
        [DataRow("6_StartDate_20221215_pageNo_1_calendarSet_5.json", 2022, 12, 15, 1, 5, 7)]
        [DataRow("7_StartDate_20230115_pageNo_1_calendarSet_5.json", 2023, 1, 15, 1, 5, 7)]
        [DataRow("8_StartDate_20230415_pageNo_1_calendarSet_5.json", 2023, 4, 15, 1, 5, 7)]
        [DataRow("9_StartDate_20230615_pageNo_1_calendarSet_5.json", 2023, 6, 15, 1, 5, 7)]
        [DataRow("10_StartDate_20230715_pageNo_1_calendarSet_5.json", 2023, 7, 15, 1, 5, 7)]
        [DataRow("11_StartDate_20230915_pageNo_1_calendarSet_5.json", 2023, 9, 15, 1, 5, 7)]
        [DataRow("12_StartDate_20231215_pageNo_1_calendarSet_5.json", 2023, 12, 15, 1, 5, 7)]
        [DataRow("13_StartDate_20230115_pageNo_1_calendarSet_10.json", 2023, 1, 15, 1, 10, 7)]
        [DataRow("14_StartDate_20230115_pageNo_2_calendarSet_10.json", 2023, 1, 15, 2, 10, 7)]
        [DataRow("15_StartDate_20220115_pageNo_3_calendarSet_10.json", 2023, 1, 15, 3, 10, 7)]
        public void Tests_Success(string fileName, int startDateYear, int startDateMonth, int startDateDay, int pageNo, int calendarSet, int startMonthOfFinancialYear)
        {
            var startDate = new DateOnly(startDateYear, startDateMonth, startDateDay);
            var expectedCalendarsJson = ReadJsonContent(fileName);
            var financialCalendarService = new FinancialCalendarService();
            var actualCalendars = financialCalendarService.GetFinancialCalendars(startDate, pageNo, calendarSet, startMonthOfFinancialYear);
            var actualCalendarsJson = JsonConvert.SerializeObject(actualCalendars, Formatting.None);
            Assert.AreEqual(expectedCalendarsJson, actualCalendarsJson);
        }

        [TestMethod]        
        [DataRow("15_StartDate_20220115_pageNo_3_calendarSet_10.json", 2023, 2, 29, 3, 10, 7)]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Year, Month, and Day parameters describe an un-representable DateTime.")]
        public void Test_Invalid_Date_Fail(string fileName, int startDateYear, int startDateMonth, int startDateDay, int pageNo, int calendarSet, int startMonthOfFinancialYear)
        {
            var startDate = new DateOnly(startDateYear, startDateMonth, startDateDay);
            var expectedCalendarsJson = ReadJsonContent(fileName);
            var financialCalendarService = new FinancialCalendarService();
            var actualCalendars = financialCalendarService.GetFinancialCalendars(startDate, pageNo, calendarSet, startMonthOfFinancialYear);
            var actualCalendarsJson = JsonConvert.SerializeObject(actualCalendars, Formatting.None);
            Assert.AreEqual(expectedCalendarsJson, actualCalendarsJson);
        }

    }
}