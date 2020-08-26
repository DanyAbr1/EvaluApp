using EvaluApp.Mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace EvaluApp.Mobile.ViewModels
{
    public class FacturaPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ApiService _apiService;
        private DelegateCommand _pagarCommand;
        private float _total;
        private int _idVehiculo;

        public FacturaPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = new ApiService();            
        }

        #region Propiedades
        public DelegateCommand PagarCommand => _pagarCommand ?? (_pagarCommand = new DelegateCommand(Pagar));


        public float Total
        { 
            get => _total; 
            set => SetProperty(ref _total,value); 
        }
        #endregion

        #region Metodos
        private async void Pagar()
        {
            Preferences.Set("TotalPagado", _total.ToString());
            Preferences.Set("idPagado", _idVehiculo.ToString());
            await App.Current.MainPage.DisplayAlert("Información","El pago se ha realizado con éxito." , "Aceptar");
            await _navigationService.NavigateAsync("/MenuPage/NavigationPage/VehiculosPage");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() != Prism.Navigation.NavigationMode.Back)
            {
                Total = float.Parse(parameters["Total"].ToString());
                _idVehiculo = int.Parse(parameters["idvehiculopago"].ToString());
                base.OnNavigatedTo(parameters);
            }
        }
        #endregion
    }
}
