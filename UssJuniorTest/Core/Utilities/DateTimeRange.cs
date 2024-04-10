namespace UssJuniorTest.Core.Utilities
{
    public class DateTimeRange
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public DateTimeRange() { }

        public bool IsFilled() 
        {
            return Start != null && End != null;
        }
    }
}
