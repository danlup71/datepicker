using Microsoft.AspNetCore.Components;

namespace Danlup;

public class DatePickerBase : ComponentBase
{
    [Parameter]
    public DateTime current { get; set; } = DateTime.Now;

    [Parameter]
    public EventCallback<DateTime> SelectionChanged { get; set; }

    protected int columnDay1 => current.calendarColumnDay1() - 1;

    protected bool needFifthRow => current.calendarFifthRowNeeded();

    protected bool needSixthRow => current.calendarSixthRowNeeded();

    protected string message = "";

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
    }

    protected void NextMonth()
    {
        current = current.AddMonths(1);
    }

    public void DateSelected(int day)
    {
        message = $" selected = {new DateTime(current.Year, current.Month, day).ToShortDateString()}";
        current = new DateTime(current.Year, current.Month, day);

        NotifyParent();
    }

    private void NotifyParent()
    {
        SelectionChanged.InvokeAsync(current);
    }
}
