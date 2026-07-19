namespace PingPong.Domain.Models;

public record Location(
    double Latitude,
    double Longitude,
    string City,
    string Country,
    string Address);
