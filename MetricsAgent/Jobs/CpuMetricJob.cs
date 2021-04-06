﻿using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;


namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly ICpuMetricsRepository _repository;
        private readonly PerformanceCounter _cpuCounter;

        public CpuMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<ICpuMetricsRepository>();
            _cpuCounter = new PerformanceCounter ("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new CpuMetricModel()
            {
                Time = time,
                Value = cpuUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}