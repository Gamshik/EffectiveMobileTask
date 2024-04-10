namespace ConsoleApplication.Tests.DataWrappers
{
    public class DataWrapper<T, TExp>
    {
        public T Value { get; set; }
        public TExp Expectation { get; set; }
    }
}
