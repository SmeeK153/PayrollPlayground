using System.Collections.Generic;

namespace Domain.ValueObjects.Discounts
{
    public class NameStartsWithADiscount : CompanyBenefitsDiscount
    {
        protected override double Discount { get; } = 0.1;
        
        public Name For { get; }

        public NameStartsWithADiscount(Name name) => (For) = (name);
        
        protected override IEnumerable<object> GetComponentValues()
        {
            yield return base.GetComponentValues();
            yield return For;
        }

        public override double CalculateDiscount() => (For.First.ToLower().StartsWith("a") ? 0.9 : 1);
    }
}