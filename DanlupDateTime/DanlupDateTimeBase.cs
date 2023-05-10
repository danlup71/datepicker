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
    protected int columnDay1 = 0;  

    protected bool needFifthRow = false;

    protected bool needSixthRow = false;

    protected string message = "";

    protected override void OnParametersSet()
    {
        SetData();
        base.OnParametersSet();
    }

    private void SetData()
    {
        columnDay1 = current.calendarColumnDay1() - 1;

        needFifthRow = current.calendarFifthRowNeeded();

        needSixthRow = current.calendarSixthRowNeeded();
    }

    protected int GetDayNumberPerColumn(int column)
    {
        if (!columnInRange(column, columnDay1, current))
            return 0;

        return column - columnDay1;
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
