using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;

namespace Domain.UseCases;

public class AppointmentService
{
    private IAppointmentRepository _db;

    public AppointmentService(IAppointmentRepository db)
    {
        _db = db;
    }

    public Result<Appointment> GetAppointment(int id)
    {
        try
        {
            var success = _db.Get(id);

            if (success is null)
            {
                return Result.Fail<Appointment>(
                    "AppointmentService.GetAppointment: Appointment doesn't exist."
                );
            }

            return Result.Ok<Appointment>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Appointment>("AppointmentService.GetAppointment: " + ex.Message);
        }
    }

    public Result<Appointment> CreateAppointment(
        Appointment appointment,
        Doctor doctor,
        Schedule schedule
    )
    {
        if (appointment.IsValid().IsFailure)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.SaveAppointment: " + appointment.IsValid().Error
            );
        }

        if (doctor.Id != appointment.DoctorId)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.SaveAppointment: Doctor is invalid."
            );
        }

        if (schedule.DoctorId != doctor.Id)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.SaveAppointment: Schedule is invalid."
            );
        }

        if (
            appointment.StartTime < schedule.StartOfShift
            || appointment.StartTime.AddMinutes(doctor.AppointmentTimeMinutes) > schedule.EndOfShift
        )
        {
            return Result.Fail<Appointment>(
                "AppointmentService.SaveAppointment: StartTime is invalid."
            );
        }

        try
        {
            var appointments = _db.GetAll(
                appointment.DoctorId,
                appointment.Date,
                appointment.StartTime.AddMinutes(1 - doctor.AppointmentTimeMinutes),
                appointment.StartTime.AddMinutes(doctor.AppointmentTimeMinutes - 1)
            );

            if (appointments.Any())
            {
                return Result.Fail<Appointment>(
                    "AppointmentService.SaveAppointment: Time is busy."
                );
            }

            var success = _db.Create(appointment);

            return Result.Ok<Appointment>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Appointment>("AppointmentService.SaveAppointment: " + ex.Message);
        }
    }

    public Result<Appointment> UpdateAppointment(Appointment appointment)
    {
        if (appointment.IsValid().IsFailure)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.UpdateAppointment: " + appointment.IsValid().Error
            );
        }

        try
        {
            var success = _db.Update(appointment);

            return Result.Ok<Appointment>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Appointment>("AppointmentService.UpdateAppointment: " + ex.Message);
        }
    }

    public Result<Appointment> DeleteAppointment(int id)
    {
        try
        {
            var success = _db.Delete(id);

            return Result.Ok(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Appointment>("AppointmentService.DeleteAppointment: " + ex.Message);
        }
    }

    public Result<List<Appointment>> GetAllAppointments(int specializationId, DateOnly date)
    {
        try
        {
            var success = _db.GetAll(specializationId, date);

            return Result.Ok<List<Appointment>>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<Appointment>>(
                "AppointmentService.GetAllAppointments: " + ex.Message
            );
        }
    }
}
