using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Document.Api.Test.Infrastructure
{
    public class StandardHttpClient : IDisposable
    {
        private readonly HttpClient client;

        public StandardHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<T> PostAsJsonAsync<T>(string uri, object item, Dictionary<string, string> headers = null)
        {
            var requestMessage = GetHttpRequestMessage(HttpMethod.Post, uri, headers);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
            var response = await client.SendAsync(requestMessage);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T item, Dictionary<string, string> headers = null)
        {
            var requestMessage = GetHttpRequestMessage(HttpMethod.Post, uri, headers);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
            var response = await client.SendAsync(requestMessage);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsUrlEncodedAsync(string uri, Dictionary<string, string> formPostBodyData, Dictionary<string, string> headers = null)
        {
            var requestMessage = GetHttpRequestMessage(HttpMethod.Post, uri, headers);
            requestMessage.Content = new FormUrlEncodedContent(ToFormPostData(formPostBodyData));
            return await client.SendAsync(requestMessage);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string> headers = null)
        {
            var requestMessage = GetHttpRequestMessage(HttpMethod.Get, uri, headers);
            return await client.SendAsync(requestMessage);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri, Dictionary<string, string> headers = null)
        {
            var requestMessage = GetHttpRequestMessage(HttpMethod.Delete, uri, headers);
            return await client.SendAsync(requestMessage);
        }

        private List<KeyValuePair<string, string>> ToFormPostData(Dictionary<string, string> formPostBodyData)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            formPostBodyData.Keys.ToList().ForEach(key =>
            {
                result.Add(new KeyValuePair<string, string>(key, formPostBodyData[key]));
            });
            return result;
        }

        private HttpRequestMessage GetHttpRequestMessage(HttpMethod method, string uri, Dictionary<string, string> headers)
        {
            var requestMessage = new HttpRequestMessage(method, uri);

            if (headers == null) return requestMessage;

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            return requestMessage;
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
