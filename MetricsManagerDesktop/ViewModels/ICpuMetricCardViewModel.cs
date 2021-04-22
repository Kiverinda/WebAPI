using System;
using System.Collections.Generic;
using MetricsManagerDesktop.Requests;

namespace MetricsManagerDesktop.ViewModels
{
    public interface ICpuMetricsCardViewModel
    {
        void UpdateCpuMetrics(GetAllCpuMetricsApiRequest request);
        void StartView();
        void StopView();
        void SetFromTime(DateTimeOffset fromTime);
        void ViewRange();
        void SetToTime(DateTimeOffset toTime);
        void SetAgent(KeyValuePair<int, string> agent);
    }
}
