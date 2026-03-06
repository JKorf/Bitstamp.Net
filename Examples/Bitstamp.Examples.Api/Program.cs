using Bitstamp.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Bitstamp services
builder.Services.AddBitstamp();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddBitstamp(options =>
{
    options.ApiCredentials = new ApiCredentials("<APIKEY>", "<APISECRET>");
    options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoint and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] IBitstampRestClient client, string symbol) =>
{
    var symbolName = WebUtility.UrlDecode(symbol); // Fix `/` replacing
    var result = await client.ExchangeApi.ExchangeData.GetTickerAsync(symbolName);
    return result.Data.LastPrice;
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] IBitstampRestClient client) =>
{
    var result = await client.ExchangeApi.Account.GetAccountBalancesAsync();
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.Run();