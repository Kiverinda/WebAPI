using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MetricsManagerClient.Responses;

namespace MetricsManagerClient.Controllers
{
    public static class CpuController
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(AllCpuMetricsResponse product)
        {
            foreach (CpuMetricManagerDto item in product.Metrics)
            {
               // Console.WriteLine($"Time: {item.Time}\tValue: " + $"{item.Value}\tAgent: {item.IdAgent}");
            }
        }

        static async Task<AllCpuMetricsResponse> GetProductAsync(string path)
        {
            AllCpuMetricsResponse product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<AllCpuMetricsResponse>();
            }

            return product;
        }

        //static void Main()
        //{
        //    RunAsync().GetAwaiter().GetResult();
        //}

        public static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:5050/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                string url = @"api/metrics/cpu/all";
                AllCpuMetricsResponse product = await GetProductAsync(url);
                ShowProduct(product);
                MainWindow._cpuChart.ColumnSeriesValues[0].Values.Add(48d);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}

