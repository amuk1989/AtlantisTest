namespace WebRequest.Data
{
    public class WebRequestSignals
    {
        public struct ConnectionError
        {
            public readonly string ErrorMessage;

            public ConnectionError(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
        }
    }
}