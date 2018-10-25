namespace webapi
{
    internal class ValueRepositoryInMemory : IValueRepository
    {
        public string Get(string key)
        {
            return $"value{key}";
        }
    }
}