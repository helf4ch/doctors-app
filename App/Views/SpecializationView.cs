using System.Text.Json.Serialization;
using Domain.Models;

namespace App.Views;

#nullable disable

public class SpecializationView
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public SpecializationView(Specialization role)
    {
        Id = role.Id;
        Name = role.Name;
    }
}
