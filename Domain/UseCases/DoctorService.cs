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

    public async Task<Result<Doctor>> GetDoctor(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Doctor>("DoctorService.GetDoctor: Invalid id.");
        }

        try
        {
            var success = await _db.Get(id);

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

    public async Task<Result<Doctor>> CreateDoctor(Doctor doctor)
    {
        if (doctor.IsValid().IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.CreateDoctor: " + doctor.IsValid().Error);
        }

        try
        {
            var success = await _db.Create(doctor);

            return Result.Ok<Doctor>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Doctor>("DoctorService.CreateDoctor: " + ex.Message);
        }
    }

    public async Task<Result<Doctor>> UpdateDoctor(Doctor doctor)
    {
        if (doctor.IsValid().IsFailure)
        {
            return Result.Fail<Doctor>("DoctorService.UpdateDoctor: " + doctor.IsValid().Error);
        }

        try
        {
            var success = await _db.Update(doctor);

            return Result.Ok<Doctor>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Doctor>("DoctorService.UpdateDoctor: " + ex.Message);
        }
    }

    public async Task<Result<Doctor>> DeleteDoctor(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Doctor>("DoctorService.DeleteDoctor: Invalid id.");
        }

        try
        {
            var success = await _db.Delete(id);

            return Result.Ok<Doctor>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Doctor>("DoctorService.DeleteDoctor: " + ex.Message);
        }
    }

    public async Task<Result<List<Doctor>>> GetAllDoctors()
    {
        try
        {
            var success = await _db.GetAll();

            return Result.Ok<List<Doctor>>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<Doctor>>("DoctorService.GetAllDoctors: " + ex.Message);
        }
    }

    public async Task<Result<List<Doctor>>> GetAllDoctorsBySpecialization(int specializationId)
    {
        if (specializationId == 0)
        {
            return Result.Fail<List<Doctor>>(
                "DoctorService.GetAllDoctorsBySpecialization: Invalid specializationId."
            );
        }

        try
        {
            var success = await _db.GetAllBySpecialization(specializationId);

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
