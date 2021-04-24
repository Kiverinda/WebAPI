using System;
using System.Collections.Generic;

namespace MetricsManagerDesktop.UserControls
{
    public interface IDotNetMetricsCard
    {
        void StartView();
        void StopView();
        void SetFromTime(DateTimeOffset dateTimeOffset);
        void SetToTime(DateTimeOffset dateTimeOffset);
        void SetAgent(KeyValuePair<int, string> agent);
        void ViewRange();
    }
}
