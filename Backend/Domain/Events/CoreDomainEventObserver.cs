using System;
using Foundations.Events;
using MediatR;

namespace Domain.Events
{
    public class CoreDomainEventObserver : DomainEventObserver
    {
        private IMediator _mediator { get; }
        public CoreDomainEventObserver(IMediator mediator) => (_mediator) = (mediator);

        public override void OnNext(DomainEvent value)
        {
            if (value is INotification notification)
            {
                _mediator.Publish(notification);
            }
            else
            {
                throw new InvalidOperationException($"The event {value} was not an {nameof(INotification)} type");
            }
        }
        
        public override void OnCompleted()
        {
            this.Dispose();
        }

        public override void OnError(Exception error)
        {
            this.Dispose();
        }
    }
}