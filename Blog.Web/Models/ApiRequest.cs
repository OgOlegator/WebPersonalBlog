namespace Blog.Web.Models
{
    public class ApiRequest
    {

        public SD.ApiType APIType { get; set; } = SD.ApiType.GET;

        public string Url { get; set; }

        public object Data { get; set; }

        public string AccessToken { get; set; }
    }
}
