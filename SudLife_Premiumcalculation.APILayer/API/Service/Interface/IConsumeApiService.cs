namespace SudLife_Premiumcalculation.APILayer.API.Service.Interface
{
    public interface IConsumeApiService
    {
        public Task<T> ConsumeAPI<T>(HttpMethod method, string url, object data = null, IDictionary<string, string> headers = null);

    }
}
