using System;
using System.Collections.Generic;

namespace BusinessDates
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime beginDate, DateTime endDate)
        {
            if (endDate <= beginDate)
            {
                return 0;
            }

            var totalWeekdaysInBetweenDates = 0;
            var totalDaysBetweenDates = (endDate - beginDate).Days;
            var tempDate = beginDate;

            for (int i = 0; i < totalDaysBetweenDates - 1; i++)
            {
                tempDate = tempDate.AddDays(1);
                if (tempDate.IsWeekday())
                {
                    totalWeekdaysInBetweenDates++;
                }
            }

            return totalWeekdaysInBetweenDates;
        }

        public int BusinessBetweenTwoDates(DateTime beginDate, DateTime endDate, IList<DateTime> publicHolidays)
        {
            if (endDate <= beginDate)
            {
                return 0;
            }

            var totalBusinessDaysInBetweenDates = 0;
            var totalDaysBetweenDates = (endDate - beginDate).Days;
            var tempDate = beginDate;

            for (int i = 0; i < totalDaysBetweenDates - 1; i++)
            {
                tempDate = tempDate.AddDays(1);
                if (tempDate.IsBusinessDay(publicHolidays))
                {
                    totalBusinessDaysInBetweenDates++;
                }
            }

            return totalBusinessDaysInBetweenDates;
        }

        public int BusinessBetweenTwoDates(DateTime beginDate, DateTime endDate, IList<IHolidayRule> holidayRules)
        {
            if (endDate <= beginDate)
            {
                return 0;
            }

            var totalBusinessDaysInBetweenDates = 0;
            var totalDaysBetweenDates = (endDate - beginDate).Days;
            var tempDate = beginDate;

            for (int i = 0; i < totalDaysBetweenDates - 1; i++)
            {
                tempDate = tempDate.AddDays(1);
                if (tempDate.IsWeekday())
                {
                    bool isHoliday = false;
                    foreach (var rule in holidayRules)
                    {
                        if (rule.IsHoliday(tempDate))
                        {
                            isHoliday = true;
                            break;
                        }
                    }

                    if (!isHoliday)
                    {
                        totalBusinessDaysInBetweenDates++;
                    }
                }
            }

            return totalBusinessDaysInBetweenDates;
        }
    }
}
