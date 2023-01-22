using Domain.Logic;

namespace Domain.Models;

public class Role
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public Result IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return Result.Fail("Role.IsValid: Null or empty Name.");
        }

        if (Name.Length > 50)
        {
            return Result.Fail("Role.IsValid: Name has MaxLenght of 50.");
        }

        return Result.Ok();
    }
}
