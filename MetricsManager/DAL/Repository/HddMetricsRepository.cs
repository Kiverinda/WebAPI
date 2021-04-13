﻿using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repository
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HddMetricModel item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO hddmetrics (value, time, idagent) VALUES(@value, @time, @idagent)",
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
            connection.Execute("DELETE FROM hddmetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(HddMetricModel item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("UPDATE hddmetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<HddMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .Query<HddMetricModel>($"SELECT * From hddmetrics")
                .ToList();
        }

        public HddMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .QuerySingle<HddMetricModel>("SELECT * FROM hddmetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<HddMetricModel>($"SELECT * From hddmetrics WHERE time > @FromTime AND time < @ToTime",
                new
                    {
                    FromTime = fromTime,
                    ToTime = toTime
                    })
                .ToList();
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<HddMetricModel>($"SELECT * From hddmetrics WHERE time > @FromTime AND time < @ToTime AND idagent = @IdAgent",
                    new
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        IdAgent = idAgent
                    })
                .ToList();
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<HddMetricModel>($"SELECT * From hddmetrics WHERE time > @FromTime AND time < @ToTime ORDER BY {field}",
                    new
                    {
                        FromTime = fromTime,
                        ToTime = toTime
                    })
                .ToList();
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field, int idAgent)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<HddMetricModel>($"SELECT * From hddmetrics WHERE time > @FromTime AND time < @ToTime AND idagent = @IdAgent ORDER BY {field} ",
                    new
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        IdAgent = idAgent
                    })
                .ToList();
        }

        public HddMetricModel LastLine()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .QuerySingle<HddMetricModel>("SELECT * FROM hddmetrics ORDER BY time DESC LIMIT 1");
        }
    }
}
