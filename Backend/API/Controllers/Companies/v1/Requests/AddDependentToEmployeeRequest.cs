namespace API.Controllers.Companies.v1.Requests
{
    public class AddDependentToEmployeeRequest
    {
        public long TaxIdentificationNumber { get; set; }
        public string DependentFirstName { get; set; }
        public string DependentLastName { get; set; }
    }
}