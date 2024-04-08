﻿using System.Globalization;

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
        private string _addressStart;
        /// <summary>
        /// Маска подсети
        /// </summary>
        private string _addressMask;
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
        public string AddressStart
        {
            get
            {
                return _addressStart;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Address start argument is incorrect.");

                _addressStart = value;
            }
        }
        /// <summary>
        /// Маска подсети
        /// </summary>
        /// <exception cref="ArgumentNullException">Если значение является пустым или ранво null</exception>
        /// <exception cref="Exception">Если нижняя граница диапазона адрессов не инитиализированна</exception>
        public string AddressMask
        {
            get
            {
                return _addressMask;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Address mask argument is incorrect.");
                else if (string.IsNullOrEmpty(_addressStart))
                    throw new Exception("To initialize address mask options, start address must be initialized.");

                _addressMask = value;
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
                if (value < _timeStart)
                    throw new ArgumentOutOfRangeException("Time end can`t be less than time start option.");

                _timeEnd = value;
            }
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
                { "--address-start", value => options.AddressStart = value },
                { "--address-mask", value => options.AddressMask = value },
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
                    throw new ArithmeticException("Not all argument have a value.");

                argumentHandlers[argName](args[argValueIndex]);
            }

            return options;
        }
        /// <summary>
        /// Метод для парсинга строки формата dd.MM.yyyy в дату 
        /// </summary>
        /// <param name="date">строка фомрата dd.MM.yyyy</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Если строка не корректная</exception>
        private static DateTime DateParse(string date)
        {
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;
            else
                throw new ArgumentException($"Argument - {date} is incorrect.");
        }
    }
}
