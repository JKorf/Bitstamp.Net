using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitstamp.Net.Objects.Options
{
    /// <summary>
    /// Bitstamp options
    /// </summary>
    public class BitstampOptions : LibraryOptions<BitstampRestOptions, BitstampSocketOptions, ApiCredentials, BitstampEnvironment>
    {
    }
}
