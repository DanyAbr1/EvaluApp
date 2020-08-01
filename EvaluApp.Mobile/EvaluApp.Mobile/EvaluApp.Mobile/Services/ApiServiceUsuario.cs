using EvaluApp.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EvaluApp.Mobile.Services
{
    public class ApiServiceUsuario
    {
        public async Task<Response<Usuario>> GetUsuarioByEmailAsync(
         string urlBase,
         string servicePrefix,
         string controller,
         string email,
         string password)
        {
            try
            {
                var request = new LoginRequest { Login = email, Password = password };
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };


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
    }
}
