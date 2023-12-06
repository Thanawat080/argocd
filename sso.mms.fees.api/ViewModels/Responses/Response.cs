namespace sso.mms.fees.api.ViewModels.Responses
{
    public class Response<T>
    {
        public T? Result { get; set; }
        public int? Status { get; set; }

        public string? Message { get; set; }
    }

    public class ResponseList<T>
    {
        public List<T>? ResultList { get; set; }
        public int? Status { get; set; }

        public string? Message { get; set; }
    }

    public class PingModels
    {
        public int? jobId { get; set; }
        public List<string>? filepath { get; set; }
    }
}
