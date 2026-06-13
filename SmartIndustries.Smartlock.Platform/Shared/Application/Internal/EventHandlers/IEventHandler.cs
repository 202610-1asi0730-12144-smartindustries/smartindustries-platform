using Cortex.Mediator.Notifications;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.Events;

namespace SmartIndustries.Smartlock.Platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
