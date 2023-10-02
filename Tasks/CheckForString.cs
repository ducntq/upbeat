using System.Data;
using System.Text.Json;
using upbeat.Entities;

namespace upbeat.Tasks
{
    public class CheckForString : Coravel.Invocable.IInvocable
    {
        private readonly ILogger<CheckForString> _logger;
        public CheckForString(ILogger<CheckForString> logger)
        {
            _logger = logger;
        }
        public async Task Invoke()
        {
            string configPath = Directory.GetCurrentDirectory() + "/config.json";
            if (!File.Exists(configPath)) throw new FileNotFoundException("File config.json does not exist");
            string fileContent = await File.ReadAllTextAsync(configPath);
            try {
            var configs = JsonSerializer.Deserialize<List<SiteConfig>>(fileContent) ?? throw new DataException("Config file is empty");
            foreach (SiteConfig config in configs)
            {
                _logger.LogInformation("Checking for: " + config.Name);
                HttpClient client = new HttpClient();
                var content = await client.GetAsync(config.Url);
                var body = await content.Content.ReadAsStringAsync();
                if (!body.Contains(config.Pattern)) _logger.LogCritical("Pattern not found on: " + config.Name);
                else _logger.LogInformation("Found pattern!");
            }
            }
            catch (Exception e) {
                _logger.LogCritical(e.Message);
            }
        }
    }
}