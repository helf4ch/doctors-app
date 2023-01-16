using domain.Logic;
using domain.Logic.Repositories;
using domain.Models;
using domain.UseCases;

namespace UnitTesting;

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
    public void GetDoctorGetItemError_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.Get(It.IsAny<int>()))
            .Returns(() => Result.Fail<Doctor>("get test"));

        var result = _doctorService.GetDoctor(0);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.GetDoctor: get test", result.Error);
    }

    [Fact]
    public void GetDoctor_ShouldPass()
    {
        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"), 1);

        _doctorRepositoryMock.Setup(r => r.Get(0)).Returns(() => Result.Ok<Doctor>(doctor));

        var result = _doctorService.GetDoctor(0);

        Assert.True(result.Success);
        Assert.Equal(doctor, result.Value);
    }

    [Fact]
    public void CreateDoctorIsValid_ShouldFail()
    {
        Doctor doctor = new Doctor(0, "A", "B", String.Empty, new Specialization(0, "D"), 1);

        var result = _doctorService.CreateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "DoctorService.CreateDoctor: Doctor.IsValid: Null or empty Surname.",
            result.Error
        );
    }

    [Fact]
    public void CreateDoctorCreateError_ShouldFail()
    {
        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"), 1);

        _doctorRepositoryMock
            .Setup(r => r.Create(It.IsAny<Doctor>()))
            .Returns(() => Result.Fail<Doctor>("create test"));

        var result = _doctorService.CreateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.CreateDoctor: create test", result.Error);
    }

    [Fact]
    public void CreateDoctor_ShouldPass()
    {
        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"), 1);

        _doctorRepositoryMock.Setup(r => r.Create(doctor)).Returns(() => Result.Ok<Doctor>(doctor));

        var result = _doctorService.CreateDoctor(doctor);

        Assert.True(result.Success);
        Assert.Equal(doctor, result.Value);
    }

    [Fact]
    public void UpdateDoctorIsValid_ShouldFail()
    {
        Doctor doctor = new Doctor(0, "A", "B", String.Empty, new Specialization(0, "D"), 1);

        var result = _doctorService.UpdateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "DoctorService.UpdateDoctor: Doctor.IsValid: Null or empty Surname.",
            result.Error
        );
    }

    [Fact]
    public void UpdateDoctorUpdateError_ShouldFail()
    {
        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"), 1);

        _doctorRepositoryMock
            .Setup(r => r.Update(It.IsAny<Doctor>()))
            .Returns(() => Result.Fail<Doctor>("update test"));

        var result = _doctorService.UpdateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.UpdateDoctor: update test", result.Error);
    }

    [Fact]
    public void UpdateDoctor_ShouldPass()
    {
        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"), 1);

        _doctorRepositoryMock.Setup(r => r.Update(doctor)).Returns(() => Result.Ok<Doctor>(doctor));

        var result = _doctorService.UpdateDoctor(doctor);

        Assert.True(result.Success);
        Assert.Equal(doctor, result.Value);
    }

    [Fact]
    public void DeleteDoctorDeleteError_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Returns(() => Result.Fail("delete test"));

        var result = _doctorService.DeleteDoctor(1);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.DeleteDoctor: delete test", result.Error);
    }

    [Fact]
    public void DeleteDoctor_ShouldPass()
    {
        _doctorRepositoryMock.Setup(r => r.Delete(1)).Returns(() => Result.Ok());

        var result = _doctorService.DeleteDoctor(1);

        Assert.True(result.Success);
    }

    [Fact]
    public void GetAllDoctorsGetAllError_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.GetAll())
            .Returns(() => Result.Fail<List<Doctor>>("get test"));

        var result = _doctorService.GetAllDoctors();

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.GetAllDoctors: get test", result.Error);
    }

    [Fact]
    public void GetAllDoctors_ShouldPass()
    {
        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"), 1);
        List<Doctor> list = new List<Doctor>() { doctor, doctor, doctor };

        _doctorRepositoryMock.Setup(r => r.GetAll()).Returns(() => Result.Ok<List<Doctor>>(list));

        var result = _doctorService.GetAllDoctors();

        Assert.True(result.Success);
        Assert.Equal(list, result.Value);
    }

    [Fact]
    public void SearchBySpecIsValid_ShouldFail()
    {
        Specialization spec = new Specialization(0, String.Empty);

        var result = _doctorService.Search(spec);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "DoctorService.Search: Specialization.IsValid: Null or empty Name.",
            result.Error
        );
    }

    [Fact]
    public void SearchBySpecSearchError_ShouldFail()
    {
        Specialization spec = new Specialization(0, "D");

        _doctorRepositoryMock
            .Setup(r => r.SearchBySpecialization(spec))
            .Returns(() => Result.Fail<List<Doctor>>("search test"));

        var result = _doctorService.Search(spec);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService.Search: search test", result.Error);
    }

    [Fact]
    public void SearchBySpec_ShouldPass()
    {
        Specialization spec = new Specialization(0, "D");
        Doctor doctor = new Doctor(0, "A", "B", "C", spec, 1);
        List<Doctor> list = new List<Doctor>() { doctor, doctor, doctor };

        _doctorRepositoryMock
            .Setup(r => r.SearchBySpecialization(spec))
            .Returns(() => Result.Ok<List<Doctor>>(list));

        var result = _doctorService.Search(spec);

        Assert.True(result.Success);
        Assert.Equal(list, result.Value);
    }
}
