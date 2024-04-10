using ConsoleApplication.Comparers;
using ConsoleApplication.Options;
using System.Globalization;
using System.Net;

namespace ConsoleApplication
{
    public class Program
    {
        /// <summary>
        /// Переменная содержащая значения переданных аргументов
        /// </summary>
        private static ProgramOption _options;
        static async Task Main(string[] args)
        {
            try
            {
                _options = ProgramOption.ParseOptions(args);
            }
            catch (ArgumentException argumentEx)
            {
                Console.WriteLine($"At parsing options error occured: {argumentEx.Message}");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occured: {ex.Message}");
                return;
            }

            if (!_options.IsValid())
            {
                Console.WriteLine("Not all required arguments are initialize.");
                return;
            }

            try
            {
                var lines = await File.ReadAllLinesAsync(_options.LogFile);

                var ipAddressCount = GetDictionaryIpAddressCountByOptions(lines, _options);

                if (ipAddressCount.Count == 0)
                {
                    Console.WriteLine("No matching ip addresses found.");
                    return;
                }

                using StreamWriter writer = new StreamWriter(_options.OutputFile, false);

                foreach (var item in ipAddressCount)
                {
                    await writer.WriteLineAsync($"{item.Key} {item.Value}");
                }
            }
            catch (ArgumentException argumentEx)
            {
                Console.WriteLine($"Argument is incorrect: {argumentEx.Message}");
                return;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Path to file is incorrect.");
                return;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Path to directory is incorrect.");
                return;
            }
            catch (UnauthorizedAccessException unauthorizedAccessEx)
            {
                Console.WriteLine($"At file write access error occured: {unauthorizedAccessEx.Message}");
                return;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"At file read/write error occured: {ioEx.Message}");
                return;
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine($"Argument format is incorrect: {formatEx.Message}");
                return;
            }
        }
        public static Dictionary<IPAddress, int> GetDictionaryIpAddressCountByOptions(string[] logs, ProgramOption options)
        {
            var ipAddressComparer = new IPAddressComparer();

            var ipAddressCount = new Dictionary<IPAddress, int>();

            foreach (var log in logs)
            {
                var logParts = log.Split(':', 2);

                DateTime requestTime = DateTime.ParseExact(logParts[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                if (requestTime >= options.TimeStart && requestTime <= options.TimeEnd)
                {
                    IPAddress ipAddress = IPAddress.Parse(logParts[0]);

                    if (options.IpAddressStart != null && ipAddressComparer.Compare(ipAddress, options.IpAddressStart) < 0)
                        continue;

                    if (options.IpAddressEnd != null)
                    {
                        var ipAddressEnd = IPAddress.Parse($"{options.IpAddressEnd}.0.0.0");

                        if (ipAddressComparer.Compare(ipAddress, ipAddressEnd) > 0)
                            continue;
                    }

                    if (ipAddressCount.ContainsKey(ipAddress))
                        ipAddressCount[ipAddress]++;
                    else
                        ipAddressCount.Add(ipAddress, 1);
                }
            }

            return ipAddressCount;
        }
    }
}