namespace DesignPatterns;

public abstract class PhotoSatellite : Satellite
{ 
    protected PhotoSatellite(string id, int startTime, int elapseTime) : base(id, startTime, elapseTime)
    {
        
    }
}