using domain.Logic;
using domain.Models;

namespace domain.UseCases.Interfaces;

public interface IDoctorService
{
    Result<Doctor> GetDoctor(int id);
    Result<Doctor> CreateDoctor(Doctor doctor);
    Result<Doctor> UpdateDoctor(Doctor doctor);
    Result DeleteDoctor(int id);
    Result<List<Doctor>> GetAllDoctors();
    Result<List<Doctor>> Search(int specializationId);
}
