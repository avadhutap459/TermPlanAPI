namespace SudLife_Premiumcalculation.APILayer.API.Service.Interface
{
    public interface IDigitalSign
    {
        string DataToBeDigitallySign(string Plaintxt, string Sourcename);
        void Dispose();
    }
}
