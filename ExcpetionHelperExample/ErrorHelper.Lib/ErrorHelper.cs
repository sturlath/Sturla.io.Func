using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Sturla.io.Func.ErrorHelperLib
{
	public static class ErrorHelper
	{
		public const string HttpStatus500 = "500";

		/// <summary>
		/// All errors are caught here so there is no need for try/catch except here.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <typeparam name="TArgument"></typeparam>
		/// <param name="method"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		public static TResult SafeExecutor<TArgument, TResult>(Func<TArgument, TResult> method, TArgument request)
		{
			//create an instance of the type we are going to return.
			TResult response = (TResult)Activator.CreateInstance(typeof(TResult));

			try
			{
				//Run the method delegate. All exceptions are handled here!
				return method(request);
			}
			catch (WebException webEx)
			{
				SetWebException(webEx, ref response);
			}
			//If you are using e.g Refit you could add that or other types of exception handling here.
			//catch (ApiException webEx)
			//{
			//	SetApiException(webEx, ref response);
			//}
			catch (Exception ex)
			{
				SetException(ex, ref response);
			}

			return response;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ex"></param>
		/// <param name="response"></param>
		private static void SetWebException<T>(WebException ex, ref T response)
		{
			SetError(ref response, ex.Message, HttpStatus500);
		}

		/// <summary>
		/// General exception
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ex"></param>
		/// <param name="response"></param>
		private static void SetException<T>(Exception ex, ref T response)
		{
			SetError(ref response, ex.Message, HttpStatus500);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="errorMessage"></param>
		/// <param name="errorCode"></param>
		public static void SetError<T>(ref T obj, string errorMessage, string errorCode)
		{
			PropertyInfo prop = obj.GetType().GetProperty("Error", BindingFlags.Public | BindingFlags.Instance);

			Error error = new Error()
			{
				ErrorCode = errorCode,
				ErrorMessage = errorMessage
			};

			if (prop != null && prop.CanWrite)
			{
				prop.SetValue(obj, error, null);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <returns></returns>
		public static async Task<T> ReadResponse<T>(HttpResponseMessage message) where T : BaseResponse, new()
		{
			string receiveStream = await message.Content.ReadAsStringAsync();

			// Here you will have to handle all the status codes you want to result in an error message.
			if (message.StatusCode == HttpStatusCode.Unauthorized)
			{
				return new T
				{
					Error = new Error
					{
						ErrorCode = ((int)message.StatusCode).ToString(),
						ErrorMessage = receiveStream,
					}
				};
			}

			if (message.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<T>(receiveStream);
			}

			// If we reached here there is something else wrong so we add a Error to the response with message.
			return new T
			{
				Error = new Error
				{
					ErrorCode = ((int)message.StatusCode).ToString(),
					ErrorMessage = receiveStream,
				}
			};
		}

	}
}
