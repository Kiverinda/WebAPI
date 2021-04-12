using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MetricsManager.Client;
using MetricsManager.Client.ApiRequests;
using MetricsManager.Client.ApiResponses;


namespace MetricsManager.Jobs
{
    public class MetricsFromAgents : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IMetricsManagerClient _client;

        public MetricsFromAgents(IServiceProvider provider, IMetricsManagerClient client)
        {
            _provider = provider;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            DateTimeOffset toTime = DateTimeOffset.UtcNow;
            DateTimeOffset fromTime = _provider.GetService<ICpuMetricsRepository>()?.LastLine()?.Time ?? DateTimeOffset.FromUnixTimeSeconds(0);
            IList<AgentModel> agents = _provider.GetService<IAgentsRepository>()?.GetAll();
            
            if (agents.Count != 0)
            {
                foreach (var agent in agents)
                {
                    if (agent.Status == true)
                    {
                        AllCpuMetricsApiResponse allCpuMetrics = _client.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
                        {
                            FromTime = fromTime,
                            ToTime = toTime,
                            Addres = agent.IpAddress
                        });

                        if (allCpuMetrics.Metrics != null)
                        {
                            foreach (var metric in allCpuMetrics.Metrics)
                            {
                                _provider.GetService<ICpuMetricsRepository>()?.Create(new CpuMetricModel
                                {
                                    IdAgent = agent.Id,
                                    Time = metric.Time,
                                    Value = metric.Value
                                });
                            }
                        }

                        AllDotNetMetricsApiResponse allDotNetMetrics = _client.GetAllDotNetMetrics(new GetAllDotNetMetricsApiRequest
                        {
                            FromTime = fromTime,
                            ToTime = toTime,
                            Addres = agent.IpAddress
                        });

                        if (allDotNetMetrics.Metrics != null)
                        {
                            foreach (var metric in allDotNetMetrics.Metrics)
                            {
                                _provider.GetService<IDotNetMetricsRepository>()?.Create(new DotNetMetricModel
                                {
                                    IdAgent = agent.Id,
                                    Time = metric.Time,
                                    Value = metric.Value
                                });
                            }
                        }

                        AllNetworkMetricsApiResponse allNetworkMetrics = _client.GetAllNetworkMetrics(new GetAllNetworkMetricsApiRequest
                        {
                            FromTime = fromTime,
                            ToTime = toTime,
                            Addres = agent.IpAddress
                        });

                        if (allNetworkMetrics.Metrics != null)
                        {
                            foreach (var metric in allNetworkMetrics.Metrics)
                            {
                                _provider.GetService<INetworkMetricsRepository>()?.Create(new NetworkMetricModel
                                {
                                    IdAgent = agent.Id,
                                    Time = metric.Time,
                                    Value = metric.Value
                                });
                            }
                        }

                        AllRamMetricsApiResponse allRamMetrics = _client.GetAllRamMetrics(new GetAllRamMetricsApiRequest
                        {
                            FromTime = fromTime,
                            ToTime = toTime,
                            Addres = agent.IpAddress
                        });

                        if (allRamMetrics.Metrics != null)
                        {
                            foreach (var metric in allRamMetrics.Metrics)
                            {
                                _provider.GetService<IRamMetricsRepository>()?.Create(new RamMetricModel
                                {
                                    IdAgent = agent.Id,
                                    Time = metric.Time,
                                    Value = metric.Value
                                });
                            }
                        }

                        AllHddMetricsApiResponse allHddMetrics = _client.GetAllHddMetrics(new GetAllHddMetricsApiRequest
                        {
                            FromTime = fromTime,
                            ToTime = toTime,
                            Addres = agent.IpAddress
                        });

                        if (allHddMetrics.Metrics != null)
                        {
                            foreach (var metric in allHddMetrics.Metrics)
                            {
                                _provider.GetService<IHddMetricsRepository>()?.Create(new HddMetricModel
                                {
                                    IdAgent = agent.Id,
                                    Time = metric.Time,
                                    Value = metric.Value
                                });
                            }
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
