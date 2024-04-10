using ConsoleApplication.Comparers;
using ConsoleApplication.Options;
using System.Globalization;
using System.Net;

namespace ConsoleApplication
{
    public class Program
    {
        /// <summary>
        /// Словарь: key - ip адресс, value - количество обращений к нему
        /// </summary>
        private static Dictionary<IPAddress, int> _ipAddressCount = new Dictionary<IPAddress, int>();
        /// <summary>
        /// Переменная компаратор для ip адрессов
        /// </summary>
        private static IPAddressComparer _ipAddressComparer = new IPAddressComparer();
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
                using StreamReader reader = new StreamReader(_options.LogFile);

                string? line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var lines = line.Split(':', 2);

                    DateTime requestTime = DateTime.ParseExact(lines[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    if (requestTime >= _options.TimeStart && requestTime <= _options.TimeEnd)
                    {
                        IPAddress ipAddress = IPAddress.Parse(lines[0]);

                        if (_options.IpAddressStart != null && _ipAddressComparer.Compare(ipAddress, _options.IpAddressStart) < 0)
                            continue;

                        if (_options.IpAddressEnd != null)
                        {
                            var ipAddressEnd = IPAddress.Parse($"{_options.IpAddressEnd}.0.0.0");

                            if (_ipAddressComparer.Compare(ipAddress, ipAddressEnd) > 0)
                                continue;
                        }

                        if (_ipAddressCount.ContainsKey(ipAddress))
                            _ipAddressCount[ipAddress]++;
                        else
                            _ipAddressCount.Add(ipAddress, 1);
                    }
                }

                if (_ipAddressCount.Count == 0)
                {
                    Console.WriteLine("No matching ip addresses found.");
                    return;
                }

                using StreamWriter writer = new StreamWriter(_options.OutputFile, false);

                foreach (var item in _ipAddressCount)
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

            Console.WriteLine("Success!");
        }
    }
}