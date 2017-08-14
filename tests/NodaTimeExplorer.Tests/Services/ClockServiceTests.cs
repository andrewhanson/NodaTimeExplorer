using NodaTime;
using NodaTimeExplorer.Services;
using NodaTime.Testing;
using Xunit;
using NodaTime.Text;

namespace NodaTimeExplorer.Tests.Services
{
    public class ClockServiceTests
    {
        DateTimeZone _timeZone;
        IClock _fakeClock;

        Instant _now;

        public ClockServiceTests(){
            _timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("US/Pacific");
            _now = Instant.FromUtc(2017, 2, 14, 0, 0, 0);
            _fakeClock = new FakeClock(_now);
        }

        [Fact]
         public void CurrentInstantNoShift()
        {
            var clock = new ClockService(_timeZone, null, _fakeClock);
            Assert.Equal(_now,clock.Now);
        }

        [Fact]
        public void CurrentInstantShifted()
        {
            var offset = new LocalDateTime(2017,2,14,16,40,0);
            var clock = new ClockService(_timeZone, offset, _fakeClock);

            Assert.Equal("2017-02-15T00:40:00Z", clock.Now.ToString());
            Assert.Equal(offset, clock.LocalNow);
        }

        [Theory]
        [InlineData("2017-02-27T16:00:00", "US/Pacific","2017-02-28T00:00:00Z")]
        [InlineData("2017-02-27T19:00:00", "US/Eastern", "2017-02-28T00:00:00Z")]
        [InlineData("2017-07-27T19:00:00", "US/Eastern", "2017-07-27T23:00:00Z")]
        public void UserOffsetDate(string userOffsetDateTime,  string timezoneCode, string expectedTimeInUtc )
        {
            var offset = LocalDateTimePattern.ExtendedIso.Parse(userOffsetDateTime).Value;
            var tz = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timezoneCode);
            var clock = new ClockService(tz, offset, _fakeClock);

            Assert.Equal(expectedTimeInUtc, clock.Now.ToString());
        }
    }
}