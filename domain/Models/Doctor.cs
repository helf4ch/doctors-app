using domain.Logic;

namespace domain.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Secondname { get; set; }
    public string Surname { get; set; }
    public Specialization Specialization { get; set; }

    public Doctor(
        int id,
        string name,
        string secondname,
        string surname,
        Specialization specialization
    )
    {
        Id = id;
        Name = name;
        Secondname = secondname;
        Surname = surname;
        Specialization = specialization;
    }

    public Result IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return Result.Fail("Doctor: Null or empty Name.");
        }

        if (string.IsNullOrEmpty(Secondname))
        {
            return Result.Fail("Doctor: Null or empty Secondname.");
        }

        if (string.IsNullOrEmpty(Surname))
        {
            return Result.Fail("Doctor: Null or empty Surname.");
        }

        if (Specialization.IsValid().IsFailure)
        {
            return Result.Fail("Doctor: " + Specialization.IsValid().Error);
        }

        return Result.Ok();
    }
}
