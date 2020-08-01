using EvaluApp.Mobile.Models;
using System.Threading.Tasks;

namespace EvaluApp.Mobile.Services
{
    public interface IApiServiceUsuario
    {
        Task<Response<Usuario>> GetUsuarioByEmailAsync(
              string urlBase,
              string servicePrefix,
              string controller,
              string email,
              string password);
    }
}
