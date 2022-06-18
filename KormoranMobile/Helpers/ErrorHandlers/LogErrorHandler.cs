using System.Diagnostics;

namespace KormoranMobile.Helpers.ErrorHandlers
{
	public class LogErrorHandler : IErrorHandler
	{
		public Task HandleAsync(string message)
		{
			Debug.WriteLine(message);
			return Task.CompletedTask;
		}
	}
}
