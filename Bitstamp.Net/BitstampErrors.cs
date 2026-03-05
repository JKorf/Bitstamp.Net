using CryptoExchange.Net.Objects.Errors;

namespace Bitstamp.Net
{
    internal static class BitstampErrors
    {
        internal static ErrorMapping RestErrorMapping { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.SystemError, false, "Server error", "500.001", "500.002", "500.003"),

                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API key", "API0011", "API0001"),
                new ErrorInfo(ErrorType.Unauthorized, false, "IP Address not allowed", "API0002"),
                new ErrorInfo(ErrorType.Unauthorized, false, "No permissions", "API0003"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Account frozen", "API0006"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter validation error", "validation-error", "400.001", "400.014", "400.015", "400.016", "400.017"),

                new ErrorInfo(ErrorType.RateLimitRequest, false, "Rate limit request", "400.002"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order not found", "404.001", "404.002"),
                new ErrorInfo(ErrorType.UnavailableSymbol, false, "Unknown or unavailable symbol", "404.003", "400.003"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Missing parameters", "400.004"),

                new ErrorInfo(ErrorType.InvalidQuantity, false, "Missing quantity", "400.005"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity too low", "400.020", "400.046"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity too high", "400.037", "400.038", "400.039"),

                new ErrorInfo(ErrorType.InvalidPrice, false, "Missing price", "400.006"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Invalid price", "400.021"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameters", "400.007"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "400.009", "400.075"),

                new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "400.019"),
            ]
        );
    }
}