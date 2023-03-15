using Http.Helper.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

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
    }
}
