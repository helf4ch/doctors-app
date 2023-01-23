using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases;
using UnitTests.DomainModelTests;

namespace UnitTests.DomainServicesTests;

public class RoleServiceTests
{
    private readonly RoleService _roleService;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;

    public RoleServiceTests()
    {
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _roleService = new RoleService(_roleRepositoryMock.Object);
    }

    [Fact]
    public void GetRoleIdInvalid_ShouldFail()
    {
        var result = _roleService.GetRole(0);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.GetRole: Invalid id.", result.Error);
    }

    [Fact]
    public void GetRoleDoesntExist_ShouldFail()
    {
        _roleRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);

        var result = _roleService.GetRole(1);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.GetRole: Role doesn't exist.", result.Error);
    }

    [Fact]
    public void GetRoleException_ShouldFail()
    {
        _roleRepositoryMock
            .Setup(r => r.Get(It.IsAny<int>()))
            .Throws(() => new Exception("get test"));

        var result = _roleService.GetRole(1);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.GetRole: get test", result.Error);
    }

    [Fact]
    public void GetRole_ShouldPass()
    {
        Role role = RoleTests.GetModel();

        _roleRepositoryMock.Setup(r => r.Get(role.Id)).Returns(() => role);

        var result = _roleService.GetRole(role.Id);

        Assert.True(result.Success);
        Assert.Equal(role, result.Value);
    }

    [Fact]
    public void CreateRoleIsValid_ShouldFail()
    {
        Role role = RoleTests.GetModel();
        role.Id = 0;
        role.Name = string.Empty;

        var result = _roleService.CreateRole(role);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.CreateRole: Role.IsValid: Null or empty Name.", result.Error);
    }

    [Fact]
    public void CreateRoleException_ShouldFail()
    {
        Role role = RoleTests.GetModel();
        role.Id = 0;

        _roleRepositoryMock
            .Setup(r => r.Create(It.IsAny<Role>()))
            .Throws(() => new Exception("create test"));

        var result = _roleService.CreateRole(role);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.CreateRole: create test", result.Error);
    }

    [Fact]
    public void CreateRole_ShouldPass()
    {
        Role role = RoleTests.GetModel();
        role.Id = 0;

        _roleRepositoryMock.Setup(r => r.Create(role)).Returns(() => RoleTests.GetModel());

        var result = _roleService.CreateRole(role);

        role.Id = result.Value!.Id;

        Assert.True(result.Success);
        Assert.Equivalent(role, result.Value);
    }

    [Fact]
    public void UpdateRoleIsValid_ShouldFail()
    {
        Role role = RoleTests.GetModel();
        role.Id = 0;
        role.Name = string.Empty;

        var result = _roleService.UpdateRole(role);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.UpdateRole: Role.IsValid: Null or empty Name.", result.Error);
    }

    [Fact]
    public void UpdateRoleException_ShouldFail()
    {
        Role role = RoleTests.GetModel();
        role.Id = 0;

        _roleRepositoryMock
            .Setup(r => r.Update(It.IsAny<Role>()))
            .Throws(() => new Exception("update test"));

        var result = _roleService.UpdateRole(role);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.UpdateRole: update test", result.Error);
    }

    [Fact]
    public void UpdateRole_ShouldPass()
    {
        Role role = RoleTests.GetModel();
        role.Id = 0;

        _roleRepositoryMock.Setup(r => r.Update(role)).Returns(() => RoleTests.GetModel());

        var result = _roleService.UpdateRole(role);

        role.Id = result.Value!.Id;

        Assert.True(result.Success);
        Assert.Equivalent(role, result.Value);
    }

    [Fact]
    public void DeleteRoleIdInvalid_ShouldFail()
    {
        var result = _roleService.DeleteRole(0);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.DeleteRole: Invalid id.", result.Error);
    }

    [Fact]
    public void DeleteRoleException_ShouldFail()
    {
        _roleRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Throws(() => new Exception("delete test"));

        var result = _roleService.DeleteRole(1);

        Assert.True(result.IsFailure);
        Assert.Equal("RoleService.DeleteRole: delete test", result.Error);
    }

    [Fact]
    public void DeleteRole_ShouldPass()
    {
        Role role = RoleTests.GetModel();

        _roleRepositoryMock.Setup(r => r.Delete(role.Id)).Returns(() => role);

        var result = _roleService.DeleteRole(role.Id);

        Assert.True(result.Success);
        Assert.Equal(role, result.Value);
    }
}
