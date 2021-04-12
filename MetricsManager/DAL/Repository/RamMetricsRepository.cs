using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repository
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly SQLiteConnection _connection;

        public RamMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(RamMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("INSERT INTO rammetrics (value, time, idagent) VALUES(@value, @time, @idagent)",
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
            connection.Execute("DELETE FROM rammetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(RamMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<RamMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<RamMetricModel>($"SELECT * From rammetrics")
                .ToList();
        }

        public RamMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .QuerySingle<RamMetricModel>("SELECT * FROM rammetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }

        public IList<RamMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection.Query<RamMetricModel>($"SELECT * From rammetrics WHERE time > @FromTime AND time < @ToTime",
                new
                    {
                    FromTime = fromTime,
                    ToTime = toTime
                    })
                .ToList();
        }

        public IList<RamMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection.Query<RamMetricModel>($"SELECT * From rammetrics WHERE time > @FromTime AND time < @ToTime AND idagent = @idagent",
                    new
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        idagent = idAgent
                    })
                .ToList();
        }

        public IList<RamMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection.Query<RamMetricModel>($"SELECT * From rammetrics WHERE time > @FromTime AND time < @ToTime ORDER BY {field}",
                    new
                    {
                        FromTime = fromTime,
                        ToTime = toTime
                    })
                .ToList();
        }

        public IList<RamMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field, int idAgent)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection.Query<RamMetricModel>($"SELECT * From rammetrics WHERE time > @FromTime AND time < @ToTime AND idagent = @idagent ORDER BY {field} ",
                    new
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        idagent = idAgent
                    })
                .ToList();
        }

        public RamMetricModel LastLine()
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .QuerySingle<RamMetricModel>("SELECT * FROM rammetrics ORDER BY time DESC LIMIT 1");
        }
    }
}
