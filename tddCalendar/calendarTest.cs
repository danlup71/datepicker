using DanlupDateTime;

namespace UnitTest;

public class calendarTest
{
    //    February 2023
    //
    //       1|  2|  3|  4|  5|  6|  7|
    //        |   |   |   |   |   |   |
    //      M   T   W   T   F   S   S
    // 0            01  02  03  04  05 
    // 1    06  07  08  09  10  11  12
    // 2    13  14  15  16  17  18  19
    // 3    20  21  22  23  24  25  26
    // 4    27  28  
    // 5      

    //  1. Find the column of day1 per month
    //  2. Find when we need the 5th row 
    //  3. Find when we need the 6th row 


    [Fact]
    public void WhenFebruary2023_ColumnDay1_IsWednesday()
    {
        // Arrange
        // Act
        DateTime date = new DateTime(2023, 02, 05);

        // Assert
        Assert.Equal(3, date.calendarColumnDay1());
    }

    [Fact]
    public void WhenMay2023_ColumnDay1_IsMonday()
    {
        // Arrange
        // Act
        DateTime date = new DateTime(2023, 05, 05);

        // Assert
        Assert.Equal(1, date.calendarColumnDay1());
    }

    [Fact]
    public void WhenJanuary2023_ColumnDay1_IsSunday()
    {
        // Arrange
        // Act
        DateTime date = new DateTime(2023, 01, 05);

        // Assert
        Assert.Equal(7, date.calendarColumnDay1());
    }

    [Fact]
    public void WhenFebruary2023_FifthRow_IsNeeded()
    {
        // Arrange
        // Act
        DateTime date = new DateTime(2023, 02, 05);

        // Assert
        Assert.True(date.calendarFifthRowNeeded());
    }

    [Fact]
    public void WhenFebruary2021_FifthRow_IsNotNeeded()
    {
        // Arrange
        // Act
        DateTime date = new DateTime(2021, 02, 05);

        // Assert
        Assert.False(date.calendarFifthRowNeeded());
    }

    [Fact]
    public void WhenFebruary2021_SixthRow_IsNotNeeded()
    {
        // Arrange
        // Act
        DateTime date = new DateTime(2021, 02, 05);

        // Assert
        Assert.False(date.calendarSixthRowNeeded());
    }

    [Fact]
    public void WhenMay2021_SixthRow_IsNeeded()
    {
        // Arrange
        // Act
        DateTime date = new DateTime(2021, 05, 05);

        // Assert
        Assert.True(date.calendarSixthRowNeeded());
    }

}