using System.Text.Json.Serialization;
using Domain.Models;

namespace App.Views;

#nullable disable

public class AppointmentView
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("doctorId")]
    public int DoctorId { get; set; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("startTime")]
    public TimeOnly StartTime { get; set; }

    public AppointmentView(Appointment appointment)
    {
        Id = appointment.Id;
        UserId = appointment.UserId;
        DoctorId = appointment.DoctorId;
        Date = appointment.Date;
        StartTime = appointment.StartTime;
    }
}
