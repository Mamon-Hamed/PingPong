using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Partners;

public sealed class PartnerOpeningHour : Entity<OpeningHourId>
{
    private PartnerOpeningHour(OpeningHourId id, DayOfWeek day, string start, string end, bool isClosed, PartnerId partnerId)
        : base(id)
    {
        Day = day;
        Start = start;
        End = end;
        IsClosed = isClosed;
        PartnerId = partnerId;
    }

    private PartnerOpeningHour() { }

    public DayOfWeek Day { get; private set; }
    public string Start { get; private set; } = string.Empty; // HH:mm format
    public string End { get; private set; } = string.Empty;   // HH:mm format
    public bool IsClosed { get; private set; }
    
    public PartnerId PartnerId { get; private set; } = default!;
    public PartnerEntity? Partner { get; private set; }

    public static PartnerOpeningHour Create(DayOfWeek day, string start, string end, bool isClosed, PartnerId partnerId)
    {
        return new PartnerOpeningHour(OpeningHourId.New(), day, start, end, isClosed, partnerId);
    }
}
