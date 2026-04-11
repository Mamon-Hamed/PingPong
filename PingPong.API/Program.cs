using PingPong.API.Install;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InstallServices(builder.Configuration);

var app = builder.Build();
app.BuildServices();
app.Run();
