using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<List<Doctor>> GetAll();
    Task<List<Doctor>> GetAllBySpecialization(int specializationId);
}
