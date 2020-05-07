using System;
using MediatR;

namespace Domain.Events.Companies
{
    public class RemoveEmployeeEvent : CoreDomainEvent, INotification
    {
        public Guid Company { get; set; }
        public Guid Employee { get; set; }
    }
}