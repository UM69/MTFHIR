// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using EnsureThat;
using MedTech.AzureFunctionServer.Configs;
using MedTech.AzureFunctionServer.Features.Health;
using MedTech.AzureFunctionServer.Features.Search;
using MedTech.AzureFunctionServer.Features.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Health.Extensions.DependencyInjection;
using Microsoft.Health.Fhir.Core.Registration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FhirServerBuilderMedtechAzureFunctionRegistrationExtensions
    {
        public static IFhirServerBuilder AddMedtechAzureFunction(this IFhirServerBuilder fhirServerBuilder, Action<MedTechDataStoreConfiguration> configureAction = null)
        {
            EnsureArg.IsNotNull(fhirServerBuilder, nameof(fhirServerBuilder));

            IServiceCollection services = fhirServerBuilder.Services;

            services.Add<MedTechFhirDataStore>().Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add(provider =>
            {
                var config = new MedTechDataStoreConfiguration();
                provider.GetService<IConfiguration>().GetSection("MedTech").Bind(config);
                configureAction?.Invoke(config);
                return config;
            })
            .Singleton()
            .AsSelf();

            return fhirServerBuilder
                .AddMedTechHealthCheck()
                .AddMedTechSearch()
                .AddMedTechServices();
        }

        private static IFhirServerBuilder AddMedTechHealthCheck(this IFhirServerBuilder fhirServerBuilder)
        {
            fhirServerBuilder.Services.AddHealthChecks()
                .AddCheck<MedTechHealthCheck>(name: nameof(MedTechHealthCheck));

            return fhirServerBuilder;
        }

        private static IFhirServerBuilder AddMedTechSearch(this IFhirServerBuilder fhirServerBuilder)
        {
            fhirServerBuilder.Services.Add<FhirMedTechSearchService>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            return fhirServerBuilder;
        }

        private static IFhirServerBuilder AddMedTechServices(this IFhirServerBuilder fhirServerBuilder)
        {
            fhirServerBuilder.Services.Add<MedTechAzureFunctionService>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            return fhirServerBuilder;
        }
    }
}
