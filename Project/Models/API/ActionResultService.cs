using Newtonsoft.Json;

namespace BTLWEB.Models.API
{
    public class ActionResultService
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public StatusHttpCode StatusCode { get; set; }
        public object? Data { get; set; }

        public ActionResultService() { }
        public ActionResultService(bool success, string message, StatusHttpCode statusCode)
        {
            this.Success = success;
            this.Message = message;
            this.StatusCode = statusCode;
        }

        public ActionResultService(bool success, string message, StatusHttpCode statusCode, object data)
        {
            this.Success = success;
            this.Message = message;
            this.StatusCode = statusCode;
            this.Data = data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


    }
}
