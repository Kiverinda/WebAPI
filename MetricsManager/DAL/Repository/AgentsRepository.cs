using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repository
{

    public class AgentsRepository : IAgentsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public void Create(AgentModel item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute($"INSERT INTO agents(status, ipaddress, name) VALUES(@status, @ipaddress, @name)",
                new
                {
                    status = Convert.ToInt32(item.Status),
                    ipaddress = item.Ipaddres,
                    name = item.Name
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute($"DELETE FROM agents WHERE id = @id",
                new
                {
                    id = target
                });
        }

        public void Update(AgentModel item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute($"UPDATE agents SET status = @status, ipaddress = @ipaddress, name = @name WHERE id = @id",
                new
                {
                    status = Convert.ToInt32(item.Status),
                    ipaddress = item.Ipaddres,
                    name = item.Name,
                    id = item.Id
                });
        }

        public IList<AgentModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            var q = connection
                .Query<AgentModel>($"SELECT * From agents")
                .ToList();
            return q;
        }

        public AgentModel GetById(int target)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection
                .QuerySingle<AgentModel>("SELECT * FROM agents WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }
    }
}
