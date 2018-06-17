using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sturla.io.Func.Four.Lib
{
	public class ErrorHelper
	{
		public const string HttpStatus500 = "500";

		/// <summary>
		/// All errors are caught here so there is no need for try/catch except here.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <param name="method"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		public static T SafeExecutor<T, T1>(Func<T1, T> method, T1 request)
		{
			//create an instance of the type we are going to return.
			var response = (T)Activator.CreateInstance(typeof(T));

			try
			{
				//Run the method delegate. All exceptions are handled here!
				return method(request); 
			}
			catch (WebException webEx)
			{
				SetWebException(webEx, ref response);
			}
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
		/// 
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
			var prop = obj.GetType().GetProperty("Error", BindingFlags.Public | BindingFlags.Instance);

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
			var receiveStream = await message.Content.ReadAsStringAsync();

			// Here you will have to handle all the status codes you want to result in an error message.
			if (message.StatusCode == HttpStatusCode.Unauthorized)
			{
				var response = new T
				{
					Error = new Error
					{
						ErrorCode = ((int)message.StatusCode).ToString(),
						ErrorMessage = receiveStream,
					}
				};

				return response;
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
