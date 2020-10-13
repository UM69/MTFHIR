// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using EnsureThat;
using MedTech.AzureFunctionServer.Configs;
using Microsoft.Health.Fhir.Core.Features.Persistence;
using Microsoft.Health.Fhir.Core.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MedTechAzureFunctionService : IMedTechAzureFunctionService
    {
        private readonly MedTechDataStoreConfiguration _configuration;
        private static readonly HttpClient _client = new HttpClient();

        public MedTechAzureFunctionService(MedTechDataStoreConfiguration configuration)
        {
            EnsureArg.IsNotNull(configuration, nameof(configuration));
            _configuration = configuration;
        }

        public async Task<ResourceWrapper> GetClassificationsByPatientId(string patientId, string resourceType)
        {
            var medTechAzureFunctionUrl = string.Format(_configuration.Host + _configuration.AzureFunctionPath, patientId);
            HttpResponseMessage httpResponse = await _client.GetAsync(new Uri(medTechAzureFunctionUrl)).ConfigureAwait(true);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return await Task.FromResult(new ResourceWrapper(
                    patientId,
                    "1",
                    resourceType,
                    new RawResource(null, FhirResourceFormat.Json, false),
                    new ResourceRequest("POST"),
                    new DateTimeOffset(DateTime.Now),
                    false,
                    null,
                    null,
                    null)).ConfigureAwait(true);
            }

            var condition = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(true);
            return await Task.FromResult(new ResourceWrapper(
                patientId,
                "1",
                resourceType,
                new RawResource(condition, FhirResourceFormat.Json, false),
                new ResourceRequest("POST"),
                new DateTimeOffset(DateTime.Now),
                false,
                null,
                null,
                null)).ConfigureAwait(true);
        }
    }
}
