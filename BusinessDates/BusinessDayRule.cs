using System;

namespace BusinessDates
{
    public interface IHolidayRule
    {
        bool IsHoliday(DateTime date);
    }

    public class AlwaysOnSameDayRule : IHolidayRule
    {
        public int Day
        {
            get; set;
        }

        public int Month
        {
            get; set;
        }

        public bool IsHoliday(DateTime date)
        {
            if (date.Day == this.Day && date.Month == this.Month)
            {
                return true;
            }

            return false;
        }        
    }

    public class AlwaysOnSameDayExceptOnWeekendsRule : IHolidayRule
    {
        public int Day
        {
            get; set;
        }

        public int Month
        {
            get; set;
        }

        public bool IsHoliday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Monday)
            {
                // Check if there was a holiday that landed on the weekend
                var saturday = date.AddDays(-2);
                var sunday = date.AddDays(-1);

                if ((saturday.Day == this.Day && saturday.Month == this.Month) ||
                    (sunday.Day == this.Day && sunday.Month == this.Month))
                {
                    return true;
                }

                return false;
            }

            if (date.IsWeekday())
            {
                if (date.Day == this.Day && date.Month == this.Month)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }

    public class CertainOccurenceOfCertainDayInAMonthRule : IHolidayRule
    {
        public int Month
        {
            get; set;
        }

        public int WeekOfMonth
        {
            get; set;
        }

        public DayOfWeek DayOfWeek
        {
            get; set;
        }
        
        public bool IsHoliday(DateTime date)
        {
            if (date.Month != this.Month)
            {
                return false;
            }

            if (date.DayOfWeek != this.DayOfWeek)
            {
                return false;
            }

            if (date.Day <= ((this.WeekOfMonth * 7) - 7) 
                && date.Day >= (this.WeekOfMonth * 7))
            {
                return false;
            }

            return true;
        }
    }
}
