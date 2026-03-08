using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using PostService.API.GRPC.Protos;
using ThreadService.Application.Services;
using ThreadService.Core.Abstractions;
using ThreadService.DataAccess;
using ThreadService.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();

builder.Services.AddDbContext<ThreadServiceDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ThreadServiceDbContext)));
});


builder.Services.AddScoped<IThreadsSevice, ThreadsSevice>();
builder.Services.AddScoped<IThreadsRepository, ThreadsRepository>();

var channel = GrpcChannel.ForAddress(builder.Configuration["Grpc:Host"]);
builder.Services.AddScoped(provider =>
    new GRPCPostsController.GRPCPostsControllerClient(channel)
);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseGrpcWeb();

app.UseCors();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
