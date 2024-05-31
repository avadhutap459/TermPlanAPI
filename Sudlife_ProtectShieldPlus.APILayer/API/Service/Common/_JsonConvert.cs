﻿using Newtonsoft.Json;

namespace Sudlife_ProtectShieldPlus.APILayer.API.Service.Common
{
    public class _JsonConvert
    {
        public static string SerializeObject<T>(T RequestObject)
        {
            try
            {
                return JsonConvert.SerializeObject(RequestObject);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static T DeSerializeObject<T>(string ResponseString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(ResponseString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
