namespace DotLms.Services.Providers.Contracts
{
    public interface IJsonConvertProvider<T> where T : class
    {
        string SerializeObject(object value);
        T DeserializeObect(string json);
    }
}