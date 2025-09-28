using TaskManagein.Exceptions.Handler;

namespace TaskManagein.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }
}
