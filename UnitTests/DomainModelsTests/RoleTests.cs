using Domain.Models;

namespace UnitTests.DomainModelTests;

public class RoleTests
{
    private readonly Role _role;

    public RoleTests()
    {
        _role = GetModel();
    }

    public static Role GetModel()
    {
        return new Role { Id = 1, Name = "Name" };
    }

    [Fact]
    public void IsValidNameEmpty_ShouldFail()
    {
        _role.Name = string.Empty;

        var result = _role.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Role.IsValid: Null or empty Name.", result.Error);
    }

    [Fact]
    public void IsValidNameLenght_ShouldFail()
    {
        _role.Name = new String('A', 51);

        var result = _role.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Role.IsValid: Name has MaxLenght of 50.", result.Error);
    }

    [Fact]
    public void IsValid_ShouldPass()
    {
        var result = _role.IsValid();

        Assert.True(result.Success);
    }
}
