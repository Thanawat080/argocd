namespace sso.mms.login.ViewModels
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
        public List<T>? ResultList { get; set; }
    }

    public class ResponseResult<T>
    {
        public int StatusCode { get; set; }
        public bool? Status { get; set; }
        public T? Result { get; set; }
    }

    public class ResponseResultList<T>
    {
        public int StatusCode { get; set; }
        public bool? Status { get; set; }
        public List<T>? ResultList { get; set; }
    }

    public class ResponseChangePasswordModel
    {
        public int StatusCode { get; set;}
        public string? Message { get; set; }
        public bool isStatus { get; set; }

    }
}
