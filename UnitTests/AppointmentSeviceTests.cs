using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases;
using Domain.UseCases.Interfaces;

namespace UnitTesting;

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
    public void GetAppointmentGetError_ShouldFail()
    {
        _appointmentRepositoryMock
            .Setup(r => r.Get(It.IsAny<int>()))
            .Returns(() => Result.Fail<Appointment>("get test"));

        var result = _appointmentService.GetAppointment(1);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.GetAppointment: get test", result.Error);
    }

    [Fact]
    public void GetAppointment_ShouldPass()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(1, 1, 1),
            new TimeOnly(1, 1)
        );

        _appointmentRepositoryMock
            .Setup(r => r.Get(1))
            .Returns(() => Result.Ok<Appointment>(appointment));

        var result = _appointmentService.GetAppointment(1);

        Assert.True(result.Success);
        Assert.Equal(appointment, result.Value);
    }

    [Fact]
    public void SaveAppointmentGetDoctorError_ShouldFail()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(1, 1, 1),
            new TimeOnly(1, 1)
        );

        var doctorService = new Mock<IDoctorService>();
        var scheduleService = new Mock<IScheduleService>();

        doctorService
            .Setup(r => r.GetDoctor(It.IsAny<int>()))
            .Returns(() => Result.Fail<Doctor>("get doctor test"));

        var result = _appointmentService.SaveAppointment(
            appointment,
            doctorService.Object,
            scheduleService.Object
        );

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: get doctor test", result.Error);
    }

    [Fact]
    public void SaveAppointmentGetSchedule_ShouldFail()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(1, 1, 1),
            new TimeOnly(1, 1)
        );
        Doctor doctor = new Doctor(1, "A", "B", "C", 1, 1);

        var doctorService = new Mock<IDoctorService>();
        var scheduleService = new Mock<IScheduleService>();

        doctorService.Setup(r => r.GetDoctor(1)).Returns(() => Result.Ok<Doctor>(doctor));
        scheduleService
            .Setup(r => r.GetSchedule(It.IsAny<int>(), It.IsAny<DateOnly>()))
            .Returns(() => Result.Fail<Schedule>("get schedule test"));

        var result = _appointmentService.SaveAppointment(
            appointment,
            doctorService.Object,
            scheduleService.Object
        );

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: get schedule test", result.Error);
    }

    [Fact]
    public void SaveAppointmentStartTimeInvalid_ShouldFail()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(16, 30)
        );
        Doctor doctor = new Doctor(1, "A", "B", "C", 1, 60);
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(8, 00),
            new TimeOnly(17, 00)
        );

        var doctorService = new Mock<IDoctorService>();
        var scheduleService = new Mock<IScheduleService>();

        doctorService.Setup(r => r.GetDoctor(1)).Returns(() => Result.Ok<Doctor>(doctor));
        scheduleService
            .Setup(r => r.GetSchedule(1, new DateOnly(2000, 1, 1)))
            .Returns(() => Result.Ok<Schedule>(schedule));

        var result = _appointmentService.SaveAppointment(
            appointment,
            doctorService.Object,
            scheduleService.Object
        );

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: StartTime is invalid.", result.Error);
    }

    [Fact]
    public void SaveAppointmentTimeBusy_ShouldFail()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(15, 30)
        );
        Doctor doctor = new Doctor(1, "A", "B", "C", 1, 60);
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(8, 00),
            new TimeOnly(17, 00)
        );

        var doctorService = new Mock<IDoctorService>();
        var scheduleService = new Mock<IScheduleService>();

        doctorService.Setup(r => r.GetDoctor(1)).Returns(() => Result.Ok<Doctor>(doctor));
        scheduleService
            .Setup(r => r.GetSchedule(1, new DateOnly(2000, 1, 1)))
            .Returns(() => Result.Ok<Schedule>(schedule));
        _appointmentRepositoryMock
            .Setup(r => r.IsTimeFree(It.IsAny<int>(), It.IsAny<DateOnly>(), It.IsAny<TimeOnly>()))
            .Returns(() => Result.Fail("time busy test"));

        var result = _appointmentService.SaveAppointment(
            appointment,
            doctorService.Object,
            scheduleService.Object
        );

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: Time is busy.", result.Error);
    }

    [Fact]
    public void SaveAppointmentCreateError_ShouldFail()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(15, 30)
        );
        Doctor doctor = new Doctor(1, "A", "B", "C", 1, 60);
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(8, 00),
            new TimeOnly(17, 00)
        );

        var doctorService = new Mock<IDoctorService>();
        var scheduleService = new Mock<IScheduleService>();

        doctorService.Setup(r => r.GetDoctor(1)).Returns(() => Result.Ok<Doctor>(doctor));
        scheduleService
            .Setup(r => r.GetSchedule(1, new DateOnly(2000, 1, 1)))
            .Returns(() => Result.Ok<Schedule>(schedule));
        _appointmentRepositoryMock
            .Setup(r => r.IsTimeFree(1, new DateOnly(2000, 1, 1), new TimeOnly(15, 30)))
            .Returns(() => Result.Ok());
        _appointmentRepositoryMock
            .Setup(r => r.Create(It.IsAny<Appointment>()))
            .Returns(() => Result.Fail<Appointment>("create test"));

        var result = _appointmentService.SaveAppointment(
            appointment,
            doctorService.Object,
            scheduleService.Object
        );

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.SaveAppointment: create test", result.Error);
    }

    [Fact]
    public void SaveAppointment_ShouldPass()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(15, 30)
        );
        Doctor doctor = new Doctor(1, "A", "B", "C", 1, 60);
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(8, 00),
            new TimeOnly(17, 00)
        );

        var doctorService = new Mock<IDoctorService>();
        var scheduleService = new Mock<IScheduleService>();

        doctorService.Setup(r => r.GetDoctor(1)).Returns(() => Result.Ok<Doctor>(doctor));
        scheduleService
            .Setup(r => r.GetSchedule(1, new DateOnly(2000, 1, 1)))
            .Returns(() => Result.Ok<Schedule>(schedule));
        _appointmentRepositoryMock
            .Setup(r => r.IsTimeFree(1, new DateOnly(2000, 1, 1), new TimeOnly(15, 30)))
            .Returns(() => Result.Ok());
        _appointmentRepositoryMock
            .Setup(r => r.Create(appointment))
            .Returns(() => Result.Ok<Appointment>(appointment));

        var result = _appointmentService.SaveAppointment(
            appointment,
            doctorService.Object,
            scheduleService.Object
        );

        Assert.True(result.Success);
        Assert.Equal(appointment, result.Value);
    }

    [Fact]
    public void UpdateAppointmentUpdateError_ShouldFail()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(15, 30)
        );

        _appointmentRepositoryMock
            .Setup(r => r.Update(It.IsAny<Appointment>()))
            .Returns(() => Result.Fail<Appointment>("update test"));

        var result = _appointmentService.UpdateAppointment(appointment);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.UpdateAppointment: update test", result.Error);
    }

    [Fact]
    public void UpdateAppointment_ShouldPass()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(15, 30)
        );

        _appointmentRepositoryMock
            .Setup(r => r.Update(appointment))
            .Returns(() => Result.Ok<Appointment>(appointment));

        var result = _appointmentService.UpdateAppointment(appointment);

        Assert.True(result.Success);
        Assert.Equal(appointment, result.Value);
    }

    [Fact]
    public void DeleteAppointmentDeleteError_ShouldFail()
    {
        _appointmentRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Returns(() => Result.Fail<Appointment>("delete test"));

        var result = _appointmentService.DeleteAppointment(1);

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.DeleteAppointment: delete test", result.Error);
    }

    [Fact]
    public void DeleteAppointment_ShouldPass()
    {
        _appointmentRepositoryMock.Setup(r => r.Delete(1)).Returns(() => Result.Ok());

        var result = _appointmentService.DeleteAppointment(1);

        Assert.True(result.Success);
    }

    [Fact]
    public void GetAllAppointmentsGetAllError_ShouldFail()
    {
        _appointmentRepositoryMock
            .Setup(r => r.GetAll(It.IsAny<int>(), It.IsAny<DateOnly>()))
            .Returns(() => Result.Fail<List<Appointment>>("get all test"));

        var result = _appointmentService.GetAllAppointments(1, new DateOnly(2000, 1, 1));

        Assert.True(result.IsFailure);
        Assert.Equal("AppointmentService.GetAllAppointments: get all test", result.Error);
    }

    [Fact]
    public void GetAllAppointments_ShouldPass()
    {
        Appointment appointment = new Appointment(
            1,
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(15, 30)
        );
        List<Appointment> list = new List<Appointment> { appointment, appointment, appointment };

        _appointmentRepositoryMock
            .Setup(r => r.GetAll(1, new DateOnly(2000, 1, 1)))
            .Returns(() => Result.Ok<List<Appointment>>(list));

        var result = _appointmentService.GetAllAppointments(1, new DateOnly(2000, 1, 1));

        Assert.True(result.Success);
        Assert.Equal(list, result.Value);
    }
}
