using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace DesignPatterns;

public class Time
{
    private static int _time = 0;
    private static readonly object MLocker = new Object();
    private static readonly List<TimeSubscriber> _subscribers = new List<TimeSubscriber>();
    private static readonly int timeUnitMilliSeconds = 1000;

    public static int Value
    {
        get
        {
            lock (MLocker)
            {
                return _time;
            }
        }
    }

    private static void Increment()
    {
        lock (MLocker)
        {
            _time++;
        }
    }

    public static void Attach(TimeSubscriber s)
    {
        _subscribers.Add(s);
    }

    public static void Remove(TimeSubscriber s)
    {
        _subscribers.Remove(s);
    }

    public static void Notify()
    {
        foreach (TimeSubscriber s in _subscribers)
        {
            s.OnTimeStamp(Value);
        }
    }
    
    public static void Run(bool verboseTime = true)
    {
        while (true)
        {
            if (verboseTime)
            {
                Console.WriteLine($"-------- time: {Value} --------");
            }
            Notify();
            Increment();
            Thread.Sleep(timeUnitMilliSeconds);
        }
    }
}