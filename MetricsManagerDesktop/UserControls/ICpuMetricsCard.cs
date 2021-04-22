using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerDesktop.Interfaces
{
    public interface ICpuMetricsCard
    {
        void OnPropertyChanged(string propertyName);
        void StartView();
        void StopView();
        void SetFromTime(DateTimeOffset dateTimeOffset);
        void SetToTime(DateTimeOffset dateTimeOffset);
        void SetAgent(KeyValuePair<int, string> agent);
        void ViewRange();
    }
}
