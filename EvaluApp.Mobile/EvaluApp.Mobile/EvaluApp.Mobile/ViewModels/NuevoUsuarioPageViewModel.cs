using EvaluApp.Mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluApp.Mobile.ViewModels
{
    public class NuevoUsuarioPageViewModel : ViewModelBase
    {
        private  ApiService _apiService;

        public NuevoUsuarioPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _apiService = new ApiService();
        }
    }
}
