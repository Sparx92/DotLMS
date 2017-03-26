using System.Web;
using Bytes2you.Validation;
using DotLms.Services.Http.Contracts;

namespace DotLms.Services.Http
{
    public class HttpContextProvider : IHttpContextProvider
    {
        private HttpContextBase contextBase;

        public HttpContextProvider(HttpContextBase contextBase)
        {
            Guard.WhenArgument(contextBase,nameof(contextBase)).IsNull().Throw();

            this.HttpContext = contextBase;
        }

        public HttpContextBase HttpContext
        {
            get { return this.contextBase; }
            private set { this.contextBase = value; }
        }
    }
}