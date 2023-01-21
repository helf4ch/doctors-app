using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    List<Appointment> GetAll(int specializationId, DateOnly date);
    List<Appointment> GetAll(int doctorId, DateOnly date, TimeOnly startTime, TimeOnly endTime);
}
