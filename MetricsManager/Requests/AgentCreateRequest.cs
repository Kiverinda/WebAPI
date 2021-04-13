using System;

namespace MetricsManager.Requests
{
    public class AgentCreateRequest
    {
        public bool Status { get; set; }
        public string Ipaddres { get; set; }
        public string Name { get; set; }
    }
}
