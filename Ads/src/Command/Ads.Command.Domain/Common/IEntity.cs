using Ads.Common;

namespace Ads.Command.Domain.Common;

public interface IEntity
{
    void Handle(BaseDomainEvent @event);
}