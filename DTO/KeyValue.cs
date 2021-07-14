using System.Collections.Generic;

namespace AlgorithmsServer.DTO
{
    public class KeyValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public List<object> Arg { get; set; }
    }
}