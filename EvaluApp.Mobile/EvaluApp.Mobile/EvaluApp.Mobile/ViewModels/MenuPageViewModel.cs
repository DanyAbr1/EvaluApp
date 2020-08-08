using EvaluApp.Mobile.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace EvaluApp.Mobile.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;
        private DelegateCommand _verPerfilUsuarioCommand;
        private Menu _menu;
        private Usuario _usuario;

        public MenuPageViewModel(INavigationService navigationService)
             : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }


        #region Propidades

        public List<Menu> Menus { get; set; }
        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenu));

        //public DelegateCommand VerPerfilUsuarioCommand => _verPerfilUsuarioCommand ?? (_verPerfilUsuarioCommand = new DelegateCommand(VerPerfilUsuario));


        //public string NombreCompleto
        //{
        //    get => _nombreCompleto;
        //    set => SetProperty(ref _nombreCompleto, value);
        //}

        //public string AppVersion
        //{
        //    get => _appVersion;
        //    set => SetProperty(ref _appVersion, value);
        //}

        public Menu Menu
        {
            get => _menu;
            set => SetProperty(ref _menu, value);
        }        
        #endregion

        #region Metodos

        private void LoadMenus()
        {
            Menus = new List<Menu>
            {
                new Menu
                {
                    Icono = IconFont.Home,
                    Pagina = "VehiculosPage",
                    Titulo = "Inicio"
                },
                 new Menu
                {
                    Icono = IconFont.History,
                    Pagina = "HistoriaPage",
                    Titulo = "Historial"
                },
                  new Menu
                {
                    Icono = IconFont.User,
                    Pagina = "UsuarioPage",
                    Titulo = "Usuario"
                },                
                new Menu
                {
                    Icono = IconFont.SignOutAlt,
                    Pagina = "LoginPage",
                    Titulo = "Cerrar Sesión"
                }
            };

        }

        private async void SelectMenu()
        {
            if (Menu.Pagina.Equals("LoginPage"))
            {
                var confirm = await App.Current.MainPage.DisplayAlert("Información", "¿Esta seguro que desea cerrar la sesión?", "Salir", "Cancelar");
                if (confirm)
                {
                    Preferences.Set("token", "");
                    await _navigationService.NavigateAsync("/NavigationPage/LoginPage");
                }

                return;
            }

            await _navigationService.NavigateAsync($"/MenuPage/NavigationPage/{Menu.Pagina}");

        }

        #endregion

    }
}
