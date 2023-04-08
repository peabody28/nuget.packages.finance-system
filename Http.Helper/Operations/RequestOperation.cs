using Http.Helper.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text;

namespace Http.Helper.Operations
{
    public class RequestOperation : IRequestOperation
    {
        private readonly HttpClient httpClient;

        public RequestOperation()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Get request to the resource
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns>HTTP status code</returns>
        public async Task<HttpResponseMessage> Get(string url, IDictionary<string, string>? data = null, IDictionary<string, string>? headers = null)
        {
            var queryString = data != null ? QueryHelpers.AddQueryString(url, data) : url;
            var uri = new Uri(queryString);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            
            if(headers != null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);
            
            var response = await httpClient.SendAsync(request);
            return response;
        }

        /// <summary>
        /// Post request to the resource
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns>HTTP status code</returns>
        public async Task<HttpResponseMessage> Post(string url, object data, IDictionary<string, string>? headers = null)
        {
            return await RequestWithBody(HttpMethod.Post, url, data, headers);
        }

        /// <summary>
        /// Put request to the resource
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns>HTTP status code</returns>
        public async Task<HttpResponseMessage> Put(string url, object data, IDictionary<string, string>? headers = null)
        {
            return await RequestWithBody(HttpMethod.Put, url, data, headers);
        }

        /// <summary>
        /// Delete request to the resource
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns>HTTP status code</returns>
        public async Task<HttpResponseMessage> Delete(string url, object data, IDictionary<string, string>? headers = null)
        {
            return await RequestWithBody(HttpMethod.Delete, url, data, headers);
        }

        /// <summary>
        /// POST, PUT or DELETE request to the resource (JSON)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns>HTTP status code</returns>
        private async Task<HttpResponseMessage> RequestWithBody(HttpMethod method, string url, object data, IDictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(method, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            if (headers != null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

            var response = await httpClient.SendAsync(request);
            return response;
        }
    }
}
