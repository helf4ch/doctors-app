using domain.Logic;
using domain.Logic.Repositories;
using domain.Models;
using domain.UseCases;

namespace UnitTesting;

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
    public void GetScheduleGetError_ShouldFail()
    {
        _scheduleRepositoryMock
            .Setup(r => r.GetSchedule(It.IsAny<int>(), It.IsAny<DateOnly>()))
            .Returns(() => Result.Fail<Schedule>("get test"));

        var result = _scheduleService.GetSchedule(1, new DateOnly(2000, 1, 1));

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.GetSchedule: get test", result.Error);
    }

    [Fact]
    public void GetSchedule_ShouldPass()
    {
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(13, 00),
            new TimeOnly(14, 00)
        );

        _scheduleRepositoryMock
            .Setup(r => r.GetSchedule(1, new DateOnly(2000, 1, 1)))
            .Returns(() => Result.Ok<Schedule>(schedule));

        var result = _scheduleService.GetSchedule(1, new DateOnly(2000, 1, 1));

        Assert.True(result.Success);
    }

    [Fact]
    public void CreateScheduleIsValid_ShouldFail()
    {
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(15, 00),
            new TimeOnly(14, 00)
        );

        var result = _scheduleService.CreateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "ScheduleService.CreateSchedule: Schedule.IsValid: EndOfShift can't be less or equal then StartOfShift.",
            result.Error
        );
    }

    [Fact]
    public void CreateScheduleDateBusy_ShouldFail()
    {
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(13, 00),
            new TimeOnly(14, 00)
        );

        _scheduleRepositoryMock
            .Setup(r => r.IsDateFree(It.IsAny<int>(), It.IsAny<DateOnly>()))
            .Returns(() => Result.Fail("date test"));

        var result = _scheduleService.CreateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.CreateSchedule: Date is busy.", result.Error);
    }

    [Fact]
    public void CreateScheduleCreateError_ShouldFail()
    {
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(13, 00),
            new TimeOnly(14, 00)
        );

        _scheduleRepositoryMock
            .Setup(r => r.IsDateFree(1, new DateOnly(2000, 1, 1)))
            .Returns(() => Result.Ok());
        _scheduleRepositoryMock
            .Setup(r => r.Create(It.IsAny<Schedule>()))
            .Returns(() => Result.Fail<Schedule>("create test"));

        var result = _scheduleService.CreateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.CreateSchedule: create test", result.Error);
    }

    [Fact]
    public void CreateSchedule_ShouldPass()
    {
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(13, 00),
            new TimeOnly(14, 00)
        );

        _scheduleRepositoryMock
            .Setup(r => r.IsDateFree(1, new DateOnly(2000, 1, 1)))
            .Returns(() => Result.Ok());
        _scheduleRepositoryMock
            .Setup(r => r.Create(schedule))
            .Returns(() => Result.Ok<Schedule>(schedule));

        var result = _scheduleService.CreateSchedule(schedule);

        Assert.True(result.Success);
        Assert.Equal(schedule, result.Value);
    }

    [Fact]
    public void UpdateScheduleIsValid_ShouldError()
    {
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(15, 00),
            new TimeOnly(14, 00)
        );

        var result = _scheduleService.UpdateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal(
            "ScheduleService.UpdateSchedule: Schedule.IsValid: EndOfShift can't be less or equal then StartOfShift.",
            result.Error
        );
    }

    [Fact]
    public void UpdateScheduleUpdate_ShouldFail()
    {
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(13, 00),
            new TimeOnly(14, 00)
        );

        _scheduleRepositoryMock
            .Setup(r => r.Update(It.IsAny<Schedule>()))
            .Returns(() => Result.Fail<Schedule>("update test"));

        var result = _scheduleService.UpdateSchedule(schedule);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.UpdateSchedule: update test", result.Error);
    }

    [Fact]
    public void UpdateSchedule_ShouldPass()
    {
        Schedule schedule = new Schedule(
            1,
            1,
            new DateOnly(2000, 1, 1),
            new TimeOnly(13, 00),
            new TimeOnly(14, 00)
        );

        _scheduleRepositoryMock
            .Setup(r => r.Update(schedule))
            .Returns(() => Result.Ok<Schedule>(schedule));

        var result = _scheduleService.UpdateSchedule(schedule);

        Assert.True(result.Success);
        Assert.Equal(schedule, result.Value);
    }

    [Fact]
    public void DeleteScheduleDeleteError_ShouldFail()
    {
        _scheduleRepositoryMock
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Returns(() => Result.Fail("delete fail"));

        var result = _scheduleService.DeleteSchedule(1);

        Assert.True(result.IsFailure);
        Assert.Equal("ScheduleService.DeleteSchedule: delete fail", result.Error);
    }

    [Fact]
    public void DeleteSchedule_ShouldPass()
    {
        _scheduleRepositoryMock.Setup(r => r.Delete(1)).Returns(() => Result.Ok());

        var result = _scheduleService.DeleteSchedule(1);

        Assert.True(result.Success);
    }
}
