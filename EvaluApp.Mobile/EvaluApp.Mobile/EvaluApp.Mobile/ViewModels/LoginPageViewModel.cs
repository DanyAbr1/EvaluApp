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
using System.Text;
using Xamarin.Essentials;

namespace EvaluApp.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService =  new ApiService();
        }

        #region Propiedades

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));
        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
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
        private async void Login()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Información", "No se pudo conectar a internet por favor intente más tarde.", "Aceptar");

                return;
            }

            if (string.IsNullOrEmpty(Email))
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



            var url = App.Current.Resources["UrlAPI"].ToString();

            var response = await _apiService.GetUsuarioByEmailAsync(url, "/api", "/Usuario/authenticate", Email, Password);
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
                    Password = string.Empty;
                    return;
                }

                await App.Current.MainPage.DisplayAlert("Información", response.Message, "Aceptar");
                Password = string.Empty;
                return;
            }

            Password = string.Empty;

            IsRunning = false;

            Preferences.Set("idUsuario", response.Result.Idusuario);

            var parameter = new NavigationParameters();
            parameter.Add("Usuario", response.Result);
            await _navigationService.NavigateAsync("/MenuPage/NavigationPage/VehiculosPage", parameter);
        }
        #endregion
    }
}
