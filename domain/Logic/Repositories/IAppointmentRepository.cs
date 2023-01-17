using domain.Models;

namespace domain.Logic.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    public Result IsTimeFree(int doctorId, DateOnly date, TimeOnly time);
    public Result<List<Appointment>> GetAll(int specializationId, DateOnly date);
}
