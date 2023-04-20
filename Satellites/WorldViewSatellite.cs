namespace SatellitesDesignPatterns
{
    class WorldViewSatellite
    {
        private string _location;
        public string Location { get; }
        private int[] _data;
        public string Data
        {
            get
            {
                if (_data == null) return "";
                return string.Join("", _data.Select(c => (c >= 'a' ? (char)(c - 'a' + 'A') : (char)(c + 'a' - 'A')).ToString()));
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
