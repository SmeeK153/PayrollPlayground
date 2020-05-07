namespace API.Controllers.Companies.v1.Requests
{
    public class CreateNewEmployee
    {
        public string Name { get; set; }
        public long AnnualSalaryInCents { get; set; }
    }
}