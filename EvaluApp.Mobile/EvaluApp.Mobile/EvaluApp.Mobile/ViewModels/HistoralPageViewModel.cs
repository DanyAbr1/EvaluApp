using EvaluApp.Mobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluApp.Mobile.ViewModels
{
    public class HistoralPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<Eventos> _listaEventos;

        public HistoralPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            _listaEventos = new List<Eventos>();
        }


        #region Propiedades
        public List<Eventos> ListaEventos
        {
            get => _listaEventos;
            set => SetProperty(ref _listaEventos, value);
        }
        #endregion

        #region Metodos
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() != Prism.Navigation.NavigationMode.Back)
            {
                ListaEventos = (List<Eventos>)parameters["Historial"];                

                base.OnNavigatedTo(parameters);
            }
        }
        #endregion
    }
}
