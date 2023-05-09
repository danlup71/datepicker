using Microsoft.AspNetCore.Components;

namespace DanlupDateTime;

public class DanlupDateTimeBase: ComponentBase
{
    [Parameter]
    public DateTime current { get; set; } = DateTime.Now;

    [Parameter]
    public EventCallback<DateTime> SelectionChanged { get; set; }

    // M   T   W   T   F   S   S
    //             1   2   3   4   ..  ..  ..
    // column of 1st day of the month compared to Monday based week
    protected int ColumnDay1 = 0;  

    protected bool fifthRow = false;

    protected bool sixthRow = false;

    protected string message = "";

    protected override void OnParametersSet()
    {
        SetData();
        base.OnParametersSet();
    }

    private void SetData()
    {
        DateTime firstDayOfCurrentMonth = current.AddDays(1 - current.Day);
        int dayOfWeek = (int)firstDayOfCurrentMonth.DayOfWeek;
        ColumnDay1 = dayOfWeek == 0 ? 6 : dayOfWeek - 1;

        // accordingly to the number of days and the column of day one
        // calculate the visibility of the 5th and 6th row 
        int nDaysInMonth = DateTime.DaysInMonth(current.Year, current.Month);
        fifthRow = ColumnDay1 > 0 || nDaysInMonth > 28;
        sixthRow = ColumnDay1 + nDaysInMonth > 35;
    }

    protected int GetDayNumberPerColumn(int column)
    {
        if (!columnInRange(column, ColumnDay1, current))
            return 0;

        return column - ColumnDay1;
    }

    private bool columnInRange(int column, int columnDay1, DateTime current)
    {
        if (column - columnDay1 < 1) return false;

        if (column - columnDay1 > DateTime.DaysInMonth(current.Year, current.Month)) return false;

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

    public void DateSelected(int day)
    {
        message = $" selected = {new DateTime(current.Year,current.Month,day).ToShortDateString()}";
        current = new DateTime(current.Year, current.Month, day);

        NotifyParent();
    }

    private void NotifyParent()
    {
        SelectionChanged.InvokeAsync(current);
    }
}
