using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases.Interfaces;

namespace Domain.UseCases;

public class AppointmentService : IAppointmentService
{
    private IAppointmentRepository _db;

    public AppointmentService(IAppointmentRepository db)
    {
        _db = db;
    }

    public Result<Appointment> GetAppointment(int id)
    {
        var success = _db.Get(id);

        if (success.IsFailure)
        {
            return Result.Fail<Appointment>("AppointmentService.GetAppointment: " + success.Error);
        }

        return success;
    }

    public Result<Appointment> SaveAppointment(
        Appointment appointment,
        IDoctorService doctorService,
        IScheduleService scheduleService
    )
    {
        var doctor = doctorService.GetDoctor(appointment.DoctorId);

        if (doctor.IsFailure)
        {
            return Result.Fail<Appointment>("AppointmentService.SaveAppointment: " + doctor.Error);
        }

        if (doctor.Value == null)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.SaveAppointment: Doctor null reference."
            );
        }

        var schedule = scheduleService.GetSchedule(appointment.DoctorId, appointment.Date);

        if (schedule.IsFailure)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.SaveAppointment: " + schedule.Error
            );
        }

        if (schedule.Value == null)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.SaveAppointment: Schedule null reference."
            );
        }

        if (
            appointment.StartTime < schedule.Value.StartOfShift
            || appointment.StartTime.AddMinutes(doctor.Value.AppointmentTimeMinutes)
                > schedule.Value.EndOfShift
        )
        {
            return Result.Fail<Appointment>(
                "AppointmentService.SaveAppointment: StartTime is invalid."
            );
        }

        if (_db.IsTimeFree(appointment.DoctorId, appointment.Date, appointment.StartTime).IsFailure)
        {
            return Result.Fail<Appointment>("AppointmentService.SaveAppointment: Time is busy.");
        }

        var success = _db.Create(appointment);

        if (success.IsFailure)
        {
            return Result.Fail<Appointment>("AppointmentService.SaveAppointment: " + success.Error);
        }

        return success;
    }

    public Result<Appointment> UpdateAppointment(Appointment appointment)
    {
        var success = _db.Update(appointment);

        if (success.IsFailure)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.UpdateAppointment: " + success.Error
            );
        }

        return success;
    }

    public Result DeleteAppointment(int id)
    {
        var success = _db.Delete(id);

        if (success.IsFailure)
        {
            return Result.Fail("AppointmentService.DeleteAppointment: " + success.Error);
        }

        return success;
    }

    public Result<List<Appointment>> GetAllAppointments(int specializationId, DateOnly date)
    {
        var success = _db.GetAll(specializationId, date);

        if (success.IsFailure)
        {
            return Result.Fail<List<Appointment>>(
                "AppointmentService.GetAllAppointments: " + success.Error
            );
        }

        return success;
    }
}
