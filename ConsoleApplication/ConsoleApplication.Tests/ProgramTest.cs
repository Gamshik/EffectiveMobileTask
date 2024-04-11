using ConsoleApplication.Comparers;
using ConsoleApplication.Options;
using ConsoleApplication.Tests.DataWrappers;
using System.Net;

namespace ConsoleApplication.Tests
{
    public class ProgramTest
    {
        private IPAddressComparer _comparer;

        [SetUp]
        public void SetUp()
        {
            _comparer = new IPAddressComparer();
        }
        [TestCaseSource(nameof(GetDictionaryIpAddressCountTestWrappers))]
        public void DictionaryIpAddressCountByOptionsTest(DictionaryIpAddressCountTestWrapper wrapper)
        {
            var actual = Program.GetDictionaryIpAddressCountByOptions(wrapper.Logs, wrapper.Options);

            Assert.That(actual.Count, Is.EqualTo(wrapper.Expectation.Count));

            foreach (var key in wrapper.Expectation.Keys)
            {
                Assert.True(actual.ContainsKey(key));
                Assert.That(actual[key], Is.EqualTo(wrapper.Expectation[key]));
            }
        }
        public static IEnumerable<DictionaryIpAddressCountTestWrapper> GetDictionaryIpAddressCountTestWrappers()
        {
            yield return new DictionaryIpAddressCountTestWrapper
            {
                Logs =
                [
                    "255.0.1.1:2024-01-01 10:00:00",
                    "192.168.1.1:2024-01-01 10:01:00",
                    "192.168.0.1:2024-01-02 10:00:00",
                    "10.0.0.1:2024-01-02 10:20:00",
                    "192.168.1.1:2024-01-02 10:40:00",
                    "172.16.0.0:2024-01-03 11:00:00",
                    "255.0.1.1:2024-01-03 11:20:00",
                    "10.0.0.1:2024-01-03 11:40:00",
                    "192.168.1.0:2024-01-04 11:00:00",
                    "172.16.0.1:2024-01-05 12:00:00",
                    "10.0.0.2:2024-01-05 12:20:00",
                    "10.0.0.1:2024-01-06 11:20:00",
                    "10.0.0.1:2024-01-06 11:21:00",
                    "192.168.1.1:2024-01-07 11:30:00",
                    "192.168.1.1:2024-01-07 11:31:00",
                    "255.0.1.1:2024-01-07 11:35:00",
                    "172.16.0.1:2024-01-08 11:40:00",
                    "10.0.0.1:2024-01-08 11:45:00",
                    "192.168.1.0:2024-01-08 11:50:00"
                ],
                Options = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4)),
                Expectation = new Dictionary<IPAddress, int>
                {
                    { new IPAddress(new byte[] { 192, 168, 0, 1 }), 1 },
                    { new IPAddress(new byte[] { 10, 0, 0, 1 }), 2 },
                    { new IPAddress(new byte[] { 192, 168, 1, 1 }), 1 },
                    { new IPAddress(new byte[] { 172, 16, 0, 0 }), 1 },
                    { new IPAddress(new byte[] { 255, 0, 1, 1 }), 1 }
                }
            };
            yield return new DictionaryIpAddressCountTestWrapper
            {
                Logs =
                [
                    "255.0.1.1:2024-01-01 10:00:00",
                    "192.168.1.1:2024-01-01 10:01:00",
                    "192.168.0.1:2024-01-02 10:00:00",
                    "10.0.0.1:2024-01-02 10:20:00",
                    "192.168.1.1:2024-01-02 10:40:00",
                    "172.16.0.0:2024-01-03 11:00:00",
                    "255.0.1.1:2024-01-03 11:20:00",
                    "10.0.0.1:2024-01-03 11:40:00",
                    "192.168.1.0:2024-01-04 11:00:00",
                    "172.16.0.1:2024-01-05 12:00:00",
                    "10.0.0.2:2024-01-05 12:20:00",
                    "10.0.0.1:2024-01-06 11:20:00",
                    "10.0.0.1:2024-01-06 11:21:00",
                    "192.168.1.1:2024-01-07 11:30:00",
                    "192.168.1.1:2024-01-07 11:31:00",
                    "255.0.1.1:2024-01-07 11:35:00",
                    "172.16.0.1:2024-01-08 11:40:00",
                    "10.0.0.1:2024-01-08 11:45:00",
                    "192.168.1.0:2024-01-08 11:50:00"
                ],
                Options = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 5), new DateTime(2024, 1, 8)),
                Expectation = new Dictionary<IPAddress, int>
                {
                    { new IPAddress(new byte[] { 172, 16, 0, 1 }), 1 },
                    { new IPAddress(new byte[] { 10, 0, 0, 2 }), 1 },
                    { new IPAddress(new byte[] { 10, 0, 0, 1 }), 2 },
                    { new IPAddress(new byte[] { 192, 168, 1, 1 }), 2 },
                    { new IPAddress(new byte[] { 255, 0, 1, 1 }), 1 }
                }
            };
            yield return new DictionaryIpAddressCountTestWrapper
            {
                Logs =
                [
                    "255.0.1.1:2024-01-01 10:00:00",
                    "192.168.1.1:2024-01-01 10:01:00",
                    "192.168.0.1:2024-01-02 10:00:00",
                    "10.0.0.1:2024-01-02 10:20:00",
                    "192.168.1.1:2024-01-02 10:40:00",
                    "172.16.0.0:2024-01-03 11:00:00",
                    "255.0.1.1:2024-01-03 11:20:00",
                    "10.0.0.1:2024-01-03 11:40:00",
                    "192.168.1.0:2024-01-04 11:00:00",
                    "172.16.0.1:2024-01-05 12:00:00",
                    "10.0.0.2:2024-01-05 12:20:00",
                    "10.0.0.1:2024-01-06 11:20:00",
                    "10.0.0.1:2024-01-06 11:21:00",
                    "192.168.1.1:2024-01-07 11:30:00",
                    "192.168.1.1:2024-01-07 11:31:00",
                    "255.0.1.1:2024-01-07 11:35:00",
                    "172.16.0.1:2024-01-08 11:40:00",
                    "10.0.0.1:2024-01-08 11:45:00",
                    "192.168.1.0:2024-01-08 11:50:00"
                ],
                Options = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 5), new DateTime(2024, 1, 8), new IPAddress(new byte[] { 192, 168, 1, 1 })),
                Expectation = new Dictionary<IPAddress, int>
                {
                    { new IPAddress(new byte[] { 192, 168, 1, 1 }), 2 },
                    { new IPAddress(new byte[] { 255, 0, 1, 1 }), 1 }
                }
            };
            yield return new DictionaryIpAddressCountTestWrapper
            {
                Logs =
                [
                    "255.0.1.1:2024-01-01 10:00:00",
                    "192.168.1.1:2024-01-01 10:01:00",
                    "192.168.0.1:2024-01-02 10:00:00",
                    "10.0.0.1:2024-01-02 10:20:00",
                    "192.168.1.1:2024-01-02 10:40:00",
                    "172.16.0.0:2024-01-03 11:00:00",
                    "255.0.1.1:2024-01-03 11:20:00",
                    "10.0.0.1:2024-01-03 11:40:00",
                    "192.168.1.0:2024-01-04 11:00:00",
                    "172.16.0.1:2024-01-05 12:00:00",
                    "10.0.0.2:2024-01-05 12:20:00",
                    "10.0.0.1:2024-01-06 11:20:00",
                    "10.0.0.1:2024-01-06 11:21:00",
                    "192.168.1.1:2024-01-07 11:30:00",
                    "192.168.1.1:2024-01-07 11:31:00",
                    "255.0.1.1:2024-01-07 11:35:00",
                    "172.16.0.1:2024-01-08 11:40:00",
                    "10.0.0.1:2024-01-08 11:45:00",
                    "192.168.1.0:2024-01-08 11:50:00"
                ],
                Options = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 5), new DateTime(2024, 1, 8), new IPAddress(new byte[] { 172, 16, 0, 0 })),
                Expectation = new Dictionary<IPAddress, int>
                {
                    { new IPAddress(new byte[] { 172, 16, 0, 1 }), 1 },
                    { new IPAddress(new byte[] { 192, 168, 1, 1 }), 2 },
                    { new IPAddress(new byte[] { 255, 0, 1, 1 }), 1 }
                }
            };
            yield return new DictionaryIpAddressCountTestWrapper
            {
                Logs =
                [
                    "255.0.1.1:2024-01-01 10:00:00",
                    "192.168.1.1:2024-01-01 10:01:00",
                    "192.168.0.1:2024-01-02 10:00:00",
                    "10.0.0.1:2024-01-02 10:20:00",
                    "192.168.1.1:2024-01-02 10:40:00",
                    "172.16.0.0:2024-01-03 11:00:00",
                    "255.0.1.1:2024-01-03 11:20:00",
                    "10.0.0.1:2024-01-03 11:40:00",
                    "192.168.1.0:2024-01-04 11:00:00",
                    "172.16.0.1:2024-01-05 12:00:00",
                    "10.0.0.2:2024-01-05 12:20:00",
                    "10.0.0.1:2024-01-06 11:20:00",
                    "10.0.0.1:2024-01-06 11:21:00",
                    "192.168.1.1:2024-01-07 11:30:00",
                    "192.168.1.1:2024-01-07 11:31:00",
                    "255.0.1.1:2024-01-07 11:35:00",
                    "172.16.0.1:2024-01-08 11:40:00",
                    "10.0.0.1:2024-01-08 11:45:00",
                    "192.168.1.0:2024-01-08 11:50:00"
                ],
                Options = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 5), new DateTime(2024, 1, 8), new IPAddress(new byte[] { 10, 0, 0, 2 }), 191),
                Expectation = new Dictionary<IPAddress, int>
                {
                    { new IPAddress(new byte[] { 172, 16, 0, 1 }), 1 },
                    { new IPAddress(new byte[] { 10, 0, 0, 2 }), 1 }
                }
            };
            yield return new DictionaryIpAddressCountTestWrapper
            {
                Logs =
                [
                    "255.0.1.1:2024-01-01 10:00:00",
                    "192.168.1.1:2024-01-01 10:01:00",
                    "192.168.0.1:2024-01-02 10:00:00",
                    "10.0.0.1:2024-01-02 10:20:00",
                    "192.168.1.1:2024-01-02 10:40:00",
                    "172.16.0.0:2024-01-03 11:00:00",
                    "255.0.1.1:2024-01-03 11:20:00",
                    "10.0.0.1:2024-01-03 11:40:00",
                    "192.168.1.0:2024-01-04 11:00:00",
                    "172.16.0.1:2024-01-05 12:00:00",
                    "10.0.0.2:2024-01-05 12:20:00",
                    "10.0.0.1:2024-01-06 11:20:00",
                    "10.0.0.1:2024-01-06 11:21:00",
                    "192.168.1.1:2024-01-07 11:30:00",
                    "192.168.1.1:2024-01-07 11:31:00",
                    "255.0.1.1:2024-01-07 11:35:00",
                    "172.16.0.1:2024-01-08 11:40:00",
                    "10.0.0.1:2024-01-08 11:45:00",
                    "192.168.1.0:2024-01-08 11:50:00"
                ],
                Options = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 5), new DateTime(2024, 1, 8), new IPAddress(new byte[] { 10, 0, 0, 3 }), 223),
                Expectation = new Dictionary<IPAddress, int>
                {
                    { new IPAddress(new byte[] { 172, 16, 0, 1 }), 1 },
                    { new IPAddress(new byte[] { 192, 168, 1, 1 }), 2 }
                }
            };
        }
    }
}