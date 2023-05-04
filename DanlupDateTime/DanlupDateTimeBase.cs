using Microsoft.AspNetCore.Components;

namespace DanlupDateTime;

public class DanlupDateTimeBase: ComponentBase
{
    [Parameter]
    public DateTime current { get; set; } = DateTime.Now;

    [Parameter]
    public EventCallback<DateTime> SelectionChanged { get; set; }

    protected int offsetMonth = 0;

    protected bool fiftyRow = false;
    protected bool sixtyRow = false;

    protected string fired = "";


    protected override void OnParametersSet()
    {
        SetData();
        base.OnParametersSet();
    }

    private void SetData()
    {
        DateTime firstDayOfCurrentMonth = current.AddDays(-current.Day + 1);
        int dayOfWeek = (int)firstDayOfCurrentMonth.DayOfWeek;
        offsetMonth = dayOfWeek == 0 ? 6 : dayOfWeek - 1;

        int nDaysInMonth = DateTime.DaysInMonth(current.Year, current.Month);
        fiftyRow = offsetMonth > 0 || nDaysInMonth > 28;
        sixtyRow = offsetMonth + nDaysInMonth > 35;
    }

    protected int GetDayNumber(int day)
    {
        if (!isValid(day, offsetMonth, current))
            return 0;

        return day - offsetMonth;
    }

    private bool isValid(int day, int offSetMonth, DateTime current)
    {
        if (day - offsetMonth < 1) return false;

        if (day - offsetMonth > DateTime.DaysInMonth(current.Year, current.Month)) return false;

        return true;
    }

    protected void PrevMonth()
    {
        current = current.AddMonths(-1);
        SetData();
    }

    protected void NextMonth()
    {
        current = current.AddMonths(1);
        SetData();
    }

    public void DayFired(int day)
    {
        fired = $" selected = {new DateTime(current.Year,current.Month,day).ToShortDateString()}";
        current = new DateTime(current.Year, current.Month, day);

        NotifyParent();
    }

    private void NotifyParent()
    {
        SelectionChanged.InvokeAsync(current);
    }
}
