using MetricsManagerDesktop.Requests;
using MetricsManagerDesktop.Responses;

namespace MetricsManagerDesktop.Models
{
    public interface IHddMetricsCardModel
    {
        AllHddMetricsApiResponse GetHddMetrics(GetAllHddMetricsApiRequest request);
    }
}
