using System.Security.Claims;

namespace BrowserGame.Static
{
	public static class GameSession
	{
		public static string GetUserId(ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
