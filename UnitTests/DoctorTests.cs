using domain.Models;

namespace UnitTesting;

public class DoctorTests
{
    private readonly Doctor _doctor;

    public DoctorTests()
    {
        _doctor = new Doctor(1, "A", "B", "C", 1, 1);
    }

    [Fact]
    public void IsValidNameEmpty_ShouldFail()
    {
        _doctor.Name = string.Empty;

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor.IsValid: Null or empty Name.", result.Error);
    }

    [Fact]
    public void IsValidSecondnameEmpty_ShouldFail()
    {
        _doctor.Secondname = string.Empty;

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor.IsValid: Null or empty Secondname.", result.Error);
    }

    [Fact]
    public void IsValidSurnameEmpty_ShouldFail()
    {
        _doctor.Surname = string.Empty;

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor.IsValid: Null or empty Surname.", result.Error);
    }

    [Fact]
    public void IsValidAppointmentTimeMinutes_ShouldFail()
    {
        _doctor.AppointmentTimeMinutes = 0;

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor.IsValid: AppointmentTimeMinutes can't be 0.", result.Error);
    }

    [Fact]
    public void IsValid_ShouldPass()
    {
        var result = _doctor.IsValid();

        Assert.True(result.Success);
    }
}
