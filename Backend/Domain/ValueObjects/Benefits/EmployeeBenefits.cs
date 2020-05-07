using Domain.Entities.People;

namespace Domain.ValueObjects.Benefits
{
    public class EmployeeBenefits : CompanyBenefits
    {
        public EmployeeBenefits(Person @for) : base(100000, @for)
        {
        }
    }
}