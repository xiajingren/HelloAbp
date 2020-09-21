namespace Xhznl.HelloAbp.Jobs.Statistics
{
    public class TrafficArgs
    {
        public TrafficArgs(int count)
        {
            ErrorCount = count;
        }

        public int ErrorCount { get; set; }
    }
}