using System;
using System.Collections.Generic;

namespace BusinessDates
{
    public static class DateExtensions
    {
        public static bool IsWeekendDay(this DateTime self)
        {
            if (self.DayOfWeek == DayOfWeek.Saturday || self.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            return false;
        }

        public static bool IsWeekday(this DateTime self)
        {
            if (self.DayOfWeek == DayOfWeek.Monday ||
                self.DayOfWeek == DayOfWeek.Tuesday ||
                self.DayOfWeek == DayOfWeek.Wednesday ||
                self.DayOfWeek == DayOfWeek.Thursday ||
                self.DayOfWeek == DayOfWeek.Friday)
            {
                return true;
            }

            return false;
        }

        public static bool IsBusinessDay(this DateTime self, IList<DateTime> holidays)
        {
            if (self.IsWeekday())
            {
                if (holidays.Contains(self))
                {
                    return false;
                }
                return true;
            }

            return false;
        }        
    }
}
