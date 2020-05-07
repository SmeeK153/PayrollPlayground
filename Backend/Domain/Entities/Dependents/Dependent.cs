using Domain.Entities.People;
using Domain.ValueObjects.Benefits;
using Foundations.Core;

namespace Domain.Entities.Dependents
{
    public class Dependent : Entity
    {
        public Person Person { get; private set; }
        
        public CompanyBenefits Benefits { get; private set; }

        public Dependent(Person @for)
        {
            Person = @for;
            Benefits = new DependentBenefits(@for);
        }
    }
}