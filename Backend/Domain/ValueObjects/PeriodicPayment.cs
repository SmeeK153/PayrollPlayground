using System.Collections.Generic;
using Domain.Enumerations;
using Foundations.Core;

namespace Domain.ValueObjects
{
    public class PeriodicPayment : ValueObject
    {
        public long AmountInCents { get; }
        public Duration PaymentPeriod { get; }

        public PeriodicPayment(long amountInCents, Duration payPeriod) => (AmountInCents, PaymentPeriod) = (amountInCents, payPeriod);
        public PeriodicPayment ConvertTo(Duration newPaymentPeriod)
        {
            var adjustedSalaryAmount = AmountInCents * (PaymentPeriod.TimesPerYear / newPaymentPeriod.TimesPerYear);
            return new PeriodicPayment(adjustedSalaryAmount, newPaymentPeriod);
        }

        protected override IEnumerable<object> GetComponentValues()
        {
            yield return AmountInCents;
            yield return PaymentPeriod;
        }
    }
}