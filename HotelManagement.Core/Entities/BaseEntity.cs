using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Core.Entities;
public class BaseEntity
{
    public int Id { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    [NotMapped]
    private readonly List<IEvent> events = new();
    [NotMapped]
    public IReadOnlyCollection<IEvent> _events => events;
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    
    public bool IsDeleted { get; set; } = false;
    public void AddEvent(IEvent @event)
    {
        events.Add(@event);
    }

    public void ClearEvent(IEvent @event) 
    {
        events.Clear();
    }
}
