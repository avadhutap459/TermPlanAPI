namespace SecurityMechansim.ServiceLayer.Interface
{
    public interface IEncryptionNDecryption
    {
        string DataToBeEncrypt(string sourcename, string Plaintxt, string key);
        string DataToBeDecrypt(string sourcename, string Encrypttxt, string key);
        void Dispose();
    }
}
