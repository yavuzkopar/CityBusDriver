[System.Serializable]
public struct Clock
{
    public int hours;
    public int minutes;

    public Clock(int hours, int minutes)
    {
        this.hours =hours+ minutes / 60;
        this.minutes = minutes % 60;
    }
    public Clock GetClock()
    {
        int hour = hours+ minutes / 60;
        int minute = minutes % 60;
        return new Clock(hour, minute);
    }
    public override string ToString()
    {
        return hours.ToString() + ":" + minutes.ToString();
    }
    public static Clock operator +(Clock a, Clock b) => new Clock(a.hours + b.hours, a.minutes + b.minutes);
    public static bool operator >(Clock a, Clock b) => a.hours > b.hours || (a.hours == b.hours && a.minutes > b.minutes);
    public static bool operator <(Clock a, Clock b) => a.hours < b.hours || (a.hours == b.hours && a.minutes < b.minutes);
}
