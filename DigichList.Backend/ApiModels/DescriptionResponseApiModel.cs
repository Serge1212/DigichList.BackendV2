namespace DigichList.Backend.ApiModels
{
    public class DescriptionResponseApiModel
    {
        public string Message {get; set;}

        public string StackTrace { get; set; }
        public DescriptionResponseApiModel(string stackTrace, string message = null)
        {
            Message = message;
            StackTrace = stackTrace;
        }
    }
}
