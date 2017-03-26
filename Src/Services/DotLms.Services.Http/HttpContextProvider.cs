using System.Web;
using DotLms.Services.Http.Contracts;

namespace DotLms.Services.Http
{
    public class HttpContextProvider : IHttpContextProvider
    {
        private HttpContextBase contextBase;

        public HttpContextProvider(HttpContextBase contextBase)
        {
            this.HttpContext = contextBase;
        }

        public HttpContextBase HttpContext
        {
            get { return this.contextBase; }
            private set { this.contextBase = value; }
        }
    }
}