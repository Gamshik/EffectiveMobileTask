using ConsoleApplication.Comparers;
using System.Globalization;
using System.Net;

namespace ConsoleApplication.Options
{
    public class ProgramOption
    {
        /// <summary>
        /// Путь к файлу с логами
        /// </summary>
        private string _logFile;
        /// <summary>
        /// Путь к файлу с результатом
        /// </summary>
        private string _outputFile;
        /// <summary>
        /// Нижняя граница диапазона адресов
        /// </summary>
        private IPAddress _ipAddressStart;
        /// <summary>
        /// Верхняя граница диапазона адресов
        /// </summary>
        private int? _ipAddressEnd;
        /// <summary>
        /// Нижняя граница временного интервала
        /// </summary>
        private DateTime? _timeStart;
        /// <summary>
        /// Верхняя граница временного интервала
        /// </summary>
        private DateTime? _timeEnd;
        /// <summary>
        /// Путь к файлу с логами
        /// </summary>
        /// <exception cref="ArgumentNullException">Если значение является пустым или ранво null</exception>
        public string LogFile
        {
            get
            {
                return _logFile;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Log file argument is incorrect.");

                _logFile = value;
            }
        }
        /// <summary>
        /// Путь к файлу с результатом
        /// </summary>
        /// <exception cref="ArgumentNullException">Если значение является пустым или ранво null</exception>
        public string OutputFile
        {
            get
            {
                return _outputFile;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Output file argument is incorrect.");

                _outputFile = value;
            }
        }
        /// <summary>
        /// Нижняя граница диапазона адресов
        /// </summary>
        /// <exception cref="ArgumentNullException">Если значение является пустым или ранво null</exception>
        public IPAddress IpAddressStart
        {
            get
            {
                return _ipAddressStart;
            }
            set
            {
                _ipAddressStart = value;
            }
        }
        /// <summary>
        /// Верхняя граница диапазона адресов
        /// </summary>
        /// <exception cref="Exception">Если нижняя граница диапазона адрессов не инитиализированна</exception>
        /// <exception cref="ArgumentOutOfRangeException">Если граница начального диапазона больше, чем конечного</exception>
        public int? IpAddressEnd
        {
            get
            {
                return _ipAddressEnd;
            }
            private set
            {
                if (IpAddressStart == null)
                    throw new Exception("To initialize address end options, start address must be initialized.");

                var ipAddressComparer = new IPAddressComparer();
                var ipAddressEnd = IpAddressParse($"{value}.0.0.0");

                if (ipAddressComparer.Compare(IpAddressStart, ipAddressEnd) > 0)
                    throw new ArgumentOutOfRangeException("Address start can`t be greater than address end.");

                _ipAddressEnd = value;
            }
        }
        /// <summary>
        /// Нижняя граница временного интервала
        /// </summary>
        public DateTime? TimeStart
        {
            get
            {
                return _timeStart;
            }
            set
            {
                _timeStart = value;
            }
        }
        /// <summary>
        /// Верхняя граница временного интервала
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Если значение меньше, чем нижняя граница временного интервала</exception>
        public DateTime? TimeEnd
        {
            get
            {
                return _timeEnd;
            }
            set
            {
                if (value < TimeStart)
                    throw new ArgumentOutOfRangeException("Time end can`t be less than time start option.");

                _timeEnd = value;
            }
        }
        /// <summary>
        /// Метод для проверки наличия обязательных параметров в классе
        /// </summary>
        /// <param name="option">Класс для валидации</param>
        /// <returns>Валидный/Невалидный</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(LogFile)
                && !string.IsNullOrWhiteSpace(OutputFile)
                && TimeStart.HasValue
                && TimeEnd.HasValue;
        }
        /// <summary>
        /// Метод для парсинга аргументов
        /// </summary>
        /// <param name="args">Строка содержащая аргументы и их значения</param>
        /// <returns>Созданный класс с аргументами</returns>
        /// <exception cref="ArgumentException">Если передан неизвестный аргумент</exception>
        /// <exception cref="ArithmeticException">Если не у каждого аргумента есть значение</exception>
        public static ProgramOption ParseOptions(string[] args)
        {
            var options = new ProgramOption();

            var argumentHandlers = new Dictionary<string, Action<string>>
            {
                { "--file-log", value => options.LogFile = value },
                { "--file-output", value => options.OutputFile = value },
                { "--address-start", value => options.IpAddressStart = IpAddressParse(value) },
                { "--address-mask", value => options.IpAddressEnd = IpAddressEndParseByMask(value) },
                { "--time-start", value => options.TimeStart = DateParse(value) },
                { "--time-end", value => options.TimeEnd = DateParse(value) }
            };

            for (var i = 0; i < args.Length; i += 2)
            {
                var argName = args[i];

                if (!argumentHandlers.ContainsKey(argName))
                    throw new ArgumentException($"Argument with name - {argName} is not exist.");

                var argValueIndex = i + 1;

                if (argValueIndex >= args.Length)
                    throw new ArgumentException("Not all argument have a value.");

                if (argumentHandlers[argName] != null)
                    throw new ArgumentException($"An argument {argName} can only be used once.");

                argumentHandlers[argName](args[argValueIndex]);
            }

            return options;
        }
        /// <summary>
        /// Парсинг строки формата dd.MM.yyyy в дату 
        /// </summary>
        /// <param name="date">Строка фомрата dd.MM.yyyy</param>
        /// <returns>Полученную дату</returns>
        /// <exception cref="ArgumentException">Если строка не корректная</exception>
        private static DateTime DateParse(string date)
        {
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;
            else
                throw new ArgumentException($"Argument - {date} is incorrect.");
        }
        /// <summary>
        /// Парсинг строки в ip адресс
        /// </summary>
        /// <param name="ipAddress">Строка содержащая ip адресс</param>
        /// <returns>Полученный ip адресс</returns>
        /// <exception cref="ArgumentException">Если строка не корректная</exception>
        private static IPAddress IpAddressParse(string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out var result))
                return result;
            else
                throw new ArgumentException($"Argument - {ipAddress} is incorrect.");
        }
        /// <summary>
        /// Получение верхней границы диапазона адрессов по маске подсети
        /// </summary>
        /// <param name="mask">Маска подсети</param>
        /// <returns>Границу верхнего диапазона адресов</returns>
        /// <exception cref="ArgumentException">Если маска задана некорректно</exception>
        private static int IpAddressEndParseByMask(string mask)
        {
            switch (mask)
            {
                case "255.0.0.0":
                    return 127;
                case "255.255.0.0":
                    return 191;
                case "255.255.255.0":
                    return 223;
                default:
                    throw new ArgumentException($"Subnet mask - {mask} is incorrect.");
            }
        }
    }
}