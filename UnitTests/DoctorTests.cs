using domain.Models;

namespace UnitTesting;

public class DoctorTests
{
    private readonly Doctor _doctor;

    public DoctorTests()
    {
        _doctor = new Doctor(1, "A", "B", "C", new Specialization(1, "D"));
    }

    [Fact]
    public void IsValidNameEmpty_ShouldFail()
    {
        _doctor.Name = string.Empty;

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor: Null or empty Name.", result.Error);
    }

    [Fact]
    public void IsValidSecondnameEmpty_ShouldFail()
    {
        _doctor.Secondname = string.Empty;

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor: Null or empty Secondname.", result.Error);
    }

    [Fact]
    public void IsValidSurnameEmpty_ShouldFail()
    {
        _doctor.Surname = string.Empty;

        var result = _doctor.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor: Null or empty Surname.", result.Error);
    }

    [Fact]
    public void IsValid_ShouldPass()
    {
        var result = _doctor.IsValid();

        Assert.True(result.Success);
    }
}
