using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    List<Appointment> GetAllByTime(
        int doctorId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime
    );
    List<Appointment> GetAllBySpecialization(int specializationId, DateOnly date);
}
