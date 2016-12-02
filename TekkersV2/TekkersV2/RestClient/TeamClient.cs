using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TekkersV2.RestClient
{
    public class TeamClient<T>
    {
        //private const string WebServiceUrl = "http://192.168.0.12/TekkersService/tables/team/";
        private const string WebServiceUrl = "http://tekkers.azurewebsites.net/tables/team/";

        public async Task<List<T>> GetAsync()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl);

            var teams = JsonConvert.DeserializeObject<List<T>>(json);

            return teams;
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

        public async Task<List<T>> GetTeamByNameAsync(string teamname)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl + teamname);

            var team = JsonConvert.DeserializeObject<List<T>>(json);

            return team;
        }

        public async Task<List<T>> GetTeamsByAgeAsync(int teamage)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl + teamage);

            var teams = JsonConvert.DeserializeObject<List<T>>(json);

            return teams;
        }

        public async Task<List<T>> GetAllTeams()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var json = await httpClient.GetStringAsync(WebServiceUrl);

            var allTeams = JsonConvert.DeserializeObject<List<T>>(json);

            return allTeams;
        }
    }
}
