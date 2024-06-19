using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel;

namespace SudLife_Premiumcalculation.APILayer.API.Service.Interface
{
    public interface ISource
    {
        ClsSource Getsourcedetailsbaseonsourcename(string sourcename);
        void Dispose();
    }
}
