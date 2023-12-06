namespace sso.mms.fees.api.ViewModels.Responses
{
    public class AiResponse
    {
        public int? jobId { get; set; }
    }
    public class AiStatusResponse
    { 
        public int? jobId { get; set;} 
        public string? status { get; set;}
        public string? outpuUrl { get; set;}
    
    }

    public class AiOutputResponse
    {
        public int? jobId { get; set; }
        public List<string> filename { get; set; }

    }

    public class AiOutputCsv
    {
        public string? petition_id { get; set; }
        public string? main_hos { get; set; }
        public string? treatment_date { get; set; }
        public string? proactive { get; set; }
        public string? pid_manual { get; set; }
        public string? end_week { get; set; }
        public string? outlier_tyoes { get; set; }
        public string? error_message { get; set; }

    }
}