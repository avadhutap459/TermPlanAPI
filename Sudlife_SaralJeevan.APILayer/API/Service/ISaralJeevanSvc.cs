using Sudlife_SaralJeevan.APILayer.API.Model;

namespace Sudlife_SaralJeevan.APILayer.API.Service
{
    public interface ISaralJeevanSvc
    {
        Task<dynamic> SaralJeevan(SaralJeevanRequest objPremiumRequest);
    }
}
