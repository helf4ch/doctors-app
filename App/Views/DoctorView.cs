using System.Text.Json.Serialization;
using Domain.Models;

namespace App.Views;

#nullable disable

public class DoctorView
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("secondname")]
    public string Secondname { get; set; }

    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    [JsonPropertyName("specializationId")]
    public int SpecializationId { get; set; }

    [JsonPropertyName("appointmentTimeMinutes")]
    public int AppointmentTimeMinutes { get; set; }

    public DoctorView(Doctor doctor)
    {
        Id = doctor.Id;
        Name = doctor.Name;
        Secondname = doctor.Secondname;
        Surname = doctor.Surname;
        SpecializationId = doctor.SpecializationId;
        AppointmentTimeMinutes = doctor.AppointmentTimeMinutes;
    }
}
