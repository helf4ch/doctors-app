using Database.Models;
using Domain.Models;

namespace Database.Converters;

public static class DomainModelSpecializationConverter
{
    public static SpecializationModel ToModel(this Specialization specialization)
    {
        return new SpecializationModel { Id = specialization.Id, Name = specialization.Name };
    }

    public static Specialization ToDomain(this SpecializationModel specialization)
    {
        return new Specialization { Id = specialization.Id, Name = specialization.Name };
    }
}
