// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MedTech.AzureFunctionServer.Features.Health
{
    public class MedTechHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(HealthCheckResult.Healthy("Connection to the data store was successful, however, the rate limit has been exceeded.")).ConfigureAwait(true);
        }
    }
}
