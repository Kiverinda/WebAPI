using LiveCharts;
using LiveCharts.Wpf;
using MetricsManagerDesktop.Requests;
using MetricsManagerDesktop.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Documents;
using System.Windows.Threading;
using MetricsManagerDesktop.Responses;

namespace MetricsManagerDesktop.ViewModels
{
    public class AgentViewModel : IAgentViewModel, INotifyPropertyChanged
    {
        public Dictionary<int, string> Agents{ get; private set; }
        private readonly HttpClient _httpClient;

        public AgentViewModel()
        {
            _httpClient = new HttpClient();
            Agents = new Dictionary<int,string>();
            GetAllAgents();
        }

        public void GetAllAgents()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:5050/api/agents/all");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var content = streamReader.ReadToEnd();
                        var result = JsonSerializer.Deserialize<AllAgentsResponse>(content, new JsonSerializerOptions()
                            { PropertyNameCaseInsensitive = true });

                        if (result.Metrics.Count == 0)
                        {
                            return;
                        }
                        foreach (var item in result.Metrics)
                        {
                            Agents.Add(item.Id, item.Ipaddress);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
