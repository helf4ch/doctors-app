using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases;
using UnitTests.DomainModelTests;

namespace UnitTests.DomainServicesTests;

public class DoctorServiceTests
{
    private readonly DoctorService _doctorService;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

    public DoctorServiceTests()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _doctorService = new DoctorService(_doctorRepositoryMock.Object);
    }

    [Fact]
    public void GetDoctorIdInvalid_ShouldFail()
    {
        var result = _doctorService.GetDoctor(0);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.GetDoctor: Invalid id.", result.Error);
    }

    [Fact]
    public void GetDoctorDoesntExist_ShouldFail()
    {
        _doctorRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);

        var result = _doctorService.GetDoctor(1);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.GetDoctor: Doctor doesn't exist.", result.Error);
    }

    [Fact]
    public void GetDoctorException_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.Get(It.IsAny<int>()))
            .Throws(() => new Exception("get test"));

        var result = _doctorService.GetDoctor(1);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.GetDoctor: get test", result.Error);
    }

    [Fact]
    public void GetDoctor_ShouldPass()
    {
        Doctor doctor = DoctorTests.GetModel();

        _doctorRepositoryMock.Setup(r => r.Get(doctor.Id)).Returns(() => doctor);

        var result = _doctorService.GetDoctor(doctor.Id);

        Assert.True(result.Success);
        Assert.Equal(doctor, result.Value);
    }

    [Fact]
    public void CreateDoctorIsValid_ShouldFail()
    {
        Doctor doctor = DoctorTests.GetModel();
        doctor.Id = 0;
        doctor.SpecializationId = 0;

        var result = _doctorService.CreateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "DoctorService.CreateDoctor: Doctor.IsValid: SpecializationId is invalid.",
            result.Error
        );
    }

    [Fact]
    public void CreateDoctorException_ShouldFail()
    {
        Doctor doctor = DoctorTests.GetModel();
        doctor.Id = 0;

        _doctorRepositoryMock
            .Setup(r => r.Create(It.IsAny<Doctor>()))
            .Throws(() => new Exception("create test"));

        var result = _doctorService.CreateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.CreateDoctor: create test", result.Error);
    }

    [Fact]
    public void CreateDoctor_ShouldPass()
    {
        Doctor doctor = DoctorTests.GetModel();
        doctor.Id = 0;

        _doctorRepositoryMock.Setup(r => r.Create(doctor)).Returns(() => DoctorTests.GetModel());

        var result = _doctorService.CreateDoctor(doctor);

        doctor.Id = result.Value!.Id;

        Assert.True(result.Success);
        Assert.Equivalent(doctor, result.Value);
    }

    [Fact]
    public void UpdateDoctorIsValid_ShouldFail()
    {
        Doctor doctor = DoctorTests.GetModel();
        doctor.SpecializationId = 0;

        var result = _doctorService.UpdateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "DoctorService.UpdateDoctor: Doctor.IsValid: SpecializationId is invalid.",
            result.Error
        );
    }

    [Fact]
    public void UpdateDoctorException_ShouldFail()
    {
        Doctor doctor = DoctorTests.GetModel();

        _doctorRepositoryMock
            .Setup(r => r.Update(It.IsAny<Doctor>()))
            .Throws(() => new Exception("update test"));

        var result = _doctorService.UpdateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.UpdateDoctor: update test", result.Error);
    }

    [Fact]
    public void UpdateDoctor_ShouldPass()
    {
        Doctor doctor = DoctorTests.GetModel();

        _doctorRepositoryMock.Setup(r => r.Update(doctor)).Returns(() => doctor);

        var result = _doctorService.UpdateDoctor(doctor);

        Assert.True(result.Success);
        Assert.Equal(doctor, result.Value);
    }

    [Fact]
    public void DeleteDoctorIdInvalid_ShouldFail()
    {
        var result = _doctorService.DeleteDoctor(0);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.DeleteDoctor: Invalid id.", result.Error);
    }

    [Fact]
    public void DeleteDoctorException_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Throws(() => new Exception("delete test"));

        var result = _doctorService.DeleteDoctor(1);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.DeleteDoctor: delete test", result.Error);
    }

    [Fact]
    public void DeleteDoctor_ShouldPass()
    {
        Doctor doctor = DoctorTests.GetModel();

        _doctorRepositoryMock.Setup(r => r.Delete(1)).Returns(() => doctor);

        var result = _doctorService.DeleteDoctor(1);

        Assert.True(result.Success);
        Assert.Equal(doctor, result.Value);
    }

    [Fact]
    public void GetAllDoctorsGetAllError_ShouldFail()
    {
        _doctorRepositoryMock.Setup(r => r.GetAll()).Throws(() => new Exception("get test"));

        var result = _doctorService.GetAllDoctors();

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.GetAllDoctors: get test", result.Error);
    }

    [Fact]
    public void GetAllDoctors_ShouldPass()
    {
        Doctor doctor = DoctorTests.GetModel();
        List<Doctor> list = new List<Doctor>() { doctor };

        _doctorRepositoryMock.Setup(r => r.GetAll()).Returns(() => list);

        var result = _doctorService.GetAllDoctors();

        Assert.True(result.Success);
        Assert.Equal(list, result.Value);
    }

    [Fact]
    public void GetAllDoctorsBySpecializationIdInvalid_ShouldFail()
    {
        var result = _doctorService.GetAllDoctorsBySpecialization(0);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "DoctorService.GetAllDoctorsBySpecialization: Invalid specializationId.",
            result.Error
        );
    }

    [Fact]
    public void GetAllDoctorsBySpecializationException_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.GetAllBySpecialization(It.IsAny<int>()))
            .Throws(() => new Exception("search test"));

        var result = _doctorService.GetAllDoctorsBySpecialization(1);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.GetAllDoctorsBySpecialization: search test", result.Error);
    }

    [Fact]
    public void GetAllDoctorsBySpecialization_ShouldPass()
    {
        Doctor doctor = DoctorTests.GetModel();
        List<Doctor> list = new List<Doctor>() { doctor };

        _doctorRepositoryMock.Setup(r => r.GetAllBySpecialization(1)).Returns(() => list);

        var result = _doctorService.GetAllDoctorsBySpecialization(1);

        Assert.True(result.Success);
        Assert.Equal(list, result.Value);
    }
}
