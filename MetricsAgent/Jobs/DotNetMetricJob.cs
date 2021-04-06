using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;


namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IDotNetMetricsRepository>();
            _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            int bytesInHeaps = Convert.ToInt32(_dotnetCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new DotNetMetricModel()
            {
                Time = time,
                Value = bytesInHeaps
            });
            return Task.CompletedTask;
        }
    }
}
