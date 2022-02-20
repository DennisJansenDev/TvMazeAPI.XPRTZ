namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity: ({name}) with key(s): ({@key}) was not found.")
        {
        }
    }
}
