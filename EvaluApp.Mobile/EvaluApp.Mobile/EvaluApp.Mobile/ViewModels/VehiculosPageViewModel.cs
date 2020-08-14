using EvaluApp.Mobile.Models;
using EvaluApp.Mobile.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EvaluApp.Mobile.ViewModels
{
    public class VehiculosPageViewModel : ViewModelBase
    {
      
      
        private readonly INavigationService _navigationService;
        private ApiService _apiService;
        private List<Vehiculo> _listaVehiculos;
        private int _usuario;
        private bool _isRunning;
        private float _puntos;
        private List<Eventos> _listaEventos;

        public VehiculosPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = new ApiService();
            _listaVehiculos = new List<Vehiculo>();
            _listaEventos = new List<Eventos>();
            _usuario = Preferences.Get("idUsuario",0);
            Title = "Inicio";
            Puntos = 100;

        }


        #region Propiedades
        public List<Vehiculo> ListaVehiculos
        {
            get => _listaVehiculos;
            set => SetProperty(ref _listaVehiculos, value);
        }

        public List<Eventos> ListaEventos
        {
            get => _listaEventos;
            set => SetProperty(ref _listaEventos, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public float Puntos
        {
            get => _puntos;
            set => SetProperty(ref _puntos, value);
        }
        #endregion

        #region Metodos


        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetVehiculos();
            await GetEventos();
        }

        private async Task GetEventos()
        {
            IsRunning = true;

            var request = new
            {
                idusuario = _usuario,
                idvehiculo = ListaVehiculos[0].Idvehiculo
            };

            var url = Prism.PrismApplicationBase.Current.Resources["UrlAPI"].ToString();



            var response = await _apiService.GetEventosDeHoy(url, "/api", "/Datos/buscarDatos", request);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                if (response.Message == "")
                {
                    response.Message = "No se pudo conectar con el Servidor por favor intente más tarde.";
                }

                await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Información", response.Message.ToString(), "Aceptar");
                await _navigationService.GoBackToRootAsync();
                return;
            }

            ListaEventos = response.Result.ToList();
            IsRunning = false;



            for (int i = 0; i < ListaEventos.Count; i++)
            {
                Puntos -= float.Parse(ListaEventos[i].Puntos.ToString());
            }

        }

        private async Task GetVehiculos()
        {
            IsRunning = true;

            var url = Prism.PrismApplicationBase.Current.Resources["UrlAPI"].ToString();



            var response = await _apiService.GetVehiculosUser(url, "/api", "/Vehiculos", _usuario);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                if (response.Message == "")
                {
                    response.Message = "No se pudo conectar con el Servidor por favor intente más tarde.";
                }

                await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Información", response.Message.ToString(), "Aceptar");
                await _navigationService.GoBackToRootAsync();
                return;
            }



            ListaVehiculos = response.Result.ToList();
            IsRunning = false;
        }
        #endregion
    }

}