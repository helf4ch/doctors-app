using domain.Models;

namespace UnitTesting;

public class UserTests
{
    private readonly User _user;

    public UserTests()
    {
        _user = new User(1, "2", "A", "B", "C", Role.Patient, "pass");
    }

    [Fact]
    public void UserPhoneNumberEmpty_ShouldFail()
    {
        _user.PhoneNumber = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Null or empty PhoneNumber.", result.Error);
    }

    [Fact]
    public void UserNameEmpty_ShouldFail()
    {
        _user.Name = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Null or empty Name.", result.Error);
    }

    [Fact]
    public void UserSecondnameEmpty_ShouldFail()
    {
        _user.Secondname = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Null or empty Secondname.", result.Error);
    }

    [Fact]
    public void UserSurnameEmpty_ShouldFail()
    {
        _user.Surname = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Null or empty Surname.", result.Error);
    }

    [Fact]
    public void UserPasswordEmpty_ShouldFail()
    {
        _user.Password = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("Null or empty Password.", result.Error);
    }
}
