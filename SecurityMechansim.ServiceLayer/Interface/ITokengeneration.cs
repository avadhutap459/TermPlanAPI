namespace SecurityMechansim.ServiceLayer.Interface
{
    public interface ITokengeneration
    {
        string GenerateToken(string sourcename, string secretkey, string customerid);
        Tuple<string, string> ValidateToken(string token, string secretkey);
        void Dispose();
    }
}
