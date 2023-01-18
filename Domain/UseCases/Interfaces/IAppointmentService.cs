using domain.Logic;
using domain.Models;

namespace domain.UseCases.Interfaces;

public interface IAppointmentService
{
    Result<Appointment> GetAppointment(int id);
    Result<Appointment> SaveAppointment(
        Appointment appointment,
        IDoctorService doctorService,
        IScheduleService scheduleService
    );
    Result<Appointment> UpdateAppointment(Appointment appointment);
    Result DeleteAppointment(int id);
    Result<List<Appointment>> GetAllAppointments(int specializationId, DateOnly date);
}
