using System.Text.Json.Serialization;

namespace PingPong.Application.Common;

public sealed class ApiResponse
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; init; }

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }

    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyDictionary<string, string[]>? Errors { get; init; }

    public static ApiResponse Ok(string? message = null) => new()
    {
        IsSuccess = true,
        StatusCode = 200,
        Message = message ?? "Request completed successfully."
    };

    public static ApiResponse Created(string? message = null) => new()
    {
        IsSuccess = true,
        StatusCode = 201,
        Message = message ?? "Resource created successfully."
    };

    public static ApiResponse Fail(int statusCode, string message, IReadOnlyDictionary<string, string[]>? errors = null) => new()
    {
        IsSuccess = false,
        StatusCode = statusCode,
        Message = message,
        Errors = errors
    };
}

public sealed class ApiResponse<TData>
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; init; }

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }

    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData? Data { get; init; }

    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyDictionary<string, string[]>? Errors { get; init; }

    public static ApiResponse<TData> Ok(TData data, string? message = null) => new()
    {
        IsSuccess = true,
        StatusCode = 200,
        Message = message ?? "Request completed successfully.",
        Data = data
    };

    public static ApiResponse<TData> Created(TData data, string? message = null) => new()
    {
        IsSuccess = true,
        StatusCode = 201,
        Message = message ?? "Resource created successfully.",
        Data = data
    };

    public static ApiResponse<TData> Fail(int statusCode, string message, IReadOnlyDictionary<string, string[]>? errors = null) => new()
    {
        IsSuccess = false,
        StatusCode = statusCode,
        Message = message,
        Errors = errors
    };
}
