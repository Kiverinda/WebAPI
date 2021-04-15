using System;

namespace MetricsManager.Requests
{
    public class AgentCreateRequest
    {
        public bool Status { get; set; }
        public string Ipaddress { get; set; }
        public string Name { get; set; }
    }
}
