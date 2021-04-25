using MetricsManagerDesktop.Requests;
using MetricsManagerDesktop.Responses;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManagerDesktop.Models
{
    class HddMetricsCardModel : IHddMetricsCardModel
    {
        private readonly HttpClient _httpClient;
        
        public HddMetricsCardModel()
        {
            _httpClient = new HttpClient();
        }
        
        public AllHddMetricsApiResponse GetHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:5050/api/metrics/hdd/agent/{request.Agent}/from/{request.FromTime:o}/to/{request.ToTime:o}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var content = streamReader.ReadToEnd();
                        var result = JsonSerializer.Deserialize<AllHddMetricsApiResponse>(content, new JsonSerializerOptions()
                            { PropertyNameCaseInsensitive = true });
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
    }
}
