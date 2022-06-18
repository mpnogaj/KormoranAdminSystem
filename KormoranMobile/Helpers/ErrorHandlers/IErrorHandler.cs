namespace KormoranMobile.Helpers.ErrorHandlers
{
	public interface IErrorHandler
	{
		Task HandleAsync(string message);
	}
}
