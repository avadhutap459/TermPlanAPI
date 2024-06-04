using Sudlife_ProtectShieldPlus.APILayer.API.Model;

namespace Sudlife_ProtectShieldPlus.APILayer.API.Service.ProtectShieldPlus
{
    public interface IProtectShieldPlusSvc
    {
        Task<dynamic> ProtectShieldPlus(ProtectShieldPlusRequest objProtectShieldPlusRequest);
    }
}
