namespace Http.Helper.Interfaces
{
    public interface IRequestOperation
    {
        Task<HttpResponseMessage> Get(string url, IDictionary<string, string>? data = null, IDictionary<string, string>? headers = null);
    }
}
