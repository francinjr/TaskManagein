namespace TaskManagein.Exceptions.Handler
{
    public class ValidationField
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public ValidationField(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}
