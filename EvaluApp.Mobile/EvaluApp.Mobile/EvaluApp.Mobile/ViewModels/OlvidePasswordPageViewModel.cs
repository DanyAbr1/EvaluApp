using EvaluApp.Mobile.Helpers;
using EvaluApp.Mobile.Models;
using EvaluApp.Mobile.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EvaluApp.Mobile.ViewModels
{
    public class OlvidePasswordPageViewModel : ViewModelBase 
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _cambiarPassCommand;
        private bool _isRunning;
        private readonly ApiService _apiService;


        public OlvidePasswordPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = new ApiService();
            IsEnabled = true;
            
        }

        #region Propiedades

        public DelegateCommand CambiarPassCommand => _cambiarPassCommand ?? (_cambiarPassCommand = new DelegateCommand(CambiarPass));

        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public bool IsEnabled { get; set; }

        #endregion

        #region Metodos
        private async void CambiarPass()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Información", "No se pudo conectar a internet por favor intente más tarde.", "Aceptar");

                return;
            }

            if (string.IsNullOrEmpty(Usuario))
            {
                await App.Current.MainPage.DisplayAlert("Información", "Debe ingresar un usuario.", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Información", "Debe ingresar una contraseña.", "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var request = new
            {
                Nombre1 = Usuario,                
                Contrasena = Password
            };
            var url = App.Current.Resources["UrlAPI"].ToString();

            var response = await _apiService.OlvidePassword(url, "/api", "/Usuario/olvidePass", request);
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
            
            IsRunning = false;
            await App.Current.MainPage.DisplayAlert("Información", "La contraseña ha sido cambiada con éxito.", "Aceptar");
            await _navigationService.NavigateAsync("LoginPage");
        }
        #endregion
    }
}
