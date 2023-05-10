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

```HTML+Razor
<DatePicker current="@(new DateTime(2023,04,10))" SelectionChanged="YourMethodHandler"></DatePicker>
<br/>
<b>@message</b>
  
@code{
    private string @message;

    private void YourMethodHandler(DateTime date)
    {
        message = $"User selected {date.ToShortDateString()}";
    }
}
```

Every suggestion is welcome.
Happy coding



[^1]: Included tests just for a better understdanding

