﻿using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repository
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly SQLiteConnection _connection;

        public CpuMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("INSERT INTO cpumetrics(value, time, idagent) VALUES(@value, @time, @idagent)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    idagent = item.IdAgent
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("DELETE FROM cpumetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(CpuMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<CpuMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_connection);
            var q = connection
                .Query<CpuMetricModel>($"SELECT * From cpumetrics")
                .ToList();
            return q;
        }

        public CpuMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .QuerySingle<CpuMetricModel>("SELECT * FROM cpumetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }

        public IList<CpuMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<CpuMetricModel>(
                    $"SELECT * From cpumetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<CpuMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<CpuMetricModel>(
                    $"SELECT * From cpumetrics WHERE time > @FromTime AND time < @ToTime AND idagent = @IdAgent",
                    new
                    {
                        FromTime = fromTime.ToUnixTimeSeconds(),
                        ToTime = toTime.ToUnixTimeSeconds(),
                        IdAgent = idAgent
                    })
                .ToList();
        }

        public IList<CpuMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<CpuMetricModel>($"SELECT * FROM cpumetrics WHERE time > @FromTime AND time < @ToTime ORDER BY {field}",
                    new
                    {
                        FromTime = fromTime.ToUnixTimeSeconds(),
                        ToTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<CpuMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field, int idAgent)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<CpuMetricModel>($"SELECT * FROM cpumetrics WHERE time > @FromTime AND time < @ToTime AND idagent = @IdAgent ORDER BY {field}",
                    new
                    {
                        FromTime = fromTime.ToUnixTimeSeconds(),
                        ToTime = toTime.ToUnixTimeSeconds(),
                        IdAgent = idAgent
                    })
                .ToList();
        }

        public CpuMetricModel LastLine()
        {
            using var connection = new SQLiteConnection(_connection);
            var result = connection.QuerySingle<CpuMetricModel>("SELECT * FROM cpumetrics ORDER BY time DESC LIMIT 1");
            return result;
        }
    }
}
