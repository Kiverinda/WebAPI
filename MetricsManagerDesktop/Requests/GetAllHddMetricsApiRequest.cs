using System;

namespace MetricsManagerDesktop.Requests
{
    public class GetAllHddMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public int Agent { get; set; }
    }
}
