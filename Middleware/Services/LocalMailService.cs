using Middleware.Interfaces;

namespace Middleware.Services
{
    public class LocalMailService : IMailService
    {
        private readonly ILogger<LocalMailService> _logger;

        public LocalMailService(ILogger<LocalMailService> logger)
        {
            _logger = logger;

        }

        public void Send()
        {
            _logger.LogInformation("This is from localMailService");
        }
    }
}
