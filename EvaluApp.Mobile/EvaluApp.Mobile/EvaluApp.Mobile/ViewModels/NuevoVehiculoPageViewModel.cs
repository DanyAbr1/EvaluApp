using EvaluApp.Mobile.Helpers;
using EvaluApp.Mobile.Models;
using EvaluApp.Mobile.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace EvaluApp.Mobile.ViewModels
{
    public class NuevoVehiculoPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _addVehiculoCommand;
        private string _matricula;
        private bool _isRunning;
        private bool _isEnabled;
        private ApiService _apiService;

        public NuevoVehiculoPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _apiService = new ApiService();
            IsEnabled = true;
            _navigationService = navigationService;
        }

        #region Propiedades
        public DelegateCommand AddVehiculoCommand => _addVehiculoCommand ?? (_addVehiculoCommand = new DelegateCommand(RegistrarVehiculo));

        public string Matricula
        {
            get => _matricula;
            set => SetProperty(ref _matricula, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        #endregion

        #region Metodos

        private async void RegistrarVehiculo()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Información", "No se pudo conectar a internet por favor intente más tarde.", "Aceptar");

                return;
            }

            if (string.IsNullOrEmpty(Matricula))
            {
                await App.Current.MainPage.DisplayAlert("Información", "Debe ingresar una Matricula.", "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;


            var request = new
            {
                matricula = Matricula,
                idusuario1 = Preferences.Get("idUsuario", 0)
            };
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.RegistroVehiculo(url, "/api", "/Vehiculos/AddVehiculo", request);
            if (!response.IsSuccess)
            {

                IsRunning = false;
                IsEnabled = true;
                if (response.Message == "")
                {
                    response.Message = "No se pudo conectar con el Servidor por favor intente más tarde.";
                }

                var isValid = IsValidJson.IsValid(response.Message);
                if (isValid)
                {
                    var respuesta = JsonConvert.DeserializeObject<ResponseMessage>(response.Message);
                    await App.Current.MainPage.DisplayAlert("Información", respuesta.Message, "Aceptar");
                    return;
                }

                await App.Current.MainPage.DisplayAlert("Información", response.Message, "Aceptar");

                return;
            }

            await App.Current.MainPage.DisplayAlert("Información","El Véhiculo ha sido registrado", "Aceptar");

            Matricula = string.Empty;
            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
