using Domain.Models;

namespace UnitTests.DomainModelTests;

public class UserTests
{
    private readonly User _user;

    public UserTests()
    {
        _user = GetModel();
    }

    public static User GetModel()
    {
        return new User
        {
            Id = 1,
            PhoneNumber = "PhoneNumber",
            Name = "Name",
            RoleId = 1,
            Password = User.GeneratePassword("Password", "Salt"),
            Salt = "Salt"
        };
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
    public void IsValidPhoneNumberLenght_ShouldFail()
    {
        _user.PhoneNumber = new String('A', 16);

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: PhoneNumber has MaxLenght of 15.", result.Error);
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
    public void IsValidNameLenght_ShouldFail()
    {
        _user.Name = new String('A', 51);

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Name has MaxLenght of 50.", result.Error);
    }

    [Fact]
    public void IsValidSecondnameLenght_ShouldFail()
    {
        _user.Secondname = new String('A', 51);

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Secondname has MaxLenght of 50.", result.Error);
    }

    [Fact]
    public void IsValidSurnameLenght_ShouldFail()
    {
        _user.Surname = new String('A', 51);

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Surname has MaxLenght of 50.", result.Error);
    }

    [Fact]
    public void IsValidRoleInvalid_ShouldFail()
    {
        _user.RoleId = 0;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: RoleId is invalid.", result.Error);
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
    public void IsValidPasswordLenght_ShouldFail()
    {
        _user.Password = new String('A', 65);

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Password has MaxLenght of 64.", result.Error);
    }

    [Fact]
    public void IsValidSaltEmpty_ShouldFail()
    {
        _user.Salt = string.Empty;

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Null or empty Salt.", result.Error);
    }

    [Fact]
    public void IsValidSaltLenght_ShouldFail()
    {
        _user.Salt = new String('A', 33);

        var result = _user.IsValid();

        Assert.True(result.IsFailure);
        Assert.Equal("User.IsValid: Salt has MaxLenght of 32.", result.Error);
    }

    [Fact]
    public void IsValid_ShouldPass()
    {
        var result = _user.IsValid();

        Assert.True(result.Success);
    }
}
