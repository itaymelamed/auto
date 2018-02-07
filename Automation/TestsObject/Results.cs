namespace Automation.TestsObjects
{
    public class Results
    {
        public int Passed { get; set; }
        public int Failed { get; set; }
        public int SentToHub { get; set; }
        public int Running { get; set; }

        public Results()
        {
            Passed = 0;
            Failed = 0;
            SentToHub = 0;
            Running = 0;
        }
    }
}