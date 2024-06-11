using gRPC.Microservicio.Autor.Context;
using gRPC.Microservicio.Autor.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
var stringConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<ContextStrategy>();
builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var mongoClient = new MongoClient(stringConnection);
    return mongoClient.GetDatabase("LibraryFiles");
});



builder.Services.AddGrpc();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

var app = builder.Build();

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseCors();

// Configure the HTTP request pipeline.
app.MapGrpcService<AutorImageService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
