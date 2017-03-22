using System;

namespace DotLms.Common
{
    public class DateTimeVariables
    {
        public static DateTime OneMinuteFromUtcNow { get; } = DateTime.UtcNow.AddMinutes(1);
        
        public static DateTime FiveMinutesFromUtcNow { get; } = DateTime.UtcNow.AddMinutes(5);

        public static DateTime TenMinutesFromUtcNow { get; } = DateTime.UtcNow.AddMinutes(10);

        public static DateTime FifteenMinutesFromUtcNow { get; } = DateTime.UtcNow.AddMinutes(15);

        public static DateTime TwentyMinutesFromUtcNow { get; } = DateTime.UtcNow.AddMinutes(20);

        public static DateTime SixtyMinutesFromUtcNow { get; } = DateTime.UtcNow.AddMinutes(60);
    }
}