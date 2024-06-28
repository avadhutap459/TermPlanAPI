

using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel;

namespace SudLife_Premiumcalculation.APILayer.API.Service.Interface
{
    public interface ISource
    {
        bool Checksourceexistornotbaseonsourcename(string sourcename);
        List<int> Getsecuritymechdatabaseonsourcename(string sourcename, string type);
        ClsSource Getsourcedetailsbaseonsourcename(string sourcename);
        void Dispose();
    }
}
