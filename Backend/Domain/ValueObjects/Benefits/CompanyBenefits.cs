using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.People;
using Domain.Enumerations;
using Domain.ValueObjects.Discounts;

namespace Domain.ValueObjects.Benefits
{
    public abstract class CompanyBenefits : PeriodicPayment
    {
        private List<CompanyBenefitsDiscount> Discounts { get; } = new List<CompanyBenefitsDiscount>();
        protected CompanyBenefits(long amountInCents, Person @for) : base(amountInCents, Duration.Annual)
        {
            Discounts.Add(new NameStartsWithADiscount(@for.Name));
        }

        public long CalculateCost()
        {
            var amount = AmountInCents * Discounts.Aggregate(1.0, (totalDiscount, discount) => totalDiscount * discount.CalculateDiscount());
            return Convert.ToInt64(amount);
        }
    }
}