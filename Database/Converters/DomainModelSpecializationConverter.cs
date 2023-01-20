using Database.Models;
using Domain.Models;

namespace Database.Converters;

public class DomainModelSpecializationConverter
{
    public static SpecializationModel ToModel(Specialization specialization)
    {
        return new SpecializationModel { Id = specialization.Id, Name = specialization.Name };
    }

    public static Specialization ToDomain(SpecializationModel specialization)
    {
        return new Specialization { Id = specialization.Id, Name = specialization.Name };
    }
}
