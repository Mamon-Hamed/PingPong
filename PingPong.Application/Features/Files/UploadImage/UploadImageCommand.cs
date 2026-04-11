using Microsoft.AspNetCore.Http;
using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Files.UploadImage;

public sealed record UploadImageCommand(IFormFile File) : ICommand<string>;
