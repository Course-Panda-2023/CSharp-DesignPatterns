namespace Satellite.Satellites
{
    internal class WeatherSatellite : AbstractSatellite
    {

        public override void SetTargetLocation(string location)
        {

            this.Location = location;
            SampleData();
        }


        protected override void SampleData()
        {
            Data(Location.ToUpper());
        }
    }
}
