using EvaluApp.Mobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluApp.Mobile.ViewModels
{
    public class StorePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public StorePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            CreateIntems();
        }


        #region Propiedades
        public List<Articulos> ListaArticulo { get; set; }
        #endregion

        #region Metodos
        private void CreateIntems()
        {
            var lista = new List<Articulos>();
            lista.Add(new Articulos { Id =1,Descripcion="1/4 Aceite",Precio =5});
            lista.Add(new Articulos { Id =2,Descripcion="Gomas",Precio =10});
            lista.Add(new Articulos { Id =3,Descripcion="Correa",Precio =15});
            lista.Add(new Articulos { Id =4,Descripcion="Amortiguador",Precio =20});
            lista.Add(new Articulos { Id =5,Descripcion="luces",Precio =25});
            lista.Add(new Articulos { Id =6,Descripcion="Disco Freno",Precio =10});
            lista.Add(new Articulos { Id =7,Descripcion="Muffle",Precio =15});

            ListaArticulo = lista;
        }
        #endregion
    }
}
