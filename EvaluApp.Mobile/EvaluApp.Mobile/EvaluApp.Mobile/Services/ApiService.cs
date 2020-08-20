using EvaluApp.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace EvaluApp.Mobile.Services
{
    public class ApiService
    {
        #region Usuario

        public async Task<Response<Usuario>> GetUsuarioByEmailAsync(
         string urlBase,
         string servicePrefix,
         string controller,
         string email,
         string password)
        {
            try
            {

                var request = new LoginRequest { Nombre1 = email, Contrasena = password };
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                var client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(urlBase)
                };

                //var client = new HttpClient
                //{
                //    BaseAddress = new Uri(urlBase)
                //};



                var url = $"{urlBase}{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<Usuario>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var usuario = JsonConvert.DeserializeObject<Usuario>(result);
                return new Response<Usuario>
                {
                    IsSuccess = true,
                    Result = usuario
                };
            }
            catch (Exception ex)
            {
                return new Response<Usuario>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<object>> OlvidePassword(
        string urlBase,
        string servicePrefix,
        string controller,
        object request
        )
        {
            try
            {

                // var request = new LoginRequest { Nombre1 = nuevapass., Contrasena = password };
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                var client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(urlBase)
                };

                //var client = new HttpClient
                //{
                //    BaseAddress = new Uri(urlBase)
                //};



                var url = $"{urlBase}{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<object>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var usuario = JsonConvert.DeserializeObject<Object>(result);
                return new Response<Object>
                {
                    IsSuccess = true,
                    Result = usuario
                };
            }
            catch (Exception ex)
            {
                return new Response<Object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<object>> SolicitudRegistro(string urlBase, string servicePrefix, string controller, UsuarioRequest userRegistro)
        {
            try
            {
                var requestString = JsonConvert.SerializeObject(userRegistro);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                var client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = $"{urlBase}{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<object>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var x = JsonConvert.DeserializeObject<object>(result);
                return new Response<object>
                {
                    IsSuccess = true,
                    Result = x
                };
            }
            catch (Exception ex)
            {

                return new Response<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        #endregion

        #region Vehiculos
        public async Task<Response<List<Vehiculo>>> GetVehiculosUser(
         string urlBase,
         string servicePrefix,
         string controller,         
         int id)
        {
            try
            {

                //var request = new IdRequest { IdUsuario = id };
                //var requestString = JsonConvert.SerializeObject(request);
                //var content = new StringContent("", Encoding.UTF8, "application/json");

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                var client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(urlBase)
                };

                //var client = new HttpClient
                //{
                //    BaseAddress = new Uri(urlBase)
                //};



                var url = $"{urlBase}{servicePrefix}{controller}/{id}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();                

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<List<Vehiculo>>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var vehiculo = JsonConvert.DeserializeObject<List<Vehiculo>>(result);
                return new Response<List<Vehiculo>>
                {
                    IsSuccess = true,
                    Result = vehiculo
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Vehiculo>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<Response<List<Eventos>>> GetEventosDeHoy(
       string urlBase,
       string servicePrefix,
       string controller,
       object request)
        {
            try
            {

                //var request = new IdRequest { IdUsuario = id };
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                var client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(urlBase)
                };

                //var client = new HttpClient
                //{
                //    BaseAddress = new Uri(urlBase)
                //};



                var url = $"{urlBase}{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<List<Eventos>>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var eventos = JsonConvert.DeserializeObject<List<Eventos>>(result);
                return new Response<List<Eventos>>
                {
                    IsSuccess = true,
                    Result = eventos
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Eventos>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<object>> RegistroVehiculo(string urlBase, string servicePrefix, string controller, object vehiculo)
        {
            try
            {
                var requestString = JsonConvert.SerializeObject(vehiculo);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                var client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = $"{urlBase}{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<object>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var x = JsonConvert.DeserializeObject<object>(result);
                return new Response<object>
                {
                    IsSuccess = true,
                    Result = x
                };
            }
            catch (Exception ex)
            {

                return new Response<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        #endregion
    }
}
