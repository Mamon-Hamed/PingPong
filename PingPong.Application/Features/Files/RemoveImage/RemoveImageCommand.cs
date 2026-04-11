using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Files.RemoveImage;

public sealed record RemoveImageCommand(string Url) : ICommand;
