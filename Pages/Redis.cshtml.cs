using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackExchange.Redis;
namespace k8s_lab.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    private IConnectionMultiplexer? _connectionMultiplexer;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGetAsync()
    {
        string? fooResult;

        try
        {
            string connectionString = "redis:6379";
            _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(connectionString);
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync("date", DateTime.Now.ToString());
            fooResult = await db.StringGetAsync("date");
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Error occurred while accessing Redis");
            throw;
        }

        ViewData["RedisDate"] = fooResult ?? "No value found";
    }
}

