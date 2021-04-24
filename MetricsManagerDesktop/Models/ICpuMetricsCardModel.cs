using MetricsManagerDesktop.Requests;
using MetricsManagerDesktop.Responses;

namespace MetricsManagerDesktop.Models
{
    public interface ICpuMetricsCardModel
    {
        AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);
    }
}
