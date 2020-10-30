using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTimeExplorer.Models;
using NodaTimeExplorer.Services;

namespace NodaTimeExplorer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var timezone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("US/Pacific");
            
            var clock = new ClockService(timezone, null);
            var timeTravelClock = new ClockService(timezone, new LocalDate(1999,12,31).AtMidnight());

            var user = new User(){
                Name = "John Doe",
                Birthday = new LocalDate(1987,5,2),
                ShiftStartTime = new LocalTime(6,0),
                TimezoneString = "US/Pacific",
                CreatedDateTime = clock.Now.Minus(Duration.FromDays(54)),
                LastModifiedDateTime = clock.Now.Minus(Duration.FromMinutes(10))
            };
            
            return Ok( new {
                            user, 
                            Now = clock.Now, 
                            LocalNow = clock.LocalNow, 
                            TimeTravelNow = timeTravelClock.Now, 
                            TimeTravelLocalNow = timeTravelClock.LocalNow 
                        });
        }
    }
}
