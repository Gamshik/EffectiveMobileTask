using ConsoleApplication.Options;

namespace ConsoleApplication.OptionExtensions
{
    public static class ProgramOptionExtension
    {
        /// <summary>
        /// Метод для проверки наличия обязательных параметров в классе
        /// </summary>
        /// <param name="option">Класс для валидации</param>
        /// <returns>Валидный/Невалидный</returns>
        public static bool IsValid(this ProgramOption option)
        {
            return !string.IsNullOrWhiteSpace(option.LogFile)
                && !string.IsNullOrWhiteSpace(option.OutputFile)
                && option.TimeStart.HasValue
                && option.TimeEnd.HasValue;
        }
    }
}
