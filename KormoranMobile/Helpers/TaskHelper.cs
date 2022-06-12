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
				errorHandler?.Handle(ex);
			}
		}

	}
}
