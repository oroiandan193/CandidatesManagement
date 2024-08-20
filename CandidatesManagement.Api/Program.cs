using CandidatesManagement.Api.Middlewares;
using CandidatesManagement.Application;
using CandidatesManagement.Application.Contracts;
using CandidatesManagement.Application.Contracts.Dtos;
using CandidatesManagement.Infrastructure;
using CandidatesManagement.Persistence.Sql;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

// Exception handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services
    .ConfigureInfrastructureServices()
    .ConfigurePersistence(builder.Configuration)
    .ConfigureApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/candidates", async (UpsertJobCandidateDto request, ICandidatesService service, IValidator<UpsertJobCandidateDto> validator) => {

    var validationResult = await validator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    await service.UpsertJobCandidateAsync(request);

    return Results.Ok();
})
.WithName("UpsertCandidateDetails")
.WithOpenApi();

app.Run();
