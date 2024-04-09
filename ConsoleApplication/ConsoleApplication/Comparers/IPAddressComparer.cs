using System.Net;

namespace ConsoleApplication.Comparers
{
    public class IPAddressComparer : IComparer<IPAddress>
    {
        public int Compare(IPAddress? firstAddress, IPAddress? secondAddress)
        {
            if (firstAddress == null || secondAddress == null)
                throw new ArgumentNullException("IPAddress argument can`t be is null.");

            int result;

            var firstAddressBytes = firstAddress.GetAddressBytes();
            var secondAddressBytes = secondAddress.GetAddressBytes();

            for (int i = 0; i < 4; i++)
            {
                result = firstAddressBytes[i].CompareTo(secondAddressBytes[i]);

                if (result != 0)
                    return result;
            }

            return 0;
        }
    }
}