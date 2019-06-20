using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessDates;
using System.Collections.Generic;

namespace BusinessDatesTests
{
    [TestClass]
    public class BusinessDatesCounterTests
    {
        [TestMethod]
        public void TestCountWeekdays()
        {
            var counter = new BusinessDayCounter();

            var beginDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            Assert.AreEqual(counter.WeekdaysBetweenTwoDates(beginDate, endDate), 1);

            beginDate = new DateTime(2013, 10, 5);
            endDate = new DateTime(2013, 10, 14);
            Assert.AreEqual(counter.WeekdaysBetweenTwoDates(beginDate, endDate), 5);

            beginDate = new DateTime(2013, 10, 7);
            endDate = new DateTime(2014, 1, 1);
            Assert.AreEqual(counter.WeekdaysBetweenTwoDates(beginDate, endDate), 61);

            beginDate = new DateTime(2013, 10, 7);
            endDate = new DateTime(2013, 10, 5);
            Assert.AreEqual(counter.WeekdaysBetweenTwoDates(beginDate, endDate), 0);
        }

        [TestMethod]
        public void TestCountBusinessDays()
        {
            var counter = new BusinessDayCounter();

            var holidays = new List<DateTime>
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };

            var beginDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            Assert.AreEqual(counter.BusinessBetweenTwoDates(beginDate, endDate, holidays), 1);

            beginDate = new DateTime(2013, 12, 24);
            endDate = new DateTime(2013, 12, 27);
            Assert.AreEqual(counter.BusinessBetweenTwoDates(beginDate, endDate, holidays), 0);

            beginDate = new DateTime(2013, 10, 7);
            endDate = new DateTime(2014, 1, 1);
            Assert.AreEqual(counter.BusinessBetweenTwoDates(beginDate, endDate, holidays), 59);            
        }

        [TestMethod]
        public void TestCountBusinessDaysUsingRules()
        {
            var counter = new BusinessDayCounter();

            var holidayRules = new List<IHolidayRule>()
            {
                new AlwaysOnSameDayRule
                {
                    Day = 25,
                    Month = 4
                },
                new AlwaysOnSameDayExceptOnWeekendsRule
                {
                    Day = 1,
                    Month = 1
                },
                new CertainOccurenceOfCertainDayInAMonthRule
                {
                    DayOfWeek = DayOfWeek.Monday,
                    Month = 6,
                    WeekOfMonth = 2
                },
                new AlwaysOnSameDayRule
                {
                    Day = 25,
                    Month = 12
                },
                new AlwaysOnSameDayRule
                {
                    Day = 26,
                    Month = 12
                },
            };

            var beginDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            Assert.AreEqual(counter.BusinessBetweenTwoDates(beginDate, endDate, holidayRules), 1);

            beginDate = new DateTime(2013, 12, 24);
            endDate = new DateTime(2013, 12, 27);
            Assert.AreEqual(counter.BusinessBetweenTwoDates(beginDate, endDate, holidayRules), 0);

            beginDate = new DateTime(2013, 10, 7);
            endDate = new DateTime(2014, 1, 1);
            Assert.AreEqual(counter.BusinessBetweenTwoDates(beginDate, endDate, holidayRules), 59);
        }
    }
}
