using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases;
using UnitTests.DomainModelTests;

namespace UnitTests.DomainServicesTests;

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
    public void GetUserIdInvalid_ShouldFail()
    {
        var result = _userSerivce.GetUser(0);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.GetUser: Invalid id.", result.Error);
    }

    [Fact]
    public void GetUserDoesntExist_ShouldFail()
    {
        _userRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);

        var result = _userSerivce.GetUser(1);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.GetUser: User doesn't exist.", result.Error);
    }

    [Fact]
    public void GetUserException_ShouldFail()
    {
        _userRepositoryMock
            .Setup(r => r.Get(It.IsAny<int>()))
            .Throws(() => new Exception("get test"));

        var result = _userSerivce.GetUser(1);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.GetUser: get test", result.Error);
    }

    [Fact]
    public void GetUser_ShouldPass()
    {
        User user = UserTests.GetModel();

        _userRepositoryMock.Setup(r => r.Get(1)).Returns(() => user);

        var result = _userSerivce.GetUser(1);

        Assert.True(result.Success);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public void AuthorizationEmptyPhoneNumber_ShouldFail()
    {
        var result = _userSerivce.Authorization(string.Empty, "Password");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: Null or empty PhoneNumber.", result.Error);
    }

    [Fact]
    public void AuthorizationEmptyPassword_ShouldFail()
    {
        var result = _userSerivce.Authorization("PhoneNumber", string.Empty);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: Null or empty Password.", result.Error);
    }

    [Fact]
    public void AuthorizationDoesntExist_ShouldFail()
    {
        _userRepositoryMock.Setup(r => r.GetByPhoneNumber(It.IsAny<string>())).Returns(() => null);

        var result = _userSerivce.Authorization("PhoneNumber", "Password");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: User doesn't exist.", result.Error);
    }

    [Fact]
    public void AuthorizationWrongPassword_ShouldFail()
    {
        User user = UserTests.GetModel();

        _userRepositoryMock.Setup(r => r.GetByPhoneNumber(user.PhoneNumber!)).Returns(() => user);

        var result = _userSerivce.Authorization(user.PhoneNumber!, "Password1");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: Wrong Password.", result.Error);
    }

    [Fact]
    public void AuthorizationException_ShouldFail()
    {
        _userRepositoryMock
            .Setup(r => r.GetByPhoneNumber(It.IsAny<string>()))
            .Throws(() => new Exception("get test"));

        var result = _userSerivce.Authorization("PhoneNumber", "Password");

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Authorization: get test", result.Error);
    }

    [Fact]
    public void Authorization_ShouldPass()
    {
        User user = UserTests.GetModel();

        _userRepositoryMock.Setup(r => r.GetByPhoneNumber(user.PhoneNumber!)).Returns(() => user);

        var result = _userSerivce.Authorization(user.PhoneNumber!, "Password");

        Assert.True(result.Success);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public void RegistrationIsValid_ShouldFail()
    {
        User user = UserTests.GetModel();
        user.Id = 0;
        user.Password = "Password";
        user.RoleId = 0;

        var result = _userSerivce.Registration(user);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Registration: User.IsValid: RoleId is invalid.", result.Error);
    }

    [Fact]
    public void RegistrationPhoneNumberTaken_ShouldFail()
    {
        User user = UserTests.GetModel();
        user.Id = 0;
        user.Password = "Password";

        _userRepositoryMock
            .Setup(r => r.GetByPhoneNumber(It.IsAny<string>()))
            .Returns(() => UserTests.GetModel());

        var result = _userSerivce.Registration(user);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Registration: PhoneNumber is already taken.", result.Error);
    }

    [Fact]
    public void RegistrationException_ShouldFail()
    {
        User user = UserTests.GetModel();
        user.Id = 0;
        user.Password = "Password";

        _userRepositoryMock.Setup(r => r.GetByPhoneNumber(user.PhoneNumber!)).Returns(() => null);
        _userRepositoryMock
            .Setup(r => r.Create(It.IsAny<User>()))
            .Throws(() => new Exception("create test"));

        var result = _userSerivce.Registration(user);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.Registration: create test", result.Error);
    }

    [Fact]
    public void Registration_ShouldPass()
    {
        User user = UserTests.GetModel();
        user.Id = 0;
        user.Password = "Password";

        _userRepositoryMock.Setup(r => r.GetByPhoneNumber(user.PhoneNumber!)).Returns(() => null);
        _userRepositoryMock.Setup(r => r.Create(user)).Returns(() => user);

        var result = _userSerivce.Registration(user);

        Assert.True(result.Success);
        Assert.Equivalent(user, result.Value);
    }

    [Fact]
    public void UpdateUserIsValid_ShouldFail()
    {
        User user = UserTests.GetModel();
        user.RoleId = 0;

        var result = _userSerivce.UpdateUser(user);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.UpdateUser: User.IsValid: RoleId is invalid.", result.Error);
    }

    [Fact]
    public void UpdateUserException_ShouldFail()
    {
        User user = UserTests.GetModel();

        _userRepositoryMock
            .Setup(r => r.Update(It.IsAny<User>()))
            .Throws(() => new Exception("update test"));

        var result = _userSerivce.UpdateUser(user);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.UpdateUser: update test", result.Error);
    }

    [Fact]
    public void UpdateUser_ShouldPass()
    {
        User user = UserTests.GetModel();

        _userRepositoryMock.Setup(r => r.Update(user)).Returns(() => user);

        var result = _userSerivce.UpdateUser(user);

        Assert.True(result.Success);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public void DeleteUserIdInvalid_ShouldFail()
    {
        var result = _userSerivce.DeleteUser(0);

        Assert.True(result.IsFailure);
        Assert.Equal("UserService.DeleteUser: Invalid id.", result.Error);
    }

    [Fact]
    public void DeleteUserException_ShouldFail()
    {
        _userRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Throws(() => new Exception("delete test"));

        var resutl = _userSerivce.DeleteUser(1);

        Assert.True(resutl.IsFailure);
        Assert.Equal("UserService.DeleteUser: delete test", resutl.Error);
    }

    [Fact]
    public void DeleteUser_ShouldPass()
    {
        User user = UserTests.GetModel();

        _userRepositoryMock.Setup(r => r.Delete(user.Id)).Returns(() => user);

        var result = _userSerivce.DeleteUser(user.Id);

        Assert.True(result.Success);
        Assert.Equal(user, result.Value);
    }
}
