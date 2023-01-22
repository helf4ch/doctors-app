using Domain.Models;

namespace UnitTests.DomainModelTests;

public class SpecializationTests
{
    private readonly Specialization _specialization;

    public SpecializationTests()
    {
        _specialization = new Specialization { Name = "Name" };
    }

    public static Specialization GetModel()
    {
        return new Specialization { Id = 1, Name = "Name" };
    }

    [Fact]
    public void IsValidNameEmpty_ShouldFail()
    {
        _specialization.Name = string.Empty;

        var result = _specialization.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Specialization.IsValid: Null or empty Name.", result.Error);
    }

    [Fact]
    public void IsValidNameLenght_ShouldFail()
    {
        _specialization.Name = new String('A', 51);

        var result = _specialization.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Specialization.IsValid: Name has MaxLenght of 50.", result.Error);
    }

    [Fact]
    public void IsValid_ShouldPass()
    {
        var result = _specialization.IsValid();

        Assert.True(result.Success);
    }
}
