using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mixvel.App.Searching.Factory;

internal class SearchServiceStateBackgroundService : BackgroundService
{
    private readonly ILogger<SearchServiceStateBackgroundService> _logger;
    private readonly IServiceScopeFactory _factory;
    private int _executionCount;

    public SearchServiceStateBackgroundService(
        ILogger<SearchServiceStateBackgroundService> logger,
        IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await using AsyncServiceScope scope = _factory.CreateAsyncScope();
                var availabilityService = scope.ServiceProvider.GetRequiredService<ISearchServiceFactory>();
                await availabilityService.RefreshStateAsync(stoppingToken);
                _executionCount++;
                _logger.LogInformation(
                    "Executed {BackgroundService} (count: {ExecutionCount})",
                    nameof(SearchServiceStateBackgroundService),
                    _executionCount);

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to execute {BackgroundService}",
                    nameof(SearchServiceStateBackgroundService));
            }
        }
    }
}