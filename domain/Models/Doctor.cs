namespace domain.Models;

public class Doctor
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Secondname { get; set; }
    public string? Surname { get; set; } 
    public Specialization? Specialization { get; set; } 
}
