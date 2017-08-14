
using System;
using NodaTime;
using NodaTime.TimeZones;
using NodaTimeExplorer.Models;

namespace NodaTimeExplorer.Services{
    public class ClockService : IClockService
    {
        public DateTimeZone TimeZone{get; protected set;}
        public LocalDateTime? OffsetDateTime{get; protected set;}

        private IClock _clock;

        public ClockService(DateTimeZone timezone, LocalDateTime? offsetDateTime){
            OffsetDateTime = offsetDateTime;
            TimeZone = timezone;
            _clock = SystemClock.Instance;
        }

        public ClockService(DateTimeZone timezone, LocalDateTime? offsetDateTime, IClock clock){
            OffsetDateTime = offsetDateTime;
            TimeZone = timezone;
            _clock = clock;
        }

        public Instant Now {
            get
            {
                if(OffsetDateTime == null){
                    return _clock.GetCurrentInstant();
                }
                /*
                * if we have 'time travelled' then we need to figure out the target time in the users local timezone and then convert it to an instant
                 */
                var offsetLocalTime = new LocalDateTime(OffsetDateTime.Value.Year, OffsetDateTime.Value.Month, OffsetDateTime.Value.Day, OffsetDateTime.Value.Hour, OffsetDateTime.Value.Minute);
                return offsetLocalTime.InZoneLeniently(TimeZone).ToInstant();
            }
        }

        public LocalDateTime LocalNow 
        {
            get{
                return Now.InZone(TimeZone).LocalDateTime;
            }
        }

        public Instant ToInstant(LocalDateTime local)
        {
            return local.InZone(TimeZone, Resolvers.LenientResolver).ToInstant();
        }

        public LocalDateTime ToLocal(Instant instant)
        {
            return instant.InZone(TimeZone).LocalDateTime;
        }
    }
}