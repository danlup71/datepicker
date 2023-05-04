# datepicker
DatePicker component for Blazor - No Javascript

### A little bit of history
Too often I had to struggle with the native DateTimePicker from Blazor as it lacks of some customization especially when it comes to handle the events and the behaviour. Even in order to change the style you need to enable some advanced features in the developer tools on the browser. Plus you can't debug it and if you look for some more documentation on-line... no way looks like it's forbidden... hidden in the deep javascript jungle...

### Javascript in 2023
 Well I wrote a bunch of code in Javascript... and the DOM...  and JQuery but now.. after I discovered Blazor I thought it was over and still it's here... surrounding anything that's a little bit more than basic... so my thought was:
 > But the logic it's pretty simple.. don't wanna use any JOPS or worse... let's give it a try

| Name | Role | Description |
| --- | --- | --- |
| current | parameter | you set the initial date here |
| SelectionChanged | event | raised when the user select a value |


### Try it in your project [^1]
This control is not optimized just left it simple and very basicx in order to allow everybody to use it and take advantage.. 


<details>

<summary>DanlupDateTime.razor</summary>
  
``` 

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
            @for (int day = 1; day <= 7; day++)
            {
                <td><Day Selected="DayFired" day="@GetDayNumber(day)"></Day></td>
            }
        </tr>
        <tr>
            @for (int day = 8; day <= 14; day++)
            {
                <td><Day Selected="DayFired" day="@GetDayNumber(day)"></Day></td>
            }
        </tr>
        <tr>
            @for (int day = 15; day <= 21; day++)
            {
                <td><Day Selected="DayFired" day="@GetDayNumber(day)"></Day></td>
            }
        </tr>
        <tr>
            @for (int day = 22; day <= 28; day++)
            {
                <td><Day Selected="DayFired" day="@GetDayNumber(day)"></Day></td>
            }
        </tr>
        @if (fiftyRow)
        {
            <tr>
                @for (int day = 29; day <= 35; day++)
                {
                    <td><Day Selected="DayFired" day="@GetDayNumber(day)"></Day></td>
                }
            </tr>
        }
        @if (sixtyRow)
        {
        <tr>
            @for (int day = 36; day <= 42; day++)
            {
                    <td><Day Selected="DayFired" day="@GetDayNumber(day)"></Day></td>
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

```

</details>


<details>

<summary>Day.razor</summary>
  
``` 

<div class="@daySelected" @onmousedown="MouseClick">@(day > 0 ? $"{day}" : "")</div>
@code {
    [Parameter]
    public int day { get; set; }

    [CascadingParameter(Name = "currentDay")]
    public DateTime currentDay { get; set; }

    [Parameter]
    public EventCallback<int> Selected { get; set; }

    public void MouseClick()
    {
        Selected.InvokeAsync(day);
    }

    private string daySelected => (day == currentDay.Day? "daySelected" : "");
    
}

```

</details>


  
Every suggestion is welcome.
Happy coding



[^1]: this project is not subject to any copyright. You can use and improve as much as you want..

