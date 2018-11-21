using Newtonsoft.Json;
using Polly;
using SuperBook.Contracts.Repository;
using SuperBook.Exceptions;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SuperBook.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        private HttpClient CreateHttpClient(string authToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }

            return client;
        }
        public async Task DeleteAsync(string uri, string authToken = "")
        {
            HttpClient client = this.CreateHttpClient(authToken);
            await client.DeleteAsync(uri);
        }

        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                HttpClient client = this.CreateHttpClient(uri);
                string result = string.Empty;

                var response = await Policy.Handle<WebException>(ex =>
                {
                    Debug.WriteLine(ex.GetType().Name + " : " + ex.Message);
                    return true;
                })
                .WaitAndRetryAsync
                (
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                ).ExecuteAsync(() => client.GetAsync(uri));

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(result);

                    return json;
                }

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(result);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, result);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetType().Name + " : " + e.Message);
                throw;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient client = this.CreateHttpClient(uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string result = string.Empty;

                var response = await Policy.Handle<WebException>(ex =>
                {
                    Debug.WriteLine(ex.GetType().Name + " : " + ex.Message);
                    return true;
                })
                .WaitAndRetryAsync
                (
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                ).ExecuteAsync(() => client.PostAsync(uri, content));

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(result);

                    return json;
                }

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(result);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, result);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetType().Name + " : " + e.Message);
                throw;
            }
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient client = this.CreateHttpClient(uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string result = string.Empty;

                var response = await Policy.Handle<WebException>(ex =>
                {
                    Debug.WriteLine(ex.GetType().Name + " : " + ex.Message);
                    return true;
                })
                .WaitAndRetryAsync
                (
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                ).ExecuteAsync(() => client.PostAsync(uri, content));

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<TR>(result);

                    return json;
                }

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(result);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, result);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetType().Name + " : " + e.Message);
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient client = this.CreateHttpClient(uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string result = string.Empty;

                var response = await Policy.Handle<WebException>(ex =>
                {
                    Debug.WriteLine(ex.GetType().Name + " : " + ex.Message);
                    return true;
                })
                .WaitAndRetryAsync
                (
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                ).ExecuteAsync(() => client.PutAsync(uri, content));

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(result);

                    return json;
                }

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(result);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, result);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetType().Name + " : " + e.Message);
                throw;
            }
        }
    }
}
