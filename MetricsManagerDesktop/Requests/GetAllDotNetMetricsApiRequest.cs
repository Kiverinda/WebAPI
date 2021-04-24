using System;

namespace MetricsManagerDesktop.Requests 
{ 
    public class GetAllDotNetMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public int Agent { get; set; }
    }
}
