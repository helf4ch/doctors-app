namespace domain;

public class User
{
    public uint Id { get; }
    public string? PhoneNumber { get; set; }
    public string? Name { get; set; }
    public Role Role { get; } = Role.Patient;
}
