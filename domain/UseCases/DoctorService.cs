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

    public Result<Doctor> CreateDoctor(Doctor doctor)
    {
        if (doctor.IsValid().IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService: " + doctor.IsValid().Error);
        }

        if (_db.IsDoctorExists(doctor.Id).Success)
        {
            return Result.Fail<Doctor>("DoctorService: Doctor already exists.");
        }

        var success = _db.Create(doctor);

        if (success.IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService: " + success.Error);
        }

        return success;
    }

    public Result DeleteDoctor(int id)
    {
        if (_db.IsDoctorExists(id).IsFailure)
        {
            return Result.Fail("DoctorService: Doctor doesn't exists.");
        }

        return _db.Delete(id);
    }

    public List<Doctor> GetAllDoctors()
    {
        return _db.GetAll();
    }

    public Result<Doctor> Search(int id)
    {
        if (_db.IsDoctorExists(id).IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService: Doctor doesn't exists.");
        }

        return _db.GetItem(id);
    }

    public List<Doctor> Search(Specialization spec)
    {
        return _db.SearchBySpecialization(spec);
    }
}
