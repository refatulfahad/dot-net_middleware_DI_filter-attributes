using Middleware.Interfaces;

namespace Middleware.Services
{
    public class CloudMailService : IMailService
    {
        private readonly ILogger<CloudMailService> _logger;

        public CloudMailService(ILogger<CloudMailService> logger)
        {
            _logger = logger;
        }

        public void Send()
        {
            _logger.LogInformation("This is from cloudMailService");
        }
    }
}
