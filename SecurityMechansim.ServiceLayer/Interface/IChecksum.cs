

namespace SecurityMechansim.ServiceLayer.Interface
{
    public interface IChecksum
    {
        string Generatechecksumusingplaintext(string sourcename, string plaintext, string keyforchecksum);
        void Dispose();
    }
}
