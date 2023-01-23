using Domain.Models;

namespace UnitTests.DomainModelTests;

public class AppointmentTests
{
    private readonly Appointment _appointment;

    public AppointmentTests()
    {
        _appointment = GetModel();
    }

    public static Appointment GetModel()
    {
        return new Appointment
        {
            Id = 1,
            UserId = 1,
            DoctorId = 1,
            Date = DateOnly.Parse(DateTime.UtcNow.ToString("d")).AddDays(1),
            StartTime = new TimeOnly(10, 3)
        };
    }

    [Fact]
    public void IsValidUserInvalid_ShouldFail()
    {
        _appointment.UserId = 0;

        var result = _appointment.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Appointment.IsValid: UserId is invalid.", result.Error);
    }

    [Fact]
    public void IsValidDoctorInvalid_ShouldFail()
    {
        _appointment.DoctorId = 0;

        var result = _appointment.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Appointment.IsValid: DoctorId is invalid.", result.Error);
    }

    [Fact]
    public void IsValidDateInvalid_ShouldFail()
    {
        _appointment.Date = DateOnly.Parse(DateTime.UtcNow.ToString("d"));

        var result = _appointment.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Appointment.IsValid: Date is invalid.", result.Error);
    }

    [Fact]
    public void IsValid_ShouldPass()
    {
        var result = _appointment.IsValid();

        Assert.True(result.Success);
    }
}
