
using Newtonsoft.Json;
using System.Net;

namespace storytiling.core.Contracts
{

    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Data { get; set; }

    }

}
