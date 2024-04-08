using System.Globalization;

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
        }
    }
}
