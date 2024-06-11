namespace SecurityMechansim.ServiceLayer.Interface
{
    public interface IDigitalSign
    {
        string DataToBeDigitallySign(string plaintxt, string sourcename, string privatefilepath, string privatefilepassword,string encryptderyptkey);
        bool DataToBeValidateDigitalSign(byte[] _bytesigndata, byte[] _bytepayloaddata, string publicfilepath);
        void Dispose();
    }
}
