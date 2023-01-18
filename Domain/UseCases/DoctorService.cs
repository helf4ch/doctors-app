using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;
using Domain.UseCases.Interfaces;

namespace Domain.UseCases;

public class DoctorService : IDoctorService
{
    private IDoctorRepository _db;

    public DoctorService(IDoctorRepository db)
    {
        _db = db;
    }

    public Result<Doctor> GetDoctor(int id)
    {
        var success = _db.Get(id);

        if (success.IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.GetDoctor: " + success.Error);
        }

        return success;
    }

    public Result<Doctor> CreateDoctor(Doctor doctor)
    {
        if (doctor.IsValid().IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.CreateDoctor: " + doctor.IsValid().Error);
        }

        var success = _db.Create(doctor);

        if (success.IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.CreateDoctor: " + success.Error);
        }

        return success;
    }

    public Result<Doctor> UpdateDoctor(Doctor doctor)
    {
        if (doctor.IsValid().IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.UpdateDoctor: " + doctor.IsValid().Error);
        }

        var success = _db.Update(doctor);

        if (success.IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.UpdateDoctor: " + success.Error);
        }

        return success;
    }

    public Result DeleteDoctor(int id)
    {
        var success = _db.Delete(id);

        if (success.IsFailure)
        {
            return Result.Fail("DoctorService.DeleteDoctor: " + success.Error);
        }

        return success;
    }

    public Result<List<Doctor>> GetAllDoctors()
    {
        var success = _db.GetAll();

        if (success.IsFailure)
        {
            return Result.Fail<List<Doctor>>("DoctorService.GetAllDoctors: " + success.Error);
        }

        return success;
    }

    public Result<List<Doctor>> Search(int specializationId)
    {
        var success = _db.SearchBySpecialization(specializationId);

        if (success.IsFailure)
        {
            return Result.Fail<List<Doctor>>("DoctorService.Search: " + success.Error);
        }

        return success;
    }
}
