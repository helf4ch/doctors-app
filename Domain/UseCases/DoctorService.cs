using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;

namespace Domain.UseCases;

public class DoctorService
{
    private IDoctorRepository _db;

    public DoctorService(IDoctorRepository db)
    {
        _db = db;
    }

    public Result<Doctor> GetDoctor(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Doctor>("DoctorService.GetDoctor: Invalid id.");
        }

        try
        {
            var success = _db.Get(id);

            if (success is null)
            {
                return Result.Fail<Doctor>("DoctorService.GetDoctor: Doctor doesn't exist.");
            }

            return Result.Ok<Doctor>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Doctor>("DoctorService.GetDoctor: " + ex.Message);
        }
    }

    public Result<Doctor> CreateDoctor(Doctor doctor)
    {
        if (doctor.IsValid().IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.CreateDoctor: " + doctor.IsValid().Error);
        }

        try
        {
            var success = _db.Create(doctor);

            return Result.Ok<Doctor>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Doctor>("DoctorService.CreateDoctor: " + ex.Message);
        }
    }

    public Result<Doctor> UpdateDoctor(Doctor doctor)
    {
        if (doctor.IsValid().IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.UpdateDoctor: " + doctor.IsValid().Error);
        }

        try
        {
            var success = _db.Update(doctor);

            return Result.Ok<Doctor>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Doctor>("DoctorService.UpdateDoctor: " + ex.Message);
        }
    }

    public Result<Doctor> DeleteDoctor(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Doctor>("DoctorService.DeleteDoctor: Invalid id.");
        }

        try
        {
            var success = _db.Delete(id);

            return Result.Ok<Doctor>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Doctor>("DoctorService.DeleteDoctor: " + ex.Message);
        }
    }

    public Result<List<Doctor>> GetAllDoctors()
    {
        try
        {
            var success = _db.GetAll();

            return Result.Ok<List<Doctor>>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<Doctor>>("DoctorService.GetAllDoctors: " + ex.Message);
        }
    }

    public Result<List<Doctor>> GetAllDoctorsBySpecialization(int specializationId)
    {
        if (specializationId == 0)
        {
            return Result.Fail<List<Doctor>>(
                "DoctorService.GetAllDoctorsBySpecialization: Invalid specializationId."
            );
        }

        try
        {
            var success = _db.GetAllBySpecialization(specializationId);

            return Result.Ok<List<Doctor>>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<Doctor>>(
                "DoctorService.GetAllDoctorsBySpecialization: " + ex.Message
            );
        }
    }
}
