using ConsoleApplication.Options;
using System.Net;

namespace ConsoleApplication.Tests.DataWrappers
{
    public class DictionaryIpAddressCountTestWrapper
    {
        public string[] Logs { get; set; }
        public ProgramOption Options { get; set; }
        public Dictionary<IPAddress, int> Expectation { get; set; }
    }
}
