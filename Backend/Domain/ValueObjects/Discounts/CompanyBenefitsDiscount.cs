using System.Collections.Generic;
using Foundations.Core;

namespace Domain.ValueObjects.Discounts
{
    public abstract class CompanyBenefitsDiscount : ValueObject
    {
        protected abstract double Discount { get; }
        
        protected override IEnumerable<object> GetComponentValues()
        {
            yield return Discount;
        }

        public abstract double CalculateDiscount();
    }
}