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
    public void RegistrationUserAlreadyExists_ShouldFail()
    {
        _userRepositoryMock.Setup(r => r.IsUserExists(It.IsAny<string>()))
            .Returns(() => Result.Ok());

        User user = new User(0, "1", "A", "B", "C", Role.Patient, "pass");
        var result = _userSerivce.Registration(user);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService: User already exists.", result.Error);
    }

    [Fact]
    public void AuthorizationEmptyPhoneNumber_ShouldFail()
    {
        var result = _userSerivce.Authorization("", "123");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService: Null or empty PhoneNumber.", result.Error);
    }

    [Fact]
    public void AuthorizationEmptyPassword_ShouldFail()
    {
        var result = _userSerivce.Authorization("123", "");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService: Null or empty Password.", result.Error);
    }

    [Fact]
    public void AuthorizationUserDoesntExist_ShouldFail()
    {
        _userRepositoryMock.Setup(r => r.IsUserExists(It.IsAny<string>()))
            .Returns(() => Result.Fail(It.IsAny<string>()));

        var result = _userSerivce.Authorization("123", "123");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService: User doesn't exists.", result.Error);
    }

    [Fact]
    public void AuthorizationWrongPassword_ShouldFail()
    {
        User userFromRepository = new User(0, "1", "A", "B", "C", Role.Patient, "pass1");

        _userRepositoryMock.Setup(r => r.IsUserExists(It.IsAny<string>()))
            .Returns(() => Result.Ok());

        _userRepositoryMock.Setup(r => r.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => Result.Ok<User>(userFromRepository));

        var result = _userSerivce.Authorization("123", "123");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService: Wrong password.", result.Error);
    }
}
