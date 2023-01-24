using System.Text.Json.Serialization;
using Domain.Models;

namespace App.Views;

#nullable disable

public class AuthView
{
    [JsonPropertyName("user")]
    public UserView User { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; }

    public AuthView(User user, string token)
    {
        User = new UserView(user);
        Token = token;
    }
}
