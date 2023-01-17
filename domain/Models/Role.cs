using domain.Logic;

namespace domain.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Result IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return Result.Fail("Role.IsValid: Null or empty name.");
        }

        return Result.Ok();
    }
}
