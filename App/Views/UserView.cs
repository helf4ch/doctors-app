using System.Text.Json.Serialization;
using Domain.Models;

namespace App.Views;

#nullable disable

public class UserView
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("secondname")]
    public string Secondname { get; set; }

    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    [JsonPropertyName("roleId")]
    public int RoleId { get; set; }

    public UserView(User user)
    {
        Id = user.Id;
        PhoneNumber = user.PhoneNumber;
        Name = user.Name;
        Secondname = user.Name;
        Surname = user.Surname;
        RoleId = user.RoleId;
    }
}
