using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Partners;

public sealed class PartnerOpeningHourEntity : Entity<OpeningHourId>
{
    private PartnerOpeningHourEntity(OpeningHourId id, DayOfWeek day, string start, string end, bool isClosed, PartnerId partnerId)
        : base(id)
    {
        Day = day;
        Start = start;
        End = end;
        IsClosed = isClosed;
        PartnerId = partnerId;
    }

    private PartnerOpeningHourEntity() { }

    public DayOfWeek Day { get; private set; }
    public string Start { get; private set; } = string.Empty; // HH:mm format
    public string End { get; private set; } = string.Empty;   // HH:mm format
    public bool IsClosed { get; private set; }
    
    public PartnerId PartnerId { get; private set; } = default!;
    public PartnerEntity? Partner { get; private set; }

    public static PartnerOpeningHourEntity Create(DayOfWeek day, string start, string end, bool isClosed, PartnerId partnerId)
    {
        return new PartnerOpeningHourEntity(OpeningHourId.New(), day, start, end, isClosed, partnerId);
    }
}
