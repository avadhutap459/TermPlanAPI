namespace Sudlife_SaralJeevan.APILayer.API.Database
{
    public interface IGenericRepo
    {
        Task<int> SaveServiceLog(string Flag, int SourceId, int LogId, string PlainReq, string PlainRes, string createdBy, string LastModifiedBy, int ProductId);
        Task<string> SaveErrorLog(int Logid, string ErrorDescription);
    }
}
