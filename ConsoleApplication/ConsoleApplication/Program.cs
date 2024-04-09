using ConsoleApplication.Options;

namespace ConsoleApplication
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ProgramOption options;

            try
            {
                options = ProgramOption.ParseOptions(args);
            }
            catch (ArgumentNullException argumentNullEx)
            {
                Console.WriteLine(argumentNullEx.Message);
                return;
            }
            catch (ArgumentOutOfRangeException argumentOutOfRangeEx)
            {
                Console.WriteLine(argumentOutOfRangeEx.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (!options.IsValid())
            {
                Console.WriteLine("Not all required arguments are initialize.");
                return;
            }
        }
    }
}
