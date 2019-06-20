using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessDates;
using System.Collections.Generic;

namespace BusinessDatesTests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void TestIsWeekday()
        {
            var date = new DateTime(2019, 6, 16); // This is a Sunday
            Assert.IsFalse(date.IsWeekday());

            date = new DateTime(2019, 6, 17); // This is a Monday
            Assert.IsTrue(date.IsWeekday());
        }

        [TestMethod]
        public void TestIsWeekend()
        {
            var date = new DateTime(2019, 6, 16); // This is a Sunday
            Assert.IsTrue(date.IsWeekendDay());

            date = new DateTime(2019, 6, 17); // This is a Monday
            Assert.IsFalse(date.IsWeekendDay());
        }

        [TestMethod]
        public void TestIsBusinessDay()
        {
            var holidays = new List<DateTime>
            {
                new DateTime(2019, 6, 16)
            };

            var date = new DateTime(2019, 6, 17); // This is a Monday
            Assert.IsTrue(date.IsBusinessDay(holidays));

            holidays.Add(new DateTime(2019, 6, 17));
            Assert.IsFalse(date.IsBusinessDay(holidays)); // Now Monday is a holiday
        }
    }
}
