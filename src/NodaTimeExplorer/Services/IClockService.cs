using System;
using NodaTime;

namespace NodaTimeExplorer.Services
{
    public interface IClockService
    {
         DateTimeZone TimeZone{get;}
         LocalDateTime? OffsetDateTime{get;}
         Instant Now{get;}
         LocalDateTime LocalNow{get;}
         Instant ToInstant(LocalDateTime local);
         LocalDateTime ToLocal(Instant instant);
    }
}