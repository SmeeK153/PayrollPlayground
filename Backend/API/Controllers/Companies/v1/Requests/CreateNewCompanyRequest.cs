namespace API.Controllers.Companies.v1.Requests
{
    public class CreateNewCompanyRequest
    {
        public string Name { get; set; }
        public int PaychecksPerYear { get; set; }
    }
}