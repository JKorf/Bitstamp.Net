using Bitstamp.Net.Objects;
using CryptoExchange.Net.Objects;

namespace Bitstamp.Net
{
    /// <summary>
    /// Bitstamp environments
    /// </summary>
    public class BitstampEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Spot Rest client address
        /// </summary>
        public string RestBaseAddress { get; }

        /// <summary>
        /// Spot Socket client address
        /// </summary>
        public string SocketBaseAddress { get; }

        internal BitstampEnvironment(string name,
            string restBaseAddress,
            string socketBaseAddress) : base(name)
        {
            RestBaseAddress = restBaseAddress;
            SocketBaseAddress = socketBaseAddress;
        }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public BitstampEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Bitstamp environment by name
        /// </summary>
        public static BitstampEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static BitstampEnvironment Live { get; }
            = new BitstampEnvironment(TradeEnvironmentNames.Live,
                                     BitstampApiAddresses.Default.RestClientAddress,
                                     BitstampApiAddresses.Default.SocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="restAddress"></param>
        /// <param name="socketAddress"></param>
        /// <returns></returns>
        public static BitstampEnvironment CreateCustom(
                        string name,
                        string restAddress,
                        string socketAddress)
            => new BitstampEnvironment(name, restAddress, socketAddress);
    }

}
