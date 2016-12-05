using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TekkersV2.Models;

namespace Plugin.RestClient.AssessmentClient
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class AssessmentClient<T>
    {
        //private const string WebServiceUrl = "http://192.168.0.12/TekkersService/tables/assessment/";
        private const string WebServiceUrl = "http://tekkers.azurewebsites.net/tables/assessment/";

        public async Task<List<T>> GetAsync()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl);

            var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

            return taskModels;
        }

        public async Task<bool> PostAsync(T t)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(WebServiceUrl, httpContent);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsync(string id, T t)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync(WebServiceUrl + id, httpContent);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var response = await httpClient.DeleteAsync(WebServiceUrl + id);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<T>> GetByNameAsync(string name)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl + name);

            var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

            return taskModels;
        }

        public async Task<bool> EnterAssessmentScoreAsync(string id, int score)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = JsonConvert.SerializeObject(score);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync(WebServiceUrl+"PutAssessmentScore/"+id+"/"+score, httpContent);

            return result.IsSuccessStatusCode;
        }
    }
}


