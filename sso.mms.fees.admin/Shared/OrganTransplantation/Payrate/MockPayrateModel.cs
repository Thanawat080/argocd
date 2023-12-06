namespace sso.mms.fees.admin.Shared.OrganTransplantation.Payrate
{
    public class MockPayrateModel
    {
        public string? No { get; set; }
        public string? ClaimNumber { get; set; }
        public string? ClaimName { get; set; }
        public string? Amount { get; set; }
        public string? FinancialAmount { get; set; }
        public string? NoContent { get; set; }
        
        public MockPayrateModel(
         string? no,
         string? claimNumber,
         string? claimName,
         string? amount,
         string? financialAmount,
         string? noContent
         )
        {
            No = no;
            ClaimNumber = claimNumber;
            ClaimName = claimName;
            Amount = amount;
            FinancialAmount = financialAmount;
            NoContent = noContent;
            
        }
       
    }
}
