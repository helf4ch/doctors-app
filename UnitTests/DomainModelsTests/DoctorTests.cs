using Domain.Models;

namespace UnitTests.DomainModelTests;

public class DoctorTests
{
    private readonly Doctor _doctor;

    public DoctorTests()
    {
        _doctor = GetModel();
    }

    public static Doctor GetModel()
    {
        return new Doctor
        {
            Id = 1,
            Name = "Name",
            SpecializationId = 1,
            AppointmentTimeMinutes = 30
        };
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
    public void IsValidNameLenght_ShouldFail()
    {
        _doctor.Name = new String('A', 51);

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor.IsValid: Name has MaxLenght of 50.", result.Error);
    }

    [Fact]
    public void IsValidSecondnameLenght_ShouldFail()
    {
        _doctor.Secondname = new String('A', 51);

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor.IsValid: Secondname has MaxLenght of 50.", result.Error);
    }

    [Fact]
    public void IsValidSurnameLenght_ShouldFail()
    {
        _doctor.Surname = new String('A', 51);

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor.IsValid: Surname has MaxLenght of 50.", result.Error);
    }

    [Fact]
    public void IsValidSpecializationInvalid_ShouldFail()
    {
        _doctor.SpecializationId = 0;

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor.IsValid: SpecializationId is invalid.", result.Error);
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
