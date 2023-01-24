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

    public async Task<Result<Appointment>> GetAppointment(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Appointment>("AppointmentService.GetAppointment: Invalid id.");
        }

        try
        {
            var success = await _db.Get(id);

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

    public async Task<Result<Appointment>> CreateAppointment(
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
            var appointments = await _db.GetAllByTime(
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

            var success = await _db.Create(appointment);

            return Result.Ok<Appointment>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Appointment>("AppointmentService.SaveAppointment: " + ex.Message);
        }
    }

    public async Task<Result<Appointment>> UpdateAppointment(Appointment appointment)
    {
        if (appointment.IsValid().IsFailure)
        {
            return Result.Fail<Appointment>(
                "AppointmentService.UpdateAppointment: " + appointment.IsValid().Error
            );
        }

        try
        {
            var success = await _db.Update(appointment);

            return Result.Ok<Appointment>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Appointment>("AppointmentService.UpdateAppointment: " + ex.Message);
        }
    }

    public async Task<Result<Appointment>> DeleteAppointment(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Appointment>("AppointmentService.DeleteAppointment: Invalid id.");
        }

        try
        {
            var success = await _db.Delete(id);

            return Result.Ok(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Appointment>("AppointmentService.DeleteAppointment: " + ex.Message);
        }
    }

    public async Task<Result<List<Appointment>>> GetAllAppointmentsBySpecialization(
        int specializationId,
        DateOnly date
    )
    {
        if (specializationId == 0)
        {
            return Result.Fail<List<Appointment>>(
                "AppointmentService.GetAllAppointments: Invalid specializationId."
            );
        }

        try
        {
            var success = await _db.GetAllBySpecialization(specializationId, date);

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
