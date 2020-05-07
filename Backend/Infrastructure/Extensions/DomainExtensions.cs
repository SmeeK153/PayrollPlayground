using System;
using System.Reflection;
using Domain.Events;
using Foundations.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static partial class DomainExtensions
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddMediatR(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(CoreDomainEvent)));
                // Add additional infrastructure projects' references here

            services.AddSingleton(serviceProvider =>
                new MediatorDomainEventObserver(serviceProvider.GetService<IMediator>()));

            return services;
        }

        public static IApplicationBuilder UseDomainEvents(this IApplicationBuilder builder)
        {
            builder.ApplicationServices.GetService<MediatorDomainEventObserver>();
            return builder;
        }
    }
    
    public class MediatorDomainEventObserver : DomainEventObserver
    {
        private IMediator _mediator { get; }
        public MediatorDomainEventObserver(IMediator mediator) => (_mediator) = (mediator);

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