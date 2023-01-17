using domain.Models;

namespace UnitTesting;

public class UserTests
{
    private readonly User _user;

    public UserTests()
    {
        _user = new User(1, "2", "A", "B", "C", 1, "pass");
    }

    [Fact]
    public void IsValidPhoneNumberEmpty_ShouldFail()
    {
        _user.PhoneNumber = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Null or empty PhoneNumber.", result.Error);
    }

    [Fact]
    public void IsValidNameEmpty_ShouldFail()
    {
        _user.Name = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Null or empty Name.", result.Error);
    }

    [Fact]
    public void IsValidSecondnameEmpty_ShouldFail()
    {
        _user.Secondname = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Null or empty Secondname.", result.Error);
    }

    [Fact]
    public void IsValidSurnameEmpty_ShouldFail()
    {
        _user.Surname = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Null or empty Surname.", result.Error);
    }

    [Fact]
    public void IsValidPasswordEmpty_ShouldFail()
    {
        _user.Password = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Null or empty Password.", result.Error);
    }

    [Fact]
    public void IsValid_ShouldPass()
    {
        var result = _user.IsValid();

        Assert.True(result.Success);
    }
}
