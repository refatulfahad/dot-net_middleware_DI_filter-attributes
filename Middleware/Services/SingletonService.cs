namespace Middleware.Services
{
    public class SingletonService
    {
        public int Counter = 0;

        private readonly TransientService _transientService;
        public SingletonService(TransientService transientService)
        {
            _transientService = transientService;
        }
        public int GetNextCounter()
        {
            return ++_transientService.Counter;
        }
    }
}


