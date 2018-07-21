using System;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;
using Sturla.io.Func.ErrorHelperLib;

namespace Sturla.io.Func.ErrorHelperConsole
{
	static class Program
	{
		static async Task Main(string[] args)
		{
			#region Logging
			Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.WriteTo.Console()
			.CreateLogger();
			#endregion

			var request = new SomeRequest();

			//Lets say this is a web controller and the request is coming from the internet. So lets pretend this is a WebApi Controller.
			var response = await SomeWebController(request);

			Log.Information("If there was an error it's http code is : {code} and http message is : {message}", response.Error.ErrorCode, response.Error.ErrorMessage);
		}

		/// <summary>
		/// No need to have try catch and logging, since that is all done in the SafeExecutor. 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		private static async Task<SomeResponse> SomeWebController(SomeRequest request)
		{
			Func<SomeRequest, Task<SomeResponse>> function = async (SomeRequest r) =>
			{
				return await ErrorHelper.ReadResponse<SomeResponse>(await Task.Run(() => { return FakingConnectionOverTheInternet(); }));
			};

			return await ErrorHelper.SafeExecutor(function, request);
		}

		private static async Task<SomeResponse> SomeOtherWebController(SomeRequest request)
		{
			//Local function. Short hand for what is in SomeWebController()
			async Task<SomeResponse> function(SomeRequest r)
			{
				return await ErrorHelper.ReadResponse<SomeResponse>(await Task.Run(() => { return FakingConnectionOverTheInternet(); }));
			}

			return await ErrorHelper.SafeExecutor(function, request);
		}

		/// <summary>
		/// Just a dummy call to an external/internal api that returns data. 
		/// </summary>
		/// <returns></returns>
		private static HttpResponseMessage FakingConnectionOverTheInternet()
		{
			return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
		}
	}
}
