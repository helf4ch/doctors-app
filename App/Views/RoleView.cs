using System.Text.Json.Serialization;
using Domain.Models;

namespace App.Views;

#nullable disable

public class RoleView
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public RoleView(Role role)
    {
        Id = role.Id;
        Name = role.Name;
    }
}
