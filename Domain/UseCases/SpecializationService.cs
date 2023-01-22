using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;

namespace Domain.UseCases;

public class SpecializationService
{
    private readonly ISpecializationRepository _db;

    public SpecializationService(ISpecializationRepository db)
    {
        _db = db;
    }

    public Result<Specialization> GetSpecialization(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Specialization>(
                "SpecializationService.GetSpecialization: Invalid id."
            );
        }
        try
        {
            var success = _db.Get(id);

            if (success is null)
            {
                return Result.Fail<Specialization>(
                    "SpecializationService.GetSpecialization: Specialization doesn't exist."
                );
            }

            return Result.Ok<Specialization>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Specialization>(
                "SpecializationService.GetSpecialization: " + ex.Message
            );
        }
    }

    public Result<Specialization> CreateSpecialization(Specialization specialization)
    {
        if (specialization.IsValid().IsFailure)
        {
            return Result.Fail<Specialization>(
                "SpecializationService.CreateSpecialization: " + specialization.IsValid().Error
            );
        }

        try
        {
            var success = _db.Create(specialization);

            return Result.Ok<Specialization>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Specialization>(
                "SpecializationService.CreateSpecialization: " + ex.Message
            );
        }
    }

    public Result<Specialization> UpdateSpecialization(Specialization specialization)
    {
        if (specialization.IsValid().IsFailure)
        {
            return Result.Fail<Specialization>(
                "SpecializationService.UpdateSpecialization: " + specialization.IsValid().Error
            );
        }

        try
        {
            var success = _db.Update(specialization);

            return Result.Ok<Specialization>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Specialization>(
                "SpecializationService.UpdateSpecialization: " + ex.Message
            );
        }
    }

    public Result<Specialization> DeleteSpecialization(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Specialization>(
                "SpecializationService.DeleteSpecialization: Invalid id."
            );
        }

        try
        {
            var success = _db.Delete(id);

            return Result.Ok<Specialization>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Specialization>(
                "SpecializationService.DeleteSpecialization: " + ex.Message
            );
        }
    }
}
