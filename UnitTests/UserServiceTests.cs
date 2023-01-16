using domain.Logic;
using domain.Logic.Repositories;
using domain.Models;
using domain.UseCases;

namespace UnitTesting;

public class UserServiceTests
{
    private readonly UserService _userSerivce;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userSerivce = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void RegistrationIsValid_ShouldFail()
    {
        User user = new User(0, "1", "A", "B", String.Empty, 1, "pass");

        var result = _userSerivce.Registration(user);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "UserService.Registration: User.IsValid: Null or empty Surname.",
            result.Error
        );
    }

    [Fact]
    public void RegistrationPhoneNumberTaken_ShouldFail()
    {
        User user = new User(0, "1", "A", "B", "C", 1, "pass");

        _userRepositoryMock
            .Setup(r => r.IsPhoneTaken(It.IsAny<string>()))
            .Returns(() => Result.Ok());

        var result = _userSerivce.Registration(user);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Registration: PhoneNumber is already taken.", result.Error);
    }

    [Fact]
    public void RegistrationCreateError_ShouldFail()
    {
        User user = new User(0, "1", "A", "B", "C", 1, "pass");

        _userRepositoryMock
            .Setup(r => r.IsPhoneTaken("1"))
            .Returns(() => Result.Fail("phone test"));
        _userRepositoryMock
            .Setup(r => r.Create(It.IsAny<User>()))
            .Returns(() => Result.Fail<User>("create test"));

        var result = _userSerivce.Registration(user);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Registration: create test", result.Error);
    }

    [Fact]
    public void Registration_ShouldPass()
    {
        User user = new User(0, "1", "A", "B", "C", 1, "pass");

        _userRepositoryMock
            .Setup(r => r.IsPhoneTaken("1"))
            .Returns(() => Result.Fail("phone test"));
        _userRepositoryMock.Setup(r => r.Create(user)).Returns(() => Result.Ok<User>(user));

        var result = _userSerivce.Registration(user);

        Assert.True(result.Success);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public void AuthorizationEmptyPhoneNumber_ShouldFail()
    {
        var result = _userSerivce.Authorization("", "123");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: Null or empty PhoneNumber.", result.Error);
    }

    [Fact]
    public void AuthorizationEmptyPassword_ShouldFail()
    {
        var result = _userSerivce.Authorization("123", "");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: Null or empty Password.", result.Error);
    }

    [Fact]
    public void AuthorizationUserDoesntExist_ShouldFail()
    {
        _userRepositoryMock
            .Setup(r => r.IsPhoneTaken(It.IsAny<string>()))
            .Returns(() => Result.Fail("exists test"));

        var result = _userSerivce.Authorization("123", "123");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: User doesn't exists.", result.Error);
    }

    [Fact]
    public void AuthorizationGetByLoginError_ShouldFail()
    {
        _userRepositoryMock.Setup(r => r.IsPhoneTaken("1")).Returns(() => Result.Ok());
        _userRepositoryMock
            .Setup(r => r.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => Result.Fail<User>("get test"));

        var result = _userSerivce.Authorization("1", "123");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: get test", result.Error);
    }

    [Fact]
    public void AuthorizationWrongPassword_ShouldFail()
    {
        User user = new User(0, "1", "A", "B", "C", 1, "pass");

        _userRepositoryMock.Setup(r => r.IsPhoneTaken("1")).Returns(() => Result.Ok());
        _userRepositoryMock.Setup(r => r.GetUserByLogin("1")).Returns(() => Result.Ok<User>(user));

        var result = _userSerivce.Authorization("1", "123");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: Wrong password.", result.Error);
    }

    [Fact]
    public void Authorization_ShouldPass()
    {
        User user = new User(0, "1", "A", "B", "C", 1, "pass");

        _userRepositoryMock.Setup(r => r.IsPhoneTaken("1")).Returns(() => Result.Ok());
        _userRepositoryMock.Setup(r => r.GetUserByLogin("1")).Returns(() => Result.Ok<User>(user));

        var result = _userSerivce.Authorization("1", "pass");

        Assert.True(result.Success);
        Assert.Equal(user, result.Value);
    }
}
