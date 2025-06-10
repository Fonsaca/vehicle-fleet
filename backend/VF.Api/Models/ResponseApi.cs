using System.Net;

namespace VF.Api.Models
{
    public class ResponseApi<TData>
    {

        public HttpStatusCode StatusCode { get; set; }

        public string Status
        {
            get
            {
                return StatusCode.ToString();
            }
        }

        public string Message { get; set; }

        public List<TData> Data { get; set; } = new List<TData>();

        public int Count
        {
            get
            {
                return Data.Count;
            }
        }

        public ResponseApi(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ResponseApi(HttpStatusCode statusCode, string message, List<TData> data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;    
        }
    }
}
