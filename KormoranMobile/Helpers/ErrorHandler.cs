using System.Diagnostics;

namespace KormoranMobile.Helpers
{
	public interface IErrorHandler
	{
		public void Handle(Exception ex);
	}


	public class LogErrorHandler : IErrorHandler
	{
		public void Handle(Exception ex)
		{
			Debug.WriteLine($"{ex.Message} - {ex.Source}");
		}
	}
}
