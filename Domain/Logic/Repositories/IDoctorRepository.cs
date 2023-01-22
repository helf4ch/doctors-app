using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    List<Doctor> GetAll();
    List<Doctor> GetAllBySpecialization(int specializationId);
}
