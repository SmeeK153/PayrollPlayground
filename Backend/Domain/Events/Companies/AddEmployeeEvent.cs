using System;
using Domain.Entities.Employees;
using MediatR;

namespace Domain.Events.Companies
{
    public class AddEmployeeEvent : CoreDomainEvent, INotification
    {
        public Guid Company { get; set; }
        public Employee Employee { get; set; }
    }
}