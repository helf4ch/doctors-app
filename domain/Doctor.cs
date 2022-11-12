namespace domain;

public class Doctor
{
    public uint Id { get; }
    public string? Name { get; set; }
    public Specialization? Specialization { get; set; }
}
