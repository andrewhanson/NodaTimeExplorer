using System;
using NodaTime;

namespace NodaTimeExplorer.Models
{
    public class User
    {
        public String Name {get;set;}
        public LocalDate Birthday{get;set;}
        public LocalTime? ShiftStartTime{get;set;}
        public String TimezoneString{get;set;}

        public Instant CreatedDateTime{get;set;}
        public Instant LastModifiedDateTime{get;set;}
    }
}