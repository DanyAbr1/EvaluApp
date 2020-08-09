using EvaluApp.Mobile.Models;
using EvaluApp.Mobile.Services;
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
    public class NuevoUsuarioPageViewModel : ViewModelBase
    {
        private ApiService _apiService;
        private bool _isRunning;
        private DelegateCommand _solicitudCommand;
        private LicenciaType _itemLicencia;
        private bool _isEnabled;
        private string _apellido;
        private string _nombre;
        private string _login;
        private List<LicenciaType> _tipoLicencia;
        private string _cedula;
        private DateTime _vence;
        private string _matricula;
        private DateTime _fechaNacimiento;
        private string _contrasena;
        private readonly INavigationService _navigationService;

        public NuevoUsuarioPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _apiService = new ApiService();
            _tipoLicencia = new List<LicenciaType>();
            _isEnabled = true;
            GetTipoLicencia();
            _navigationService = navigationService;
        }



        #region Propiedades

        public DelegateCommand SolicitudCommand => _solicitudCommand ?? (_solicitudCommand = new DelegateCommand(EnviarSolicitud));


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public LicenciaType ItemLicencia
        {
            get => _itemLicencia;
            set => SetProperty(ref _itemLicencia, value);
        }



        public List<LicenciaType> TipoLicencia
        {
            get => _tipoLicencia;
            set => SetProperty(ref _tipoLicencia, value);
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }


        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        public string Apellido
        {
            get => _apellido;
            set => SetProperty(ref _apellido, value);
        }



        public string Nombre2
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Contrasena
        {
            get => _contrasena;
            set => SetProperty(ref _contrasena, value);
        }

        public string Cedula
        {
            get => _cedula;
            set => SetProperty(ref _cedula, value);
        }

        public string Matricula
        {
            get => _matricula;
            set => SetProperty(ref _matricula, value);
        }
        public DateTime Vence
        {
            get => _vence;
            set => SetProperty(ref _vence, value);
        }

        public DateTime FechaNacimiento
        {
            get => _fechaNacimiento;
            set => SetProperty(ref _fechaNacimiento, value);
        }

        #endregion

        #region Metodos

        public async void EnviarSolicitud()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Informacion", "No se pudo conectar a internet por favor intente más tarde.", "Aceptar");
                return;
            }

            var valid = await ValidaDatosAsync();
            if (!valid) return;
            var usuario = GeneraUsuario();

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();

            var response = await _apiService.SolicitudRegistro(url, "/api", "/Usuario/AddUser", usuario);
            if (!response.IsSuccess)
            {

                IsRunning = false;
                IsEnabled = true;
                if (response.Message == "")
                {
                    response.Message ="No se pudo conectar con el servidor por favor intente más tarde.";
                }
                //var respuesta = JsonConvert.DeserializeObject<Response<object>>(response.Message);
                //var respuesta = JsonConvert.DeserializeObject<object>(response.Message);
                await App.Current.MainPage.DisplayAlert("Información", response.Message, "Aceptar");
                return;
            }

            IsRunning = false;
            IsEnabled = true;

            await App.Current.MainPage.DisplayAlert("Información", "El usuario fue Creado con éxito", "Aceptar");
            LimpiarComponentes();
            await _navigationService.NavigateAsync("LoginPage");
        }
        

        public async Task<bool> ValidaDatosAsync()
        {
            if (
                string.IsNullOrEmpty(Matricula)                
                || string.IsNullOrEmpty(Nombre)
                || string.IsNullOrEmpty(Apellido)
                || string.IsNullOrEmpty(Cedula)                
                || string.IsNullOrEmpty(Contrasena)
                || string.IsNullOrEmpty(ItemLicencia.NameLicencia)
                || Vence == null
                || FechaNacimiento == null)
            {
                await App.Current.MainPage.DisplayAlert("Información", "Debe completar todos los campos para poder procesar la soliciúd.", "Aceptar");
                return false;
            }

            return true;
        }

        private void GetTipoLicencia()
        {
            // Create a list of parts.
            var list = new List<LicenciaType>();

            list.Add(new LicenciaType() { NameLicencia = "Categiria 2" });
            list.Add(new LicenciaType() { NameLicencia = "Categiria 3" });
            list.Add(new LicenciaType() { NameLicencia = "Categiria 4" });
            list.Add(new LicenciaType() { NameLicencia = "Categiria 5" });


            _tipoLicencia = list;
        }
       

        private UsuarioRequest GeneraUsuario()
        {
            return new UsuarioRequest
            {
                Nombre1 = Nombre,

                Apellido1 = Apellido,

                Nombre2 = Nombre2,

                Tipo = ItemLicencia.NameLicencia,

                Cedula = Cedula,

                Matricula = Matricula,

                Vence = Vence,

                FechaNacimiento = FechaNacimiento,

                Contrasena = Contrasena
            };
        }

        private void LimpiarComponentes()
        {
            Matricula = string.Empty;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Cedula = string.Empty;
            Contrasena = string.Empty;
            ItemLicencia.NameLicencia = string.Empty;

        }
        #endregion

    }
}
