using CandidatesManagement.Application.Contracts;
using CandidatesManagement.Application.Contracts.Dtos;
using CandidatesManagement.Application.Services;
using CandidatesManagement.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CandidatesManagement.UnitTests")] 

namespace CandidatesManagement.Application;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICandidatesService, CandidatesService>();
        services.AddScoped<IValidator<UpsertJobCandidateDto>, JobCandidateValidator>();
            
        return services;
    }
}

