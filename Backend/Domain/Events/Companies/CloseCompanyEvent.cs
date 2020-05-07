using System;
using MediatR;

namespace Domain.Events.Companies
{
    public class CloseCompanyEvent : CoreDomainEvent, INotification
    {
        public Guid Id { get; set; }
    }
}