using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Plugin.RestClient
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class PlayerClient<T>
    {
        //private const string WebServiceUrl = "http://192.168.0.12/TekkersService/tables/player/"; //RUN LOCALLY 
        private const string WebServiceUrl = "http://tekkers.azurewebsites.net/tables/player/";

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

            var response = await httpClient.DeleteAsync(WebServiceUrl+"DeletePlayer/" + id);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<T>> GetPlayerByName(string name)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl+"GetPlayerByName/"+name);

            var results = JsonConvert.DeserializeObject<List<T>>(json);

            return results;
        }

        public async Task<List<T>> GetPlayersByAge(int age)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl+ "GetPlayersByAge/" + age);

            var results = JsonConvert.DeserializeObject<List<T>>(json);

            return results;
        }

        public async Task<List<T>> GetPlayersOnTeamAsync(string id)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl + "GetPlayersOnTeam/" + id);

            var playersOnTeam = JsonConvert.DeserializeObject<List<T>>(json);

            return playersOnTeam;
        }
    }
}
