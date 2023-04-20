namespace SatellitesDesignPatterns
{
    internal static class ControlPanelSingleton
    {
        private static ControlPanel cont;
        public static ControlPanel GetInstance()
        {
            if (cont is null)
            {
                cont = new ControlPanel();
            }
            return cont;
        }
    }
}
