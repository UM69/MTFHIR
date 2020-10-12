// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Health.Fhir.Core.Features.Persistence;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IMedTechAzureFunctionService
    {
        Task<ResourceWrapper> GetClassificationsByPatientId(string patientId, string resourceType);
    }
}
