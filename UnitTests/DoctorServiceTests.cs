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
    public void CreateDoctorDoctorAlreadyExists_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.IsDoctorExists(It.IsAny<int>()))
            .Returns(() => Result.Ok());

        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"));
        var result = _doctorService.CreateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService: Doctor already exists.", result.Error);
    }

    [Fact]
    public void CreateDoctorCreateError_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.IsDoctorExists(It.IsAny<int>()))
            .Returns(() => Result.Fail(It.IsAny<string>()));

        _doctorRepositoryMock
            .Setup(r => r.Create(It.IsAny<Doctor>()))
            .Returns(() => Result.Fail<Doctor>("Test Error"));

        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"));
        var result = _doctorService.CreateDoctor(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService: Test Error", result.Error);
    }

    [Fact]
    public void CreateDoctor_ShouldPass()
    {
        _doctorRepositoryMock
            .Setup(r => r.IsDoctorExists(It.IsAny<int>()))
            .Returns(() => Result.Fail(It.IsAny<string>()));

        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"));
        _doctorRepositoryMock
            .Setup(r => r.Create(doctor))
            .Returns(() => Result.Ok<Doctor>(doctor));

        var result = _doctorService.CreateDoctor(doctor);

        Assert.True(result.Success);
        Assert.Equal(doctor, result.Value);
    }

    [Fact]
    public void DeleteDoctorDoesntExists_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.IsDoctorExists(It.IsAny<int>()))
            .Returns(() => Result.Fail(It.IsAny<string>()));

        var result = _doctorService.DeleteDoctor(1);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService: Doctor doesn't exists.", result.Error);
    }

    [Fact]
    public void DeleteDoctor_ShouldPass()
    {
        _doctorRepositoryMock
            .Setup(r => r.IsDoctorExists(It.IsAny<int>()))
            .Returns(() => Result.Ok());

        _doctorRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Returns(() => Result.Ok());

        var result = _doctorService.DeleteDoctor(1);

        Assert.True(result.Success);
    }

    [Fact]
    public void GetAllDoctors_ShouldPass()
    {
        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"));
        List<Doctor> list = new List<Doctor>() { doctor, doctor, doctor };

        _doctorRepositoryMock.Setup(r => r.GetAll()).Returns(() => list);

        var result = _doctorService.GetAllDoctors();

        Assert.Equal(result, list);
    }

    [Fact]
    public void SearchByIdDoesntExists_ShouldFail()
    {
        _doctorRepositoryMock
            .Setup(r => r.IsDoctorExists(It.IsAny<int>()))
            .Returns(() => Result.Fail(It.IsAny<string>()));

        var result = _doctorService.Search(1);

        Assert.True(result.IsFailure);
        Assert.Equal("DoctorService: Doctor doesn't exists.", result.Error);
    }

    [Fact]
    public void SearchById_ShouldPass()
    {
        _doctorRepositoryMock
            .Setup(r => r.IsDoctorExists(It.IsAny<int>()))
            .Returns(() => Result.Ok());

        Doctor doctor = new Doctor(0, "A", "B", "C", new Specialization(0, "D"));
        _doctorRepositoryMock
            .Setup(r => r.GetItem(0))
            .Returns(() => Result.Ok<Doctor>(doctor));

        var result = _doctorService.Search(0);

        Assert.True(result.Success);
        Assert.Equal(doctor, result.Value);
    }

    [Fact]
    public void SearchBySpec_ShouldPass()
    {
        Specialization spec = new Specialization(0, "D");

        Doctor doctor = new Doctor(0, "A", "B", "C", spec);
        List<Doctor> list = new List<Doctor>() { doctor, doctor, doctor };

        _doctorRepositoryMock
            .Setup(r => r.SearchBySpecialization(spec))
            .Returns(() => list);

        var result = _doctorService.Search(spec);

        Assert.Equal(list, result);
    }
}
