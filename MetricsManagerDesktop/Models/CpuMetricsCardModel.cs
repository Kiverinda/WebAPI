using MetricsManagerDesktop.Requests;
using MetricsManagerDesktop.Responses;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManagerDesktop.Models
{
    class CpuMetricsCardModel : ICpuMetricsCardModel
    {
        private readonly HttpClient _httpClient;
        
        public CpuMetricsCardModel()
        {
            _httpClient = new HttpClient();
        }
        
        public AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:5050/api/metrics/cpu/agent/{request.Agent}/from/{request.FromTime:o}/to/{request.ToTime:o}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var content = streamReader.ReadToEnd();
                        var result = JsonSerializer.Deserialize<AllCpuMetricsApiResponse>(content, new JsonSerializerOptions()
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
