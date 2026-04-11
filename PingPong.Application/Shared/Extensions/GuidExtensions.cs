namespace PingPong.Application.Shared.Extensions;

public static class GuidExtensions
{
    public static string ToHex(this Guid guid)
    {
        return guid.ToString("N");
    }
}
