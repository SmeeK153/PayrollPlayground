namespace API.Controllers.Companies.v1.Requests
{
    public class AddEmployeeToCompanyRequest
    {
        public long TaxIdentificationNumber { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }
}