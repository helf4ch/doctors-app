using domain.Models;

namespace domain.Logic.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Result IsDoctorExists(int id);
    List<Doctor> SearchBySpecialization(Specialization spec);
}
