using System;
using Domain.Entities.Dependents;
using MediatR;

namespace Domain.Events.Companies
{
    public class AddEmployeeDependent : CoreDomainEvent, INotification
    {
        public Guid Employee { get; set; }
        public Dependent Dependent { get; set; }
    }
}