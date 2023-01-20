using Domain.Logic;

namespace Domain.Models;

public class Specialization
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public Result IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return Result.Fail("Specialization.IsValid: Null or empty Name.");
        }

        return Result.Ok();
    }
}