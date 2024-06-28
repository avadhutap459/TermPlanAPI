using Newtonsoft.Json;

namespace SudLife_Premiumcalculation.APILayer.API.Service.Global
{
    public class ClsJsonConverter
    {
        public static string SerializeObject<T>(T RequestObject)
        {
            try
            {
                return JsonConvert.SerializeObject(RequestObject);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public static T DeserializeObject<T>(string PlainBody)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(PlainBody);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
