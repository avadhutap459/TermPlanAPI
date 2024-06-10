namespace SudLife_Premiumcalculation.APILayer.API.Service.Interface
{
    public interface IEncryptionNDecryption
    {
        string DataToBeEncrypt(string Plaintxt, string key);
        string DataToBeDecrypt(string Encrypttxt, string key);
        void Dispose();
    }
}
