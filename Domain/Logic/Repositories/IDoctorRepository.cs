using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Result<List<Doctor>> GetAll();
    Result<List<Doctor>> SearchBySpecialization(int specializationId);
}
