using EvaluApp.Mobile.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace EvaluApp.Mobile.ViewModels
{
    public class StorePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;        
        private float _total;
        private DelegateCommand _comprarCommand;
        private float _cantidad;
        private float _precio;
        private Articulos _items;
        private List<Vehiculo> _vehiculos;
        private SelectVehiculos _itemVehiculos;
        private List<SelectVehiculos> _listVehiculosSelect;

        public StorePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            CreateIntems();
            var x = Preferences.Get("vehiculos", "");
            _vehiculos = JsonConvert.DeserializeObject<List<Vehiculo>>(x);
            SelectVehiculo();

        }



        #region Propiedades
        public List<Articulos> ListaArticulo { get; set; }        
        public DelegateCommand ComprarCommand => _comprarCommand ?? (_comprarCommand = new DelegateCommand(ComprarMetodo));        

        public Articulos Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public float Cantidad 
        { 
            get=> _cantidad; 
            set=> SetProperty(ref _cantidad,value); 
        }

        public float Precio
        {
            get => _precio;
            set => SetProperty(ref _precio, value);
        }

        public float Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        public SelectVehiculos ItemVehiculos
        {
            get => _itemVehiculos;
            set => SetProperty(ref _itemVehiculos, value);
        }



        public List<SelectVehiculos> ListVehiculosSelect
        {
            get => _listVehiculosSelect;
            set => SetProperty(ref _listVehiculosSelect, value);
        }
        #endregion

        #region Metodos
        private void CreateIntems()
        {
            var lista = new List<Articulos>();
            lista.Add(new Articulos { Id = 1, Descripcion = "1/4 Aceite", Precio = 500 });
            lista.Add(new Articulos { Id = 2, Descripcion = "Gomas", Precio = 1200 });
            lista.Add(new Articulos { Id = 3, Descripcion = "Correa", Precio = 700 });
            lista.Add(new Articulos { Id = 4, Descripcion = "Amortiguador", Precio = 1500 });
            lista.Add(new Articulos { Id = 5, Descripcion = "luces", Precio = 25 });
            lista.Add(new Articulos { Id = 6, Descripcion = "Disco Freno", Precio = 1000 });
            lista.Add(new Articulos { Id = 7, Descripcion = "Muffle", Precio = 1500 });

            ListaArticulo = lista;
        }

        private async void ComprarMetodo()
        {
            for (int i = 0; i < ListaArticulo.Count; i++)
            {
                if (ListaArticulo[i].Cantidad > 0 )
                {
                    _total += ListaArticulo[i].Cantidad * ListaArticulo[i].Precio;
                }
                
            }

            if(ItemVehiculos == null)
            {
                await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Información", "Debes seleccionar un vehiculo", "Aceptar");
                return;
            }

            if (_total == 0)
            {
                await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Información", "Debes agregar al menos un articulo al carrito de compras", "Aceptar"); 
                return;
            }

            float puntos = _vehiculos.FirstOrDefault(x => x.Idvehiculo == ItemVehiculos.Id).Puntos;
            if (_total > puntos)
            {
                await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Información", "No tienes puntos suficientes", "Aceptar");
                return;
            }

            var parameter = new NavigationParameters();         
            parameter.Add("Total", _total);
            parameter.Add("idvehiculopago", ItemVehiculos.Id);
            await _navigationService.NavigateAsync("FacturaPage", parameter);
        }

        private void SelectVehiculo()
        {
            // Create a list of parts.
            var list = new List<SelectVehiculos>();

            for (int i = 0; i < _vehiculos.Count; i++)
            {
                list.Add(new SelectVehiculos() { Id = _vehiculos[i].Idvehiculo, NameVehiculos = $"{ _vehiculos[i].Matricula } - ${_vehiculos[i].Puntos.ToString("0.00") }" });
            }

            ListVehiculosSelect = list;
        }


        #endregion
    }
}
