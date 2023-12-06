namespace sso.mms.fees.admin.ViewModels.Responses
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
}
