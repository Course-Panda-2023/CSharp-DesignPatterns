namespace DesignPatterns;

public class Customer: TimeSubscriber
{
    private readonly List<(string, string, int)> _requests = new List<(string, string, int)>();
    private readonly ControlUnit _controlUnit;
    
    public Customer(string filepath, ControlUnit controlUnit)
    {
        _controlUnit = controlUnit;
        
        string[] lines = System.IO.File.ReadAllLines(filepath);

        for(int i=0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }

            if ((i - 1) % 3 == 2)
            {
                _requests.Add(
                    (
                        lines[i-2], 
                        lines[i-1], 
                        Convert.ToInt32(lines[i])
                    )
                );
            }
        }
    }
    
    public void MakeRequest(RequestType requestType, string location)
    {
        _controlUnit.GetRequest(requestType, location);
    }
    public void OnTimeStamp(int time)
    {
        foreach ((string, string, int) request in _requests)
        {
            if (request.Item3 == time)
            {
                MakeRequest(
                    request.Item1 == "P" ? RequestType.Photo : RequestType.Weather,
                    request.Item2
                );
            }
        }
    }
}