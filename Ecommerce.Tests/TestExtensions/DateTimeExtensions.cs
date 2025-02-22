namespace Ecommerce.Tests.TestExtensions;

public static class DateTimeExtensions
{
    public static DateTime ReduceMillisecondPrecision(this DateTime dateTime)
    {
        var milliseconds = dateTime.Millisecond;
        return new DateTime(dateTime.Ticks - dateTime.Ticks % TimeSpan.TicksPerSecond, dateTime.Kind).AddMilliseconds(milliseconds);
    }

    public static DateTime ClearMilliseconds(this DateTime dateTime)
        => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0, DateTimeKind.Utc);
}