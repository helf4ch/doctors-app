using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases;
using UnitTests.DomainModelTests;

namespace UnitTests.DomainServicesTests;

public class AppointmentServiceTests
{
    private readonly AppointmentService _appointmentService;
    private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;

    public AppointmentServiceTests()
    {
        _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        _appointmentService = new AppointmentService(_appointmentRepositoryMock.Object);
    }

    [Fact]
    public void GetAppointmentIdInvalid_ShouldFail()
    {
        var result = _appointmentService.GetAppointment(0);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.GetAppointment: Invalid id.", result.Error);
    }

    [Fact]
    public void GetAppointmentDoesntExist_ShouldFail()
    {
        _appointmentRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);

        var result = _appointmentService.GetAppointment(1);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.GetAppointment: Appointment doesn't exist.", result.Error);
    }

    [Fact]
    public void GetAppointmentException_ShouldFail()
    {
        _appointmentRepositoryMock
            .Setup(r => r.Get(It.IsAny<int>()))
            .Throws(() => new Exception("get test"));

        var result = _appointmentService.GetAppointment(1);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.GetAppointment: get test", result.Error);
    }

    [Fact]
    public void GetAppointment_ShouldPass()
    {
        Appointment appointment = AppointmentTests.GetModel();

        _appointmentRepositoryMock.Setup(r => r.Get(1)).Returns(() => appointment);

        var result = _appointmentService.GetAppointment(1);

        Assert.True(result.Success);
        Assert.Equal(appointment, result.Value);
    }

    [Fact]
    public void CreateAppointmentIsValid_ShouldFail()
    {
        Appointment appointment = AppointmentTests.GetModel();
        appointment.Id = 0;
        appointment.UserId = 0;
        Doctor doctor = DoctorTests.GetModel();
        Schedule schedule = ScheduleTests.GetModel();

        var result = _appointmentService.CreateAppointment(appointment, doctor, schedule);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "AppointmentService.SaveAppointment: Appointment.IsValid: UserId is invalid.",
            result.Error
        );
    }

    [Fact]
    public void CreateAppointmentDoctorInvalid_ShouldFail()
    {
        Appointment appointment = AppointmentTests.GetModel();
        appointment.Id = 0;
        Doctor doctor = DoctorTests.GetModel();
        doctor.Id = 0;
        Schedule schedule = ScheduleTests.GetModel();

        var result = _appointmentService.CreateAppointment(appointment, doctor, schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: Doctor is invalid.", result.Error);
    }

    [Fact]
    public void CreateAppointmentScheduleInvalid_ShouldFail()
    {
        Appointment appointment = AppointmentTests.GetModel();
        appointment.Id = 0;
        Doctor doctor = DoctorTests.GetModel();
        Schedule schedule = ScheduleTests.GetModel();
        schedule.DoctorId = 0;

        var result = _appointmentService.CreateAppointment(appointment, doctor, schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: Schedule is invalid.", result.Error);
    }

    [Fact]
    public void CreateAppointmentStartTimeInvalid_ShouldFail()
    {
        Appointment appointment = AppointmentTests.GetModel();
        appointment.Id = 0;
        appointment.StartTime = new TimeOnly(18, 0);
        Doctor doctor = DoctorTests.GetModel();
        Schedule schedule = ScheduleTests.GetModel();

        var result = _appointmentService.CreateAppointment(appointment, doctor, schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: StartTime is invalid.", result.Error);
    }

    [Fact]
    public void CreateAppointmentTimeBusy_ShouldFail()
    {
        Appointment appointment = AppointmentTests.GetModel();
        appointment.Id = 0;
        Doctor doctor = DoctorTests.GetModel();
        Schedule schedule = ScheduleTests.GetModel();

        _appointmentRepositoryMock
            .Setup(
                r =>
                    r.GetAllByTime(
                        It.IsAny<int>(),
                        It.IsAny<DateOnly>(),
                        It.IsAny<TimeOnly>(),
                        It.IsAny<TimeOnly>()
                    )
            )
            .Returns(() => new List<Appointment> { appointment });

        var result = _appointmentService.CreateAppointment(appointment, doctor, schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: Time is busy.", result.Error);
    }

    [Fact]
    public void CreateAppointmentException_ShouldFail()
    {
        Appointment appointment = AppointmentTests.GetModel();
        appointment.Id = 0;
        Doctor doctor = DoctorTests.GetModel();
        Schedule schedule = ScheduleTests.GetModel();

        _appointmentRepositoryMock
            .Setup(
                r =>
                    r.GetAllByTime(
                        appointment.DoctorId,
                        appointment.Date,
                        appointment.StartTime.AddMinutes(1 - doctor.AppointmentTimeMinutes),
                        appointment.StartTime.AddMinutes(doctor.AppointmentTimeMinutes - 1)
                    )
            )
            .Returns(() => new List<Appointment> { });
        _appointmentRepositoryMock
            .Setup(r => r.Create(It.IsAny<Appointment>()))
            .Throws(() => new Exception("create test"));

        var result = _appointmentService.CreateAppointment(appointment, doctor, schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: create test", result.Error);
    }

    [Fact]
    public void CreateAppointment_ShouldPass()
    {
        Appointment appointment = AppointmentTests.GetModel();
        appointment.Id = 0;
        Doctor doctor = DoctorTests.GetModel();
        Schedule schedule = ScheduleTests.GetModel();

        _appointmentRepositoryMock
            .Setup(
                r =>
                    r.GetAllByTime(
                        appointment.DoctorId,
                        appointment.Date,
                        appointment.StartTime.AddMinutes(1 - doctor.AppointmentTimeMinutes),
                        appointment.StartTime.AddMinutes(doctor.AppointmentTimeMinutes - 1)
                    )
            )
            .Returns(() => new List<Appointment> { });
        _appointmentRepositoryMock
            .Setup(r => r.Create(appointment))
            .Returns(() => AppointmentTests.GetModel());

        var result = _appointmentService.CreateAppointment(appointment, doctor, schedule);

        appointment.Id = result.Value!.Id;

        Assert.True(result.Success);
        Assert.Equivalent(appointment, result.Value);
    }

    [Fact]
    public void UpdateAppointmentIsValid_ShouldFail()
    {
        Appointment appointment = AppointmentTests.GetModel();
        appointment.UserId = 0;

        var result = _appointmentService.UpdateAppointment(appointment);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "AppointmentService.UpdateAppointment: Appointment.IsValid: UserId is invalid.",
            result.Error
        );
    }

    [Fact]
    public void UpdateAppointmentException_ShouldFail()
    {
        Appointment appointment = AppointmentTests.GetModel();

        _appointmentRepositoryMock
            .Setup(r => r.Update(It.IsAny<Appointment>()))
            .Throws(() => new Exception("update test"));

        var result = _appointmentService.UpdateAppointment(appointment);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.UpdateAppointment: update test", result.Error);
    }

    [Fact]
    public void UpdateAppointment_ShouldPass()
    {
        Appointment appointment = AppointmentTests.GetModel();

        _appointmentRepositoryMock.Setup(r => r.Update(appointment)).Returns(() => appointment);

        var result = _appointmentService.UpdateAppointment(appointment);

        Assert.True(result.Success);
        Assert.Equal(appointment, result.Value);
    }

    [Fact]
    public void DeleteAppointmentIdInvalid_ShouldFail()
    {
        var result = _appointmentService.DeleteAppointment(0);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.DeleteAppointment: Invalid id.", result.Error);
    }

    [Fact]
    public void DeleteAppointmentException_ShouldFail()
    {
        _appointmentRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Throws(() => new Exception("delete test"));

        var result = _appointmentService.DeleteAppointment(1);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.DeleteAppointment: delete test", result.Error);
    }

    [Fact]
    public void DeleteAppointment_ShouldPass()
    {
        Appointment appointment = AppointmentTests.GetModel();

        _appointmentRepositoryMock.Setup(r => r.Delete(1)).Returns(() => appointment);

        var result = _appointmentService.DeleteAppointment(1);

        Assert.True(result.Success);
        Assert.Equal(appointment, result.Value);
    }

    [Fact]
    public void GetAllAppointmentsBySpecializationIdInvalid_ShouldFail()
    {
        var result = _appointmentService.GetAllAppointmentsBySpecialization(
            0,
            new DateOnly(2001, 9, 11)
        );

        Assert.True(result.IsFailure);
        Assert.Equal(
            "AppointmentService.GetAllAppointments: Invalid specializationId.",
            result.Error
        );
    }

    [Fact]
    public void GetAllAppointmentsBySpecializationException_ShouldFail()
    {
        _appointmentRepositoryMock
            .Setup(r => r.GetAllBySpecialization(It.IsAny<int>(), It.IsAny<DateOnly>()))
            .Throws(() => new Exception("get all test"));

        var result = _appointmentService.GetAllAppointmentsBySpecialization(
            1,
            new DateOnly(2001, 9, 11)
        );

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.GetAllAppointments: get all test", result.Error);
    }

    [Fact]
    public void GetAllAppointmentsBySpecialization_ShouldPass()
    {
        Appointment appointment = AppointmentTests.GetModel();
        List<Appointment> list = new List<Appointment> { appointment };

        _appointmentRepositoryMock
            .Setup(r => r.GetAllBySpecialization(1, appointment.Date))
            .Returns(() => list);

        var result = _appointmentService.GetAllAppointmentsBySpecialization(1, appointment.Date);

        Assert.True(result.Success);
        Assert.Equal(list, result.Value);
    }
}
