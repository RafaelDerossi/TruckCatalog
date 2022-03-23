using System;

namespace TruckCatalog.App.Core.Helpers
{
    public static class BrasiliaDateTime
    {
        public static DateTime Get()
        {
           return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Timezone.GetTimezone());
        }
        
        public static DateTime Set(DateTime data)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(data.ToUniversalTime(), Timezone.GetTimezone());
        }
    }
}