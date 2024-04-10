namespace UssJuniorTest.Core.Utilities
{
    public class OptionalParameters
    {
        public string? DriverNameToFilterBy { get; set; }

        public string? CarModelToFilterBy { get; set; }

        public bool MustSortByDriver { get; set; }

        public bool MustSortByCar { get; set; }

        public OptionalParameters() { }
    }
}
