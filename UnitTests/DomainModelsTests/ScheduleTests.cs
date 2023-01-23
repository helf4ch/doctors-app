using Domain.Models;

namespace UnitTests.DomainModelTests;

public class ScheduleTests
{
    private readonly Schedule _schedule;

    public ScheduleTests()
    {
        _schedule = GetModel();
    }

    public static Schedule GetModel()
    {
        return new Schedule
        {
            Id = 1,
            DoctorId = 1,
            Date = new DateOnly(2001, 9, 11),
            StartOfShift = new TimeOnly(9, 0),
            EndOfShift = new TimeOnly(17, 0)
        };
    }

    [Fact]
    public void IsValidDoctorInvalid_ShouldFail()
    {
        _schedule.DoctorId = 0;

        var result = _schedule.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Schedule.IsValid: DoctorId is invalid.", result.Error);
    }

    [Fact]
    public void IsValidTimeError_ShouldFail()
    {
        _schedule.StartOfShift = new TimeOnly(17, 00);

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
