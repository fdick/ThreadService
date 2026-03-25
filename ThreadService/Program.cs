using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using PostService.API.GRPC.Protos;
using ThreadService.Application.Abstractions;
using ThreadService.Application.Services;
using ThreadService.Core.Abstractions;
using ThreadService.Persistance;
using ThreadService.Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc(opt =>
{
    opt.EnableDetailedErrors = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ThreadServiceDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ThreadServiceDbContext)));
});


builder.Services.AddScoped<IThreadsSevice, ThreadsSevice>();
builder.Services.AddScoped<IThreadsRepository, ThreadsRepository>();
builder.Services.AddScoped<ITokenProvider, KeycloakTokenProvider>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", policy =>
    {
        policy.AllowAnyOrigin()  // Разрешить любые источники
              .AllowAnyMethod()   // Разрешить любые HTTP методы (GET, POST, PUT, DELETE и т.д.)
              .AllowAnyHeader();   // Разрешить любые заголовки
    });
});

builder.Services.AddGrpcClient<GRPCPostsController.GRPCPostsControllerClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["Grpc:Host"]!);
});
builder.Services.AddScoped<IPostsServiceGRPC, PostsServiceGRPC>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ThreadServiceDbContext>();

    // Это создаст базу данных и таблицы, если их нет
    //dbContext.Database.EnsureCreated();
    // Или используйте миграции (рекомендуется)
    dbContext.Database.Migrate();
}

app.UseRouting();

app.UseGrpcWeb();

app.UseCors("all");



//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
