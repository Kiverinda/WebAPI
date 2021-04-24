using MetricsManagerDesktop.Requests;
using MetricsManagerDesktop.Responses;

namespace MetricsManagerDesktop.Models
{
    public interface IRamMetricsCardModel
    {
        AllRamMetricsApiResponse GetRamMetrics(GetAllRamMetricsApiRequest request);
    }
}
