namespace Satellite.Satellites
{
    class WorldViewSatellite
    {
        private string? _location;

        public string Location
        {
            set { _location = value; }
            get
            {
                return _location;
            }
        }
        private int[] _data;
        public int[] Data
        {
            get
            {
                return _data;
            }
        }
        private bool _isDataSet;
        public int[] View()
        {
            var d = _data;
            _data = null;
            _location = null;
            _isDataSet = false;

            return d;
        }
        public bool SetLocation(string location)
        {
            if (_isDataSet)
            {
                return false;
            }
            else
            {
                _isDataSet = true;
                _location = location;
                return true;
            }
        }
        public void SampleData()
        {
            _data = _location.ToCharArray().Select((c) => c >= 'a' ? c + 'a' - 'A' : c).ToArray();
        }
    }
}
