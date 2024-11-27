using Contracts.Domains.Interfaces;

namespace Contracts.Domains;
// Abstract class: not allow class create new instance
public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public TKey Id { get; set; } 
}