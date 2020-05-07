using Domain.Entities.People;

namespace Domain.ValueObjects.Benefits
{
    public class DependentBenefits : CompanyBenefits
    {
        public DependentBenefits(Person @for) : base(50000, @for)
        {
        }
    }
}