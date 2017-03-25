using System.Web;

namespace DotLms.Services.Http.Contracts
{
    public interface IHttpContextProvider
    {
        HttpContextBase HttpContext { get; }
    }
}