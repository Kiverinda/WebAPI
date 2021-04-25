using MetricsManagerDesktop.Requests;
using MetricsManagerDesktop.Responses;

namespace MetricsManagerDesktop.Models
{
    public interface IDotNetMetricsCardModel
    {
        AllDotNetMetricsApiResponse GetDotNetMetrics(GetAllDotNetMetricsApiRequest request);
    }
}
