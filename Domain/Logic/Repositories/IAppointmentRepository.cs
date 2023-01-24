using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<List<Appointment>> GetAllByTime(
        int doctorId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime
    );
    Task<List<Appointment>> GetAllBySpecialization(int specializationId, DateOnly date);
}
