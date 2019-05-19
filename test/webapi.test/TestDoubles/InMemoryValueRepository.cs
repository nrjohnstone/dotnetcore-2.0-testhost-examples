using System.Collections.Generic;
using WebApi;

namespace webapi.test.TestDoubles
{
    internal class InMemoryValueRepository : IValueRepository
    {
        public Dictionary<string,string> Values { get; }

        public InMemoryValueRepository()
        {
            Values = new Dictionary<string, string>();
        }

        public string Get(string key)
        {
            return $"InMemoryValueRepository:{Values[key]}";
        }
    }
}