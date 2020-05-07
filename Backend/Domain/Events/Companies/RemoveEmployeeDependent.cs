using System;
using MediatR;

namespace Domain.Events.Companies
{
    public class RemoveEmployeeDependent : CoreDomainEvent, INotification
    {
        public Guid Employee { get; set; }
        public Guid Dependent { get; set; }
    }
}