using System;
using Domain.ValueObjects;
using Foundations.Core;

namespace Domain.Entities.People
{
    public class Person : Entity
    {
        public Name Name { get; private set; }

        public Person(Guid id, Name name) : base(id) => (Name) = (name);
    }
}