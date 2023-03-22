namespace DesignPatternsTamas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string requestPath = "C:\\Users\\dan-client\\Source\\Repos\\DesignPatternsTamas\\targil\\data\\4R.txt";
            string satellitePath = "C:\\Users\\dan-client\\Source\\Repos\\DesignPatternsTamas\\targil\\data\\4S.txt";
            Time.Instance.Run(satellitePath, requestPath);
        }
    }
}