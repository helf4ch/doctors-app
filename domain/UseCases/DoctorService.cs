using domain.Logic;
using domain.Logic.Repositories;
using domain.Models;

namespace domain.UseCases;

public class DoctorService
{
    private IDoctorRepository _db;

    public DoctorService(IDoctorRepository db)
    {
        _db = db;
    }

    public Result<Doctor> GetDoctor(int id)
    {
        if (_db.IsExists(id).IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.GetDoctor: Doctor doesn't exists.");
        }

        var success = _db.GetItem(id);

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

        if (_db.IsExists(doctor.Id).Success)
        {
            return Result.Fail<Doctor>("DoctorService.CreateDoctor: Doctor already exists.");
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

        if (_db.IsExists(doctor.Id).IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.UpdateDoctor: Doctor doesn't exists.");
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
        if (_db.IsExists(id).IsFailure)
        {
            return Result.Fail("DoctorService.DeleteDoctor: Doctor doesn't exists.");
        }

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

    public Result<List<Doctor>> Search(Specialization spec)
    {
        if (spec.IsValid().IsFailure)
        {
            return Result.Fail<List<Doctor>>("DoctorService.Search: " + spec.IsValid().Error);
        }

        var success = _db.SearchBySpecialization(spec);

        if (success.IsFailure)
        {
            return Result.Fail<List<Doctor>>("DoctorService.Search: " + success.Error);
        }

        return success;
    }
}
