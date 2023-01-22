using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases;
using UnitTests.DomainModelTests;

namespace UnitTests.DomainServicesTests;

public class SpecializationServiceTests
{
    private readonly SpecializationService _specializationService;
    private readonly Mock<ISpecializationRepository> _specializationRepositoryMock;

    public SpecializationServiceTests()
    {
        _specializationRepositoryMock = new Mock<ISpecializationRepository>();
        _specializationService = new SpecializationService(_specializationRepositoryMock.Object);
    }

    [Fact]
    public void GetSpecializationIdInvalid_ShouldFail()
    {
        var result = _specializationService.GetSpecialization(0);

        Assert.True(result.IsFailure);
        Assert.Equal("SpecializationService.GetSpecialization: Invalid id.", result.Error);
    }

    [Fact]
    public void GetSpecializationDoesntExist_ShouldFail()
    {
        _specializationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);

        var result = _specializationService.GetSpecialization(1);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "SpecializationService.GetSpecialization: Specialization doesn't exist.",
            result.Error
        );
    }

    [Fact]
    public void GetSpecializationException_ShouldFail()
    {
        _specializationRepositoryMock
            .Setup(r => r.Get(It.IsAny<int>()))
            .Throws(() => new Exception("get test"));

        var result = _specializationService.GetSpecialization(1);

        Assert.True(result.IsFailure);
        Assert.Equal("SpecializationService.GetSpecialization: get test", result.Error);
    }

    [Fact]
    public void GetSpecialization_ShouldPass()
    {
        Specialization specialization = SpecializationTests.GetModel();

        _specializationRepositoryMock
            .Setup(r => r.Get(specialization.Id))
            .Returns(() => specialization);

        var result = _specializationService.GetSpecialization(specialization.Id);

        Assert.True(result.Success);
        Assert.Equal(specialization, result.Value);
    }

    [Fact]
    public void CreateSpecializationIsValid_ShouldFail()
    {
        Specialization specialization = SpecializationTests.GetModel();
        specialization.Id = 0;
        specialization.Name = string.Empty;

        var result = _specializationService.CreateSpecialization(specialization);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "SpecializationService.CreateSpecialization: Specialization.IsValid: Null or empty Name.",
            result.Error
        );
    }

    [Fact]
    public void CreateSpecializationException_ShouldFail()
    {
        Specialization specialization = SpecializationTests.GetModel();
        specialization.Id = 0;

        _specializationRepositoryMock
            .Setup(r => r.Create(It.IsAny<Specialization>()))
            .Throws(() => new Exception("create test"));

        var result = _specializationService.CreateSpecialization(specialization);

        Assert.True(result.IsFailure);
        Assert.Equal("SpecializationService.CreateSpecialization: create test", result.Error);
    }

    [Fact]
    public void CreateSpecialization_ShouldPass()
    {
        Specialization specialization = SpecializationTests.GetModel();
        specialization.Id = 0;

        _specializationRepositoryMock
            .Setup(r => r.Create(specialization))
            .Returns(() => SpecializationTests.GetModel());

        var result = _specializationService.CreateSpecialization(specialization);

        specialization.Id = result.Value!.Id;

        Assert.True(result.Success);
        Assert.Equivalent(specialization, result.Value);
    }

    [Fact]
    public void UpdateSpecializationIsValid_ShouldFail()
    {
        Specialization specialization = SpecializationTests.GetModel();
        specialization.Id = 0;
        specialization.Name = string.Empty;

        var result = _specializationService.UpdateSpecialization(specialization);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "SpecializationService.UpdateSpecialization: Specialization.IsValid: Null or empty Name.",
            result.Error
        );
    }

    [Fact]
    public void UpdateSpecializationException_ShouldFail()
    {
        Specialization specialization = SpecializationTests.GetModel();
        specialization.Id = 0;

        _specializationRepositoryMock
            .Setup(r => r.Update(It.IsAny<Specialization>()))
            .Throws(() => new Exception("update test"));

        var result = _specializationService.UpdateSpecialization(specialization);

        Assert.True(result.IsFailure);
        Assert.Equal("SpecializationService.UpdateSpecialization: update test", result.Error);
    }

    [Fact]
    public void UpdateSpecialization_ShouldPass()
    {
        Specialization specialization = SpecializationTests.GetModel();
        specialization.Id = 0;

        _specializationRepositoryMock
            .Setup(r => r.Update(specialization))
            .Returns(() => SpecializationTests.GetModel());

        var result = _specializationService.UpdateSpecialization(specialization);

        specialization.Id = result.Value!.Id;

        Assert.True(result.Success);
        Assert.Equivalent(specialization, result.Value);
    }

    [Fact]
    public void DeleteSpecializationIdInvalid_ShouldFail()
    {
        var result = _specializationService.DeleteSpecialization(0);

        Assert.True(result.IsFailure);
        Assert.Equal("SpecializationService.DeleteSpecialization: Invalid id.", result.Error);
    }

    [Fact]
    public void DeleteSpecializationException_ShouldFail()
    {
        _specializationRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Throws(() => new Exception("delete test"));

        var result = _specializationService.DeleteSpecialization(1);

        Assert.True(result.IsFailure);
        Assert.Equal("SpecializationService.DeleteSpecialization: delete test", result.Error);
    }

    [Fact]
    public void DeleteSpecialization_ShouldPass()
    {
        Specialization specialization = SpecializationTests.GetModel();

        _specializationRepositoryMock
            .Setup(r => r.Delete(specialization.Id))
            .Returns(() => specialization);

        var result = _specializationService.DeleteSpecialization(specialization.Id);

        Assert.True(result.Success);
        Assert.Equal(specialization, result.Value);
    }
}
