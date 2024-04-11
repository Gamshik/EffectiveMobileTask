using ConsoleApplication.Comparers;
using System.Net;

namespace ConsoleApplication.Tests
{
    public class IPAddressComparerTest
    {
        private IPAddressComparer _comparer;

        [SetUp]
        public void SetUp()
        {
            _comparer = new IPAddressComparer();
        }
        [TestCaseSource(nameof(GetIpAddressesForGreaterThanZeroTest))]
        public void GreaterThanZeroTest(IPAddress firstAddress, IPAddress secondAddress)
        {
            var result = _comparer.Compare(firstAddress, secondAddress);

            Assert.Greater(result, 0);
        }
        public static IEnumerable<IPAddress[]> GetIpAddressesForGreaterThanZeroTest()
        {
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                new IPAddress(new byte[] { 10, 0, 0, 0 })
            };
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 128, 128, 128, 128 }),
                new IPAddress(new byte[] { 127, 128, 128, 128 })
            };
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 255, 255, 255, 255 }),
                new IPAddress(new byte[] { 255, 254, 254, 254 })
            };
        }
        [TestCaseSource(nameof(GetIpAddressesForLessThanZeroTest))]
        public void LessThanZeroTest(IPAddress firstAddress, IPAddress secondAddress)
        {
            var result = _comparer.Compare(firstAddress, secondAddress);

            Assert.Less(result, 0);
        }
        public static IEnumerable<IPAddress[]> GetIpAddressesForLessThanZeroTest()
        {
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 127, 0, 0, 0 }),
                new IPAddress(new byte[] { 127, 0, 1, 0 })
            };
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 176, 140, 8, 8 }),
                new IPAddress(new byte[] { 192, 0, 0, 1})
            };
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 255, 0, 255, 255 }),
                new IPAddress(new byte[] { 255, 254, 254, 254 })
            };
        }
        [TestCaseSource(nameof(GetIpAddressesForEqualZeroTest))]
        public void EqualZeroTest(IPAddress firstAddress, IPAddress secondAddress)
        {
            var result = _comparer.Compare(firstAddress, secondAddress);

            Assert.Zero(result);
        }
        public static IEnumerable<IPAddress[]> GetIpAddressesForEqualZeroTest()
        {
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 127, 0, 0, 0 }),
                new IPAddress(new byte[] { 127, 0, 0, 0 })
            };
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 192, 0, 1, 8 }),
                new IPAddress(new byte[] { 192, 0, 1, 8 })
            };
            yield return new IPAddress[2]
            {
                new IPAddress(new byte[] { 255, 255, 255, 255 }),
                new IPAddress(new byte[] { 255, 255, 255, 255 })
            };
        }
    }
}