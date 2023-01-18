using domain.Models;

namespace domain.Logic.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Result<List<Doctor>> GetAll();
    Result<List<Doctor>> SearchBySpecialization(int specializationId);
}
