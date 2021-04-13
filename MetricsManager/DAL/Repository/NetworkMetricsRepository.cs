using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repository
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(NetworkMetricModel item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO networkmetrics(value, time, idagent) VALUES(@value, @time, @idagent)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    idagent = item.IdAgent
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("DELETE FROM networkmetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(NetworkMetricModel item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<NetworkMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            var q = connection
                .Query<NetworkMetricModel>($"SELECT id, time, value From networkmetrics")
                .ToList();
            return q;
        }

        public NetworkMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .QuerySingle<NetworkMetricModel>("SELECT Id, Time, Value FROM networkmetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }

        public IList<NetworkMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .Query<NetworkMetricModel>(
                    $"SELECT id, time, value From networkmetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .Query<NetworkMetricModel>(
                    $"SELECT id, time, value From networkmetrics WHERE time > @fromTime AND time < @toTime AND idagent = {idAgent}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                    })
                .ToList();
        }

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .Query<NetworkMetricModel>($"SELECT * FROM networkmetrics WHERE time > @fromTime AND time < @toTime ORDER BY {field}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field, int idAgent)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .Query<NetworkMetricModel>($"SELECT * FROM networkmetrics WHERE time > @fromTime AND time < @toTime AND idagent = {idAgent} ORDER BY {field}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                    })
                .ToList();
        }

        public NetworkMetricModel LastLine()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .QuerySingle<NetworkMetricModel>("SELECT * FROM networkmetrics ORDER BY time DESC LIMIT 1");
        }
    }
}
