using Domain.Enumerations;

namespace Domain.ValueObjects
{
    public class Salary : PeriodicPayment
    {
        public Salary(long amountInCents) : base(amountInCents, Duration.FortNightly)
        {
        }
    }
}