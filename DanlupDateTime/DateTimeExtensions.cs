namespace Danlup;

public static class DateTimeExtensions
{
    public static int calendarColumnDay1(this DateTime _date)
    {
        // M   T   W   T   F   S   S
        //             1   2   3   4   ..  ..  ..
        // column of 1st day of the month compared to Monday based week
        DateTime dayOneCurrentMonth = new DateTime(_date.Year, _date.Month, 1);
        return (int)dayOneCurrentMonth.DayOfWeek == 0 ? 7 : (int)dayOneCurrentMonth.DayOfWeek;
    }
    
    public static bool calendarFifthRowNeeded(this DateTime _date)
    {
        return _date.calendarColumnDay1() + DateTime.DaysInMonth(_date.Year, _date.Month) - 1 > 28;
    }

    public static bool calendarSixthRowNeeded(this DateTime _date)
    {
        return _date.calendarColumnDay1() + DateTime.DaysInMonth(_date.Year, _date.Month) - 1 > 35;
    }
}