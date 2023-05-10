namespace DanlupDateTime;

public static class DateTimeExtensions
{
    public static int calendarColumnDay1(this DateTime _date)
    { 
        // 1 get the day/column of the 1s of current month Monday based
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