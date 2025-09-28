using TaskManagein.Exceptions.Handler;

namespace TaskManagein.Exceptions
{
    public class InvalidFieldException : Exception
    {
        public List<ValidationField> Errors { get; }

        public InvalidFieldException(string message, List<ValidationField> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
