using ConsoleApplication.Options;
using ConsoleApplication.Tests.DataWrappers;
using System.Net;

namespace ConsoleApplication.Tests
{
    public class ProgramOptionTest
    {
        [TestCaseSource(nameof(GetDataForIsValidTest))]
        public void IsValidTest(DataWrapper<ProgramOption, bool> data)
        {
            var actual = data.Value.IsValid();

            Assert.That(actual, Is.EqualTo(data.Expectation));
        }
        public static IEnumerable<DataWrapper<ProgramOption, bool>> GetDataForIsValidTest()
        {
            yield return new DataWrapper<ProgramOption, bool>
            {
                Value = new ProgramOption
                {
                    LogFile = "pathToLogFile",
                    OutputFile = "outputFile",
                    TimeStart = new DateTime(2024, 1, 10),
                    TimeEnd = new DateTime(2024, 1, 11)
                },
                Expectation = true
            };
            yield return new DataWrapper<ProgramOption, bool>
            {
                Value = new ProgramOption
                {
                    LogFile = "pathToLogFile",
                    OutputFile = "outputFile",
                    TimeStart = new DateTime(2024, 1, 10),
                    TimeEnd = new DateTime(2024, 1, 11),
                    IpAddressStart = new IPAddress(new byte[] { 127, 0, 0, 0 })
                },
                Expectation = true
            };
            yield return new DataWrapper<ProgramOption, bool>
            {
                Value = new ProgramOption
                {
                    LogFile = "pathToLogFile",
                    OutputFile = "outputFile",
                    TimeStart = new DateTime(2024, 1, 10),
                    TimeEnd = new DateTime(2024, 1, 11),
                    IpAddressStart = new IPAddress(new byte[] { 127, 0, 0, 0 }),
                    IpAddressEnd = 191
                },
                Expectation = true
            };
            yield return new DataWrapper<ProgramOption, bool>
            {
                Value = new ProgramOption
                {
                    LogFile = "pathToLogFile",
                    OutputFile = "outputFile",
                    TimeStart = new DateTime(2024, 1, 10)
                },
                Expectation = false
            };
            yield return new DataWrapper<ProgramOption, bool>
            {
                Value = new ProgramOption
                {
                    LogFile = "pathToLogFile",
                    OutputFile = "outputFile",
                    TimeEnd = new DateTime(2024, 1, 10)
                },
                Expectation = false
            };
            yield return new DataWrapper<ProgramOption, bool>
            {
                Value = new ProgramOption
                {
                    LogFile = "pathToLogFile",
                    TimeStart = new DateTime(2024, 1, 10),
                    TimeEnd = new DateTime(2024, 1, 11)
                },
                Expectation = false
            };
            yield return new DataWrapper<ProgramOption, bool>
            {
                Value = new ProgramOption
                {
                    OutputFile = "outputFile",
                    TimeStart = new DateTime(2024, 1, 10),
                    TimeEnd = new DateTime(2024, 1, 11)
                },
                Expectation = false
            };
        }
        [TestCaseSource(nameof(GetDataForEqualsTest))]
        public void EqualsTest(DataWrapper<ProgramOption[], bool> data)
        {
            var firstOptions = data.Value[0];
            var secondOptions = data.Value[1];

            var actual = firstOptions.Equals(secondOptions);

            Assert.That(actual, Is.EqualTo(data.Expectation));
        }
        public static IEnumerable<DataWrapper<ProgramOption[], bool>> GetDataForEqualsTest()
        {
            yield return new DataWrapper<ProgramOption[], bool>
            {
                Value =
                [
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4)),
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4))
                ],
                Expectation = true
            };
            yield return new DataWrapper<ProgramOption[], bool>
            {
                Value =
                [
                    new ProgramOption("pathToLogFile_test", "pathToOutputFile", new DateTime(2024, 1, 12), new DateTime(2024, 1, 24)),
                    new ProgramOption("pathToLogFile_test", "pathToOutputFile", new DateTime(2024, 1, 12), new DateTime(2024, 1, 24))
                ],
                Expectation = true
            };
            yield return new DataWrapper<ProgramOption[], bool>
            {
                Value =
                [
                    new ProgramOption("pathToLogFile_test", "pathToOutputFile", new DateTime(2024, 1, 11), new DateTime(2024, 1, 24)),
                    new ProgramOption("pathToLogFile_test", "pathToOutputFile", new DateTime(2024, 1, 12), new DateTime(2024, 1, 24))
                ],
                Expectation = false
            };
            yield return new DataWrapper<ProgramOption[], bool>
            {
                Value =
                [
                    new ProgramOption("pathToLogFile_test", "pathToOutputFile", new DateTime(2024, 1, 11), new DateTime(2024, 1, 24)),
                    new ProgramOption("pathToLogFile_", "pathToOutputFile", new DateTime(2024, 1, 12), new DateTime(2024, 1, 24))
                ],
                Expectation = false
            };
            yield return new DataWrapper<ProgramOption[], bool>
            {
                Value =
                [
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4)),
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 9, 123, 1 }))
                ],
                Expectation = false
            };
            yield return new DataWrapper<ProgramOption[], bool>
            {
                Value =
                [
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 9, 123, 1 })),
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 9, 123, 1 }))
                ],
                Expectation = true
            };
            yield return new DataWrapper<ProgramOption[], bool>
            {
                Value =
                [
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 0, 0, 0 }), 127),
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 9, 123, 1 }))
                ],
                Expectation = false
            };
            yield return new DataWrapper<ProgramOption[], bool>
            {
                Value =
                [
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 0, 0, 0 }), 223),
                    new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 0, 0, 0 }), 223)
                ],
                Expectation = true
            };
        }
        [TestCaseSource(nameof(GetDataForParseProgramOptionTest))]
        public void ParseProgramOptionTest(DataWrapper<string[], ProgramOption> data)
        {
            ProgramOption actual = ProgramOption.ParseOptions(data.Value);

            Assert.That(actual, Is.EqualTo(data.Expectation));
        }
        public static IEnumerable<DataWrapper<string[], ProgramOption>> GetDataForParseProgramOptionTest()
        {
            yield return new DataWrapper<string[], ProgramOption>
            {
                Value =
                [
                   "--file-log",
                    "pathToLogFile",
                    "--file-output",
                    "pathToOutputFile",
                    "--time-start",
                    "02.01.2024",
                    "--time-end",
                    "04.01.2024"
                ],
                Expectation = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4))
            };
            yield return new DataWrapper<string[], ProgramOption>
            {
                Value =
                [
                    "--file-log",
                    "pathToLogFile",
                    "--file-output",
                    "pathToOutputFile",
                    "--time-start",
                    "10.01.2024",
                    "--time-end",
                    "10.01.2024"
                ],
                Expectation = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 10), new DateTime(2024, 1, 10))
            };
            yield return new DataWrapper<string[], ProgramOption>
            {
                Value =
                [
                    "--file-log",
                   "pathToLogFile",
                   "--file-output",
                   "pathToOutputFile",
                   "--time-start",
                   "02.01.2024",
                   "--time-end",
                   "04.01.2024",
                   "--address-start",
                   "127.0.0.0"
                ],
                Expectation = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 0, 0, 0 }))
            };
            yield return new DataWrapper<string[], ProgramOption>
            {
                Value =
                [
                    "--file-log",
                   "pathToLogFile",
                   "--file-output",
                   "pathToOutputFile",
                   "--time-start",
                   "02.01.2024",
                   "--time-end",
                   "04.01.2024",
                   "--address-start",
                   "127.9.123.1"
                ],
                Expectation = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 9, 123, 1 }))
            };
            yield return new DataWrapper<string[], ProgramOption>
            {
                Value =
                [
                   "--file-log",
                   "pathToLogFile",
                   "--file-output",
                   "pathToOutputFile",
                   "--time-start",
                   "02.01.2024",
                   "--time-end",
                   "04.01.2024",
                   "--address-start",
                   "127.0.0.0",
                   "--address-mask",
                   "255.0.0.0"
                ],
                Expectation = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 0, 0, 0 }), 127)
            };
            yield return new DataWrapper<string[], ProgramOption>
            {
                Value =
                [
                    "--file-log",
                   "pathToLogFile",
                   "--file-output",
                   "pathToOutputFile",
                   "--time-start",
                   "02.01.2024",
                   "--time-end",
                   "04.01.2024",
                   "--address-start",
                   "127.0.0.0",
                   "--address-mask",
                   "255.255.0.0"
                ],
                Expectation = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 0, 0, 0 }), 191)
            };
            yield return new DataWrapper<string[], ProgramOption>
            {
                Value =
                [
                    "--file-log",
                   "pathToLogFile",
                   "--file-output",
                   "pathToOutputFile",
                   "--time-start",
                   "02.01.2024",
                   "--time-end",
                   "04.01.2024",
                   "--address-start",
                   "127.0.0.0",
                   "--address-mask",
                   "255.255.255.0"
                ],
                Expectation = new ProgramOption("pathToLogFile", "pathToOutputFile", new DateTime(2024, 1, 2), new DateTime(2024, 1, 4), new IPAddress(new byte[] { 127, 0, 0, 0 }), 223)
            };
        }
    }
}