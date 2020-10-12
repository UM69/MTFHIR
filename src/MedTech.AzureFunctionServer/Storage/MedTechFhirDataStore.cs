// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Health.Fhir.Core.Features.Persistence;

namespace MedTech.AzureFunctionServer.Features.Storage
{
    public class MedTechFhirDataStore : IFhirDataStore
    {
        private readonly ILogger<MedTechFhirDataStore> _logger;
        private readonly IMedTechAzureFunctionService _medTechAzureFunctionService;

        public MedTechFhirDataStore(ILogger<MedTechFhirDataStore> logger, IMedTechAzureFunctionService medTechAzureFunctionService)
        {
            _logger = logger;
            _medTechAzureFunctionService = medTechAzureFunctionService;
        }

        public Task<ResourceWrapper> GetAsync(ResourceKey key, CancellationToken cancellationToken)
        {
            return _medTechAzureFunctionService.GetClassificationsByPatientId(key.Id, key.ResourceType);
        }

        public Task HardDeleteAsync(ResourceKey key, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResourceWrapper> UpdateSearchIndexForResourceAsync(ResourceWrapper resourceWrapper, WeakETag weakETag, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateSearchParameterHashBatchAsync(IReadOnlyCollection<ResourceWrapper> resources, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateSearchParameterIndicesBatchAsync(IReadOnlyCollection<ResourceWrapper> resources, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<UpsertOutcome> UpsertAsync(ResourceWrapper resource, WeakETag weakETag, bool allowCreate, bool keepHistory, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
