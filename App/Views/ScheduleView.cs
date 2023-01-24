using System.Text.Json.Serialization;
using Domain.Models;

namespace App.Views;

#nullable disable

public class ScheduleView
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("doctorId")]
    public int DoctorId { get; set; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("startOfShift")]
    public TimeOnly StartOfShift { get; set; }

    [JsonPropertyName("endOfShift")]
    public TimeOnly EndOfShift { get; set; }

    public ScheduleView(Schedule schedule)
    {
        Id = schedule.Id;
        DoctorId = schedule.DoctorId;
        Date = schedule.Date;
        StartOfShift = schedule.StartOfShift;
        EndOfShift = schedule.EndOfShift;
    }
}
