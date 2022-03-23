using System;

namespace TruckCatalog.App.Core.Helpers
{
    public static class Timezone
    {
        public static TimeZoneInfo GetTimezone()
        {
            TimeZoneInfo cetZone;

            try
            {
                cetZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            }
            catch
            {
                cetZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
            }

            return cetZone;
        }
    }
}