using EvaluApp.Mobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace EvaluApp.Mobile.ViewModels
{
    public class PerfilUsuarioPageViewModel : ViewModelBase
    {
        private string _nombre;
        private string _apellido;
        private string _login;
        private string _cedula;
        private DateTime _fechaNacimiento;
        private INavigationService _navigationService;
        private Usuario _usuario;

        public PerfilUsuarioPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            _usuario = new Usuario();
            
        }

        #region Propiedades

        public Usuario Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }
        #endregion


        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            Usuario = (Usuario)parameters["Usuario"];
            
            base.OnNavigatedTo(parameters);
        }
    }
}
