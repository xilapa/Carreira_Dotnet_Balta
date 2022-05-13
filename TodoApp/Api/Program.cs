using Domain.Commands;
using Domain.Repositories;
using Infra.Contexts;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Commands.Contracts;
using TodoApp.Domain.Commands.CreateTodo;
using TodoApp.Domain.Commands.MarkTodoAsDone;
using TodoApp.Domain.Commands.MarkTodoAsUndone;
using TodoApp.Domain.Commands.UpdateTodo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infra
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoDataContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<IUnitOfWork>(_ => _.GetRequiredService<TodoDataContext>());
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Domain
builder.Services.AddScoped<TodoHandler>();
builder.Services.AddScoped<IHandler<CreateTodoCommand>>(s => s.GetRequiredService<TodoHandler>());
builder.Services.AddScoped<IHandler<UpdateTodoCommand>>(s => s.GetRequiredService<TodoHandler>());
builder.Services.AddScoped<IHandler<MarkTodoAsDoneCommand>>(s => s.GetRequiredService<TodoHandler>());
builder.Services.AddScoped<IHandler<MarkTodoAsUndoneCommand>>(s => s.GetRequiredService<TodoHandler>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors(_ => _
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
