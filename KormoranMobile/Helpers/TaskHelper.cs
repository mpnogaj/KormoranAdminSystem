using KormoranMobile.Helpers.ErrorHandlers;
namespace KormoranMobile.Helpers
{
	public static class TaskHelper
	{
		public static async void FireAndForgetAsync(this Task task, IErrorHandler? errorHandler = null)
		{
			try
			{
				await task;
			}
			catch (Exception ex)
			{
				if (errorHandler != null)
				{
					await errorHandler.HandleAsync($"{ex.Message} - {ex.Source}");
				}
			}
		}
	}
}
