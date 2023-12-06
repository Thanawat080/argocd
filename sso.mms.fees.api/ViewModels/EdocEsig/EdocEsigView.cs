namespace sso.mms.fees.api.ViewModels.EdocEsig
{
    public class EdocEsigView
    {
        
    }

    public class ResCreatePdf
    {
        public string filename { get; set; }
    }

    public class CreateDocument
    { 
        public string pathFile { get; set; }
        public string identification_number { get; set; }
        public string doc_title { get; set; }
        public string filename { get; set; }
    }

    public class ResCreateDocument
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StatusDateTime { get; set; }
        public string Value { get; set; }
    }

    public class ResSendSign
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StatusDateTime { get; set; }
        public SubResSendSign Value { get; set; }
    }

    public class SubResSendSign
    {
        public int RouteId { get; set; }
        public List<SubDetailResSendSign> RouteReceiveList { get; set; }
    }

    public class SubDetailResSendSign
    {
        public string IdentityNumber { get; set; }
        public int RouteRecvId { get; set; }
    }

    public class SendSignView
    {
        public string userId { get; set; }
        public string documentRef { get; set; }
        public string? subject { get; set; }
        public string? body { get; set; }
        public int flowType { get; set; }

        public string? dueDate { get; set; }
        public int priority { get; set; }
        public string? successCallback { get; set; }
        public string? failCallback { get; set; }

        public List<string> sendTo { get; set; }

    }
}
