using lofi_backend.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace lofi_backend.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly LoFiDbContext _loFiDbContext;

        public DatabaseHealthCheck(LoFiDbContext loFiDbContext)
        {
            _loFiDbContext = loFiDbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            

            try
            {
                bool canConnect = await _loFiDbContext.Database.CanConnectAsync(cancellationToken);

                if (canConnect)
                {
                    return HealthCheckResult.Healthy("Database connection is working.");

                }
                    return HealthCheckResult.Unhealthy("Database connection failed.");
            }
            catch (Exception ex)
            {

                return HealthCheckResult.Unhealthy("Database connection failed.", ex);

            }
            
        }
    }
}
