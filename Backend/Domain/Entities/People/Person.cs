using Domain.ValueObjects;
using Foundations.Core;

namespace Domain.Entities.People
{
    public class Person : Entity
    {
        public bool TaxIdentificationProvided { get; private set; }
        public Name Name { get; private set; }
    }
}