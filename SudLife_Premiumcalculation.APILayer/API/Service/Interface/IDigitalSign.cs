namespace SudLife_Premiumcalculation.APILayer.API.Service.Interface
{
    public interface IDigitalSign
    {
        string DataToBeDigitallySign(string Plaintxt, string Sourcename);
        bool DataToBeValidateDigitalSign(byte[] _bytesigndata, byte[] _bytepayloaddata, string sourcename);
        void Dispose();
    }
}
