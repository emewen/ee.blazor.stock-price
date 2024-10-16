using Azure.Messaging.ServiceBus;
using ee.blazor.stock_price.Client.Pages;
using ee.blazor.stock_price.Components;
using ee.blazor.stock_price.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<CosmosContainerClient>();
builder.Services.AddSingleton<StockService>();
var retryOptions = new ServiceBusRetryOptions
{
    Mode = ServiceBusRetryMode.Exponential,
    MaxRetries = 3,
    Delay = TimeSpan.FromSeconds(2),
    MaxDelay = TimeSpan.FromSeconds(30)
};
var clientOptions = new ServiceBusClientOptions
{
    RetryOptions = retryOptions
};
builder.Services.AddSingleton(new ServiceBusClient("Endpoint=sb://ericewentenant-stock-price-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=W/PCxrbdHcAN+eqMMnFBavofmaJAVkM6Q+ASbMUNjc8=", clientOptions));
builder.Services.AddSingleton<ServiceBusProcessorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ee.blazor.stock_price.Client._Imports).Assembly);

app.Run();
