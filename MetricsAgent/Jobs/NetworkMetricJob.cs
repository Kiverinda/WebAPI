using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter _networkCounter;

        public NetworkMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<INetworkMetricsRepository>();
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", "Intel[R] Wireless-AC 9462");
        }

        public Task Execute(IJobExecutionContext context)
        {
            int bytesRecived = Convert.ToInt32(_networkCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new NetworkMetricModel()
            {
                Time = time,
                Value = bytesRecived
            });
            return Task.CompletedTask;
        }
    }
}
