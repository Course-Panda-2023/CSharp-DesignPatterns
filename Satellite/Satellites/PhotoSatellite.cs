namespace Satellite.Satellites
{
    internal class PhotoSatellite : AbstractSatellite
    {

        public override void SetTargetLocation(string location)
        {
            if (this.Location != null)
            {
                LocationsQueue.Enqueue(location);
                return;
            }
            this.Location = location;
            SampleData();
        }

        protected override void SampleData()
        {
            Data(Location.ToLower());
        }
    }
}
