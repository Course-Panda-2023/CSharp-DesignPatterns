namespace DesignPatterns;

public abstract class Satellite: TimeSubscriber
{
    private readonly string _id;
    public readonly int StartTime;
    public readonly int ElapseTime;

    public string Id
    {
        get
        {
            return _id;
        }
    }

    protected Satellite(string id, int startTime, int elapseTime)
    {
        _id = id;
        StartTime = startTime;
        ElapseTime = elapseTime;
    }
    
    public abstract string GetData();
    public abstract void SetCommand(string location);
    protected abstract void SampleData();
    public abstract string? GetCommand();
    public abstract bool WasDataSampled();

    public void OnTimeStamp(int time)
    {
        if ((GetCommand() == null) || WasDataSampled())
        {
            // no command is waiting on this satellite to be performed
            return;
        }

        if ((time - StartTime) % ElapseTime == Convert.ToInt32(ElapseTime / 2))
        {
            SampleData();
        }
    }
}