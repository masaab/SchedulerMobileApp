using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Scheduler.Exceptions;

namespace Scheduler.Repository
{
    public class DataRepository : IDataRepository
    {
        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                var response = await GeHttpResponseMessageForGetRequest(uri);
                var jsonResult = string.Empty;

                if (response.IsSuccessStatusCode)
                {
                    jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var classObject = JsonConvert.DeserializeObject<T>(jsonResult);

                    return classObject;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return default;
            //throw new HttpRequestExceptionEx(response.StatusCode, jsonResult);
        }

        public async Task DeleteAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient();
            await httpClient.DeleteAsync(uri);
        }

        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Console.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content).ConfigureAwait(false));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<R> PostAsync<T, R>(string uri, T data, R returnValue, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Console.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content).ConfigureAwait(false));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<R>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private async Task<R> ExecuteRequestInPollyService<T,R>(string uri, T data,R returnValue)
        {
            HttpClient httpClient = CreateHttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string jsonResult = string.Empty;
            var responseMessage = await Policy
                .Handle<WebException>(ex =>
                {
                    Console.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                    return true;
                })
                .WaitAndRetryAsync
                (
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                )
                .ExecuteAsync(async () => await httpClient.PostAsync(uri, content).ConfigureAwait(false));

            if (responseMessage.IsSuccessStatusCode)
            {
                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<R>(jsonResult);
                return json;
            }

            if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception(jsonResult);
            }

            throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
        }

        public Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            throw new Exception();
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        //TO DO: Create a singleton for HttpClient
        //TO DO: use best practices as described in course
        private async Task<HttpResponseMessage> GeHttpResponseMessageForGetRequest(string url)
        {
            HttpClient httpClient = CreateHttpClient();

            var response = await Policy
                .Handle<WebException>(ex =>
                {
                    Debug.WriteLine($"{ex.GetType().Name}");
                    return true;
                })
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () => await httpClient.GetAsync(url));

            return response;    
        }
    }
}
