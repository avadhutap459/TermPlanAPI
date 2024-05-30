using SudLife_ProtectShield.APILayer.API.Model;

namespace SudLife_ProtectShield.APILayer.API.Service.ProtectShield
{
    public interface IProtectShieldSvc
    {
        Task<dynamic> ProtectShield(ProtectShieldRequest objectrequet);
    }
}
