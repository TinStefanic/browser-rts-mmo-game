using System.Security.Claims;

namespace BrowserGame.Utility
{
	public class HttpContextUtil
	{
		public string UserId => _httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

		private readonly HttpContext _httpContext;

		public HttpContextUtil(HttpContext httpContext)
		{
			_httpContext = httpContext;
		}
	}
}
