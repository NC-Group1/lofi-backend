using Microsoft.Extensions.Diagnostics.HealthChecks;
using lofi_backend.Database;
using Microsoft.EntityFrameworkCore;

namespace lofi_backend.HealthChecks
{
    public class ApiHealthCheck : IHealthCheck
    {
        private readonly LoFiDbContext _loFiDbContext;

        public ApiHealthCheck(LoFiDbContext loFiDbContext)
        {

            _loFiDbContext = loFiDbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                int userCount = await _loFiDbContext.Users.CountAsync(cancellationToken);

                return HealthCheckResult.Healthy($"API and database are working. {userCount} users registered.");
            } 
                catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("API/database health check failed.", ex);

            }

        }
    }
}
