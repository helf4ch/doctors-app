using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases;
using UnitTests.DomainModelTests;

namespace UnitTests.DomainServicesTests;

public class ScheduleServiceTests
{
    private readonly ScheduleService _scheduleService;
    private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;

    public ScheduleServiceTests()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _scheduleService = new ScheduleService(_scheduleRepositoryMock.Object);
    }

    [Fact]
    public void GetScheduleByDateIdInvalid_ShouldFail()
    {
        var result = _scheduleService.GetScheduleByDate(0, new DateOnly(2001, 9, 11));

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.GetSchedule: Invalid doctorId.", result.Error);
    }

    [Fact]
    public void GetScheduleByDateDoesntExist_ShouldFail()
    {
        _scheduleRepositoryMock
            .Setup(r => r.GetByDate(It.IsAny<int>(), It.IsAny<DateOnly>()))
            .Returns(() => null);

        var result = _scheduleService.GetScheduleByDate(1, new DateOnly(2001, 9, 11));

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.GetSchedule: Schedule doesn't exist.", result.Error);
    }

    [Fact]
    public void GetScheduleByDateException_ShouldFail()
    {
        _scheduleRepositoryMock
            .Setup(r => r.GetByDate(It.IsAny<int>(), It.IsAny<DateOnly>()))
            .Throws(() => new Exception("get test"));

        var result = _scheduleService.GetScheduleByDate(1, new DateOnly(2001, 9, 11));

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.GetSchedule: get test", result.Error);
    }

    [Fact]
    public void GetScheduleByDate_ShouldPass()
    {
        Schedule schedule = ScheduleTests.GetModel();
        schedule.Id = 0;

        _scheduleRepositoryMock
            .Setup(r => r.GetByDate(schedule.DoctorId, schedule.Date))
            .Returns(() => ScheduleTests.GetModel());

        var result = _scheduleService.GetScheduleByDate(schedule.DoctorId, schedule.Date);

        schedule.Id = result.Value!.Id;

        Assert.True(result.Success);
        Assert.Equivalent(schedule, result.Value);
    }

    [Fact]
    public void CreateScheduleIsValid_ShouldFail()
    {
        Schedule schedule = ScheduleTests.GetModel();
        schedule.Id = 0;
        schedule.DoctorId = 0;

        var result = _scheduleService.CreateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "ScheduleService.CreateSchedule: Schedule.IsValid: DoctorId is invalid.",
            result.Error
        );
    }

    [Fact]
    public void CreateScheduleDateBusy_ShouldFail()
    {
        Schedule schedule = ScheduleTests.GetModel();
        schedule.Id = 0;

        _scheduleRepositoryMock
            .Setup(r => r.GetByDate(It.IsAny<int>(), It.IsAny<DateOnly>()))
            .Returns(() => ScheduleTests.GetModel());

        var result = _scheduleService.CreateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.CreateSchedule: Date is busy.", result.Error);
    }

    [Fact]
    public void CreateScheduleException_ShouldFail()
    {
        Schedule schedule = ScheduleTests.GetModel();
        schedule.Id = 0;

        _scheduleRepositoryMock
            .Setup(r => r.GetByDate(schedule.DoctorId, schedule.Date))
            .Returns(() => null);
        _scheduleRepositoryMock
            .Setup(r => r.Create(It.IsAny<Schedule>()))
            .Throws(() => new Exception("create test"));

        var result = _scheduleService.CreateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.CreateSchedule: create test", result.Error);
    }

    [Fact]
    public void CreateSchedule_ShouldPass()
    {
        Schedule schedule = ScheduleTests.GetModel();
        schedule.Id = 0;

        _scheduleRepositoryMock
            .Setup(r => r.GetByDate(schedule.DoctorId, schedule.Date))
            .Returns(() => null);
        _scheduleRepositoryMock
            .Setup(r => r.Create(schedule))
            .Returns(() => ScheduleTests.GetModel());

        var result = _scheduleService.CreateSchedule(schedule);

        schedule.Id = result.Value!.Id;

        Assert.True(result.Success);
        Assert.Equivalent(schedule, result.Value);
    }

    [Fact]
    public void UpdateScheduleIsValid_ShouldError()
    {
        Schedule schedule = ScheduleTests.GetModel();
        schedule.DoctorId = 0;

        var result = _scheduleService.UpdateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "ScheduleService.UpdateSchedule: Schedule.IsValid: DoctorId is invalid.",
            result.Error
        );
    }

    [Fact]
    public void UpdateScheduleException_ShouldFail()
    {
        Schedule schedule = ScheduleTests.GetModel();

        _scheduleRepositoryMock
            .Setup(r => r.Update(It.IsAny<Schedule>()))
            .Throws(() => new Exception("update test"));

        var result = _scheduleService.UpdateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.UpdateSchedule: update test", result.Error);
    }

    [Fact]
    public void UpdateSchedule_ShouldPass()
    {
        Schedule schedule = ScheduleTests.GetModel();

        _scheduleRepositoryMock.Setup(r => r.Update(schedule)).Returns(() => schedule);

        var result = _scheduleService.UpdateSchedule(schedule);

        Assert.True(result.Success);
        Assert.Equal(schedule, result.Value);
    }

    [Fact]
    public void DeleteScheduleIdInvalid_ShouldFail()
    {
        var result = _scheduleService.DeleteSchedule(0);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.DeleteSchedule: Invalid id.", result.Error);
    }

    [Fact]
    public void DeleteScheduleException_ShouldFail()
    {
        _scheduleRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Throws(() => new Exception("delete test"));

        var result = _scheduleService.DeleteSchedule(1);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.DeleteSchedule: delete test", result.Error);
    }

    [Fact]
    public void DeleteSchedule_ShouldPass()
    {
        Schedule schedule = ScheduleTests.GetModel();

        _scheduleRepositoryMock.Setup(r => r.Delete(1)).Returns(() => schedule);

        var result = _scheduleService.DeleteSchedule(1);

        Assert.True(result.Success);
        Assert.Equal(schedule, result.Value);
    }
}
