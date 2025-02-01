namespace CrossCutting.Utils;

public static class ScheduleDictionary
{
    private static readonly string[] _twentyMinuteBreak = [
            "08:00", "08:20", "08:40",
            "09:00", "09:20", "09:40",
            "10:00", "10:20", "10:40",
            "11:00", "11:20", "11:40",
            "12:00", "12:20", "12:40",
            "13:00", "13:20", "13:40",
            "14:00", "14:20", "14:40",
            "15:00", "15:20", "15:40",
            "16:00", "16:20", "16:40",
            "17:00", "17:20", "17:40",
            "18:00", "18:20", "18:40"
    ];

    private static readonly Dictionary<DayOfWeek, string[]> _scheduleBase = new()
    {
        [DayOfWeek.Monday] = _twentyMinuteBreak,
        [DayOfWeek.Tuesday] = _twentyMinuteBreak,
        [DayOfWeek.Wednesday] = _twentyMinuteBreak,
        [DayOfWeek.Thursday] = _twentyMinuteBreak,
        [DayOfWeek.Friday] = _twentyMinuteBreak
    };

    public static string[] GetSchedule(DayOfWeek dayOfWeek)
    {
        return _scheduleBase[dayOfWeek];
    }
}
