# datepicker
DatePicker component for Blazor - No Javascript

### A little bit of history
Too often I had to struggle with the native DateTimePicker from Blazor as it lacks of some customization especially when it comes to handle the events and the behaviour. Even in order to change the style you need to enable some advanced features in the developer tools on the browser. Plus you can't debug it and if you look for some more documentation on-line... no way looks like it's forbidden... hidden in the deep javascript jungle...

### Javascript in 2023
 Well I wrote a bunch of code in Javascript... and the DOM...  and JQuery but now.. after I discovered Blazor I thought it was over and still it's here... surrounding anything that's a little bit more than basic... so my thought was:
 > But the logic it's pretty simple.. don't wanna use any JOPS or worse... let's give it a try

### Try it in your project [^1]
This control is not optimized just left it simple and very basic in order to allow everybody to use it and take advantage.. 
Your feedback would be appreciated

<details>

<summary>DanlupDateTime.razor</summary>
  
``` 

@using Microsoft.AspNetCore.Components.Web
@inherits DanlupDateTimeBase

<CascadingValue Name="currentDay" Value="@current">
<div class="danlupDate">
    <div class="row1">
        <span class="prev" @onclick="PrevMonth">&lt;&lt;</span>@current.ToString("M")<span class="next" @onclick="NextMonth">&gt;&gt;</span>
    </div>

<div class="row2">
    <table>
        <tr>
            <th><div>m</div></th>
            <th><div>t</div></th>
            <th><div>w</div></th>
            <th><div>t</div></th>
            <th><div>f</div></th>
            <th><div>s</div></th>
            <th><div>s</div></th>
        </tr>
        <tr>
            @for (int iterator = 1; iterator <= 7; iterator++)
            {
                        <td><Day MouseClick="DateSelected" day="@GetDayNumberPerColumn(iterator)"></Day></td>
            }
        </tr>
        <tr>
            @for (int iterator = 8; iterator <= 14; iterator++)
            {
                        <td><Day MouseClick="DateSelected" day="@GetDayNumberPerColumn(iterator)"></Day></td>
            }
        </tr>
        <tr>
            @for (int iterator = 15; iterator <= 21; iterator++)
            {
                        <td><Day MouseClick="DateSelected" day="@GetDayNumberPerColumn(iterator)"></Day></td>
            }
        </tr>
        <tr>
            @for (int iterator = 22; iterator <= 28; iterator++)
            {
                        <td><Day MouseClick="DateSelected" day="@GetDayNumberPerColumn(iterator)"></Day></td>
            }
        </tr>
        @if (needFifthRow)
        {
            <tr>
                @for (int iterator = 29; iterator <= 35; iterator++)
                {
                            <td><Day MouseClick="DateSelected" day="@GetDayNumberPerColumn(iterator)"></Day></td>
                }
            </tr>
        }
        @if (needSixthRow)
        {
        <tr>
            @for (int iterator = 36; iterator <= 42; iterator++)
            {
                            <td><Day MouseClick="DateSelected" day="@GetDayNumberPerColumn(iterator)"></Day></td>
            }
        </tr>
        }
    </table>
</div>

</div>
</CascadingValue>



```

</details>




<details>

<summary>DanlupDateTimeBase.cs</summary>
  
``` 
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

```

</details>


<details>

<summary>Day.razor</summary>
  
``` 

@using Microsoft.AspNetCore.Components.Web
<div class="@daySelected" @onmousedown="mouseClick">@(day > 0 ? $"{day}" : "")</div>

@code {
    [Parameter]
    public int day { get; set; }

    [CascadingParameter(Name = "currentDay")]
    public DateTime currentDay { get; set; }

    [Parameter]
    public EventCallback<int> MouseClick { get; set; }

    public void mouseClick()
    {
        MouseClick.InvokeAsync(day);
    }

    private string daySelected => (day == currentDay.Day? "daySelected" : "");
    
}

```

</details>


<details>

<summary>DateTimeExtensions.cs</summary>
  
``` 

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

```

</details>

  
Every suggestion is welcome.
Happy coding



[^1]: Included tests just for a better understdanding

