namespace SudLife_ProtectShield.APILayer.API.Database
{
    public interface IGenericRepo
    {
        dynamic SaveServiceLog(string Flag, int SourceId, int LogId, string PlainReq, string PlainRes, string createdBy, string LastModifiedBy, int ProductId);
        string SaveErrorLog(int Logid, string ErrorDescription);

    }
}
