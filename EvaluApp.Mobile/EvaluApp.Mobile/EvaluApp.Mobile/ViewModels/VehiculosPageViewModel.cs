using EvaluApp.Mobile.Models;
using EvaluApp.Mobile.Services;
using Prism.Commands;
using Prism.Navigation;
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
        private List<Eventos> _listaEventos;
        private DelegateCommand _selectHistoralCommand;
        private DelegateCommand _nuevoVehiculoCommand;

        private float _penalizacion;
        private string _idPage;

        public VehiculosPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = new ApiService();
            _listaVehiculos = new List<Vehiculo>();
            _listaEventos = new List<Eventos>();
            _usuario = Preferences.Get("idUsuario", 0);
            Title = "Inicio";
            GetVehiculos();
        }


        #region Propiedades

        public DelegateCommand SelectHistoralCommand => _selectHistoralCommand ?? (_selectHistoralCommand = new DelegateCommand(SelectHistorialPage));
        public DelegateCommand NuevoVehiculoCommand => _nuevoVehiculoCommand ?? (_nuevoVehiculoCommand = new DelegateCommand(NuevoVehiculo));



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

        #endregion

        #region Metodos


        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            _idPage = Preferences.Get("menuPage", "default_value");
            if (_idPage == "NuevoVehiculoPageViewModel")
            {
                GetVehiculos();
            }

        }

        private async Task GetEventos()
        {
            IsRunning = true;

            for (int i = 0; i < ListaVehiculos.Count; i++)
            {


                var request = new
                {
                    idusuario = _usuario,
                    idvehiculo = ListaVehiculos[i].Idvehiculo,
                    //fecha = "2020-08-13"
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



                for (int ii = 0; ii < ListaEventos.Count; ii++)
                {
                    _penalizacion += float.Parse(ListaEventos[ii].Puntos.ToString());
                }

                var total = 5000 - _penalizacion;
                ListaVehiculos[i].Puntos = total;


                var src = ListaVehiculos;
                ListaVehiculos = null;
                ListaVehiculos = src;
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

            if (response.Result.Count > 0)
            {
                for (int i = 0; i < response.Result.Count; i++)
                {
                    response.Result[i].Puntos = 5000;
                }                
                ListaVehiculos = response.Result.ToList();
                await GetEventos();
            }


            IsRunning = false;
        }


        private void SelectHistorialPage()
        {
            var parameter = new NavigationParameters();
            parameter.Add("Historial", ListaEventos);
            _navigationService.NavigateAsync("HistoralPage", parameter);
        }

        private async void NuevoVehiculo()
        {
            await _navigationService.NavigateAsync("NuevoVehiculoPage");
        }
        #endregion
    }

}