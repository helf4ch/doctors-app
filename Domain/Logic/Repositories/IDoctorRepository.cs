using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    List<Doctor> GetAll();
    List<Doctor> GetAll(int specializationId);
}
