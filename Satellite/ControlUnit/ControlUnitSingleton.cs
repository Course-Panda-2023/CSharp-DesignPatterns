namespace Satellite.ControlUnit
{
    internal static class ControlUnitSingleton
    {
        private static ControlUnit? ControlUnit;

        public static ControlUnit GetInstance()
        {
            if (ControlUnit == null)
            {
                ControlUnit = new ControlUnit();
            }

            return ControlUnit;
        }
    }
}
