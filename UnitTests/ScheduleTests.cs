using domain.Models;

namespace UnitTesting;

public class ScheduleTests
{
    private readonly Schedule _schedule;

    public ScheduleTests()
    {
        _schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(13, 00),
            new TimeOnly(14, 00)
        );
    }

    [Fact]
    public void IsValidTimeError_ShouldFail()
    {
        _schedule.StartOfShift = new TimeOnly(15, 00);

        var result = _schedule.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal(
            "Schedule.IsValid: EndOfShift can't be less or equal then StartOfShift.",
            result.Error
        );
    }

    [Fact]
    public void IsValid_ShouldPass()
    {
        var result = _schedule.IsValid();

        Assert.True(result.Success);
    }
}
