using System;
using System.Collections.Generic;
using Serilog;

namespace Sturla.io.Func.Six.Console
{
	/// <summary>
	/// Memoization take a look at https://www.infoq.com/news/2007/01/CSharp-memory
	/// </summary>
	public static class Caching
	{
		/// <summary>
		/// Here you do recursion, but you remember the value of each Fibonacci/LowerCasing. 
		/// Thus, instead of 29 Million for 35th Fibonacci you do 37 calculations. And it takes just 0.002 seconds.
		/// 
		/// "In computing, memoization or memoisation is an optimization technique used primarily to speed up computer programs 
		/// by storing the results of expensive function calls and returning the cached result when the same inputs occur again."
		/// https://en.wikipedia.org/wiki/Memoization
		/// </summary>
		/// <typeparam name="TArgument"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="function"></param>
		/// <returns></returns>
		public static Func<TArgument, TResult> Memoize<TArgument, TResult>(this Func<TArgument, TResult> function)
		{
			var methodDictionaries = new Dictionary<string, Dictionary<TArgument, TResult>>();

			var name = function.Method.Name;
			if (!methodDictionaries.TryGetValue(name, out Dictionary<TArgument, TResult> values))
			{
				values = new Dictionary<TArgument, TResult>();

				Log.Information("MethodName: {name}, ReturnType: {returnType}", name, function.Method.ReturnType);

				methodDictionaries.Add(name, values);
			}

			return a =>
			{
				// If we have already got value cached from e.g Fibonacci(i) or  it has been added to values
				// and there is no need to call the expensive function. We have the result
				if (!values.TryGetValue(a, out TResult value))
				{
					value = function(a);
					values.Add(a, value);
				}

				return value;
			};
		}
	}
}
