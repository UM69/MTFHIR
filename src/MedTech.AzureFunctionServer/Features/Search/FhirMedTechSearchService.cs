// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using MedTech.AzureFunctionServer.Features.Storage;
using Microsoft.Health.Fhir.Core.Features.Search;

namespace MedTech.AzureFunctionServer.Features.Search
{
    public class FhirMedTechSearchService : SearchService
    {
        private readonly MedTechFhirDataStore _fhirDataStore;

        public FhirMedTechSearchService(
            MedTechFhirDataStore fhirDataStore,
            ISearchOptionsFactory searchOptionsFactory)
            : base(searchOptionsFactory, fhirDataStore)
        {
            EnsureArg.IsNotNull(fhirDataStore, nameof(fhirDataStore));

            _fhirDataStore = fhirDataStore;
        }

        protected override Task<SearchResult> SearchForReindexInternalAsync(SearchOptions searchOptions, string searchParameterHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<SearchResult> SearchHistoryInternalAsync(SearchOptions searchOptions, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<SearchResult> SearchInternalAsync(SearchOptions searchOptions, CancellationToken cancellationToken)
        {
            return Task.FromResult(new SearchResult(0, null));
        }
    }
}
