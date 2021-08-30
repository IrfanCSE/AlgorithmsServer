using Newtonsoft.Json;

namespace AlgorithmsServer.ErrorHandling
{
    public class ErrorDetails
    {
        public int statuscode { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
