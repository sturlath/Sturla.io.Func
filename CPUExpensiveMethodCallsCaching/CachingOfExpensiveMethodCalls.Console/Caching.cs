using Serilog;
using System;
using System.Collections.Generic;

namespace Sturla.io.Func.CachingOfExpensiveMethodCalls.Console
{
	/// <summary>
	/// Memoization take a look at https://www.infoq.com/news/2007/01/CSharp-memory
	/// </summary>
	public static class Caching
	{
		/// <summary>
		/// <para>
		/// Here you do recursion, but you remember the value of each Fibonacci/LowerCasing. 
		/// Thus, instead of 29 Million for 35th Fibonacci you do 37 calculations. And it takes just 0.002 seconds.
		/// </para>
		/// <para>
		/// "In computing, memoization or memoisation is an optimization technique used primarily to speed up computer programs 
		/// by storing the results of expensive function calls and returning the cached result when the same inputs occur again."
		/// https://en.wikipedia.org/wiki/Memoization
		/// </para>
		/// </summary>
		/// <typeparam name="TArgument"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="function">The expensive method sent in to cache its results.</param>
		/// <returns></returns>
		public static Func<TArgument, TResult> Memoize<TArgument, TResult>(this Func<TArgument, TResult> function)
		{
			var allDictionaries = new Dictionary<string, Dictionary<TArgument, TResult>>();

			var name = function.Method.Name;

			// If the this kind of function has not been cached (Memoized) before we need to create a dictionary and add it. 
			if (!allDictionaries.TryGetValue(name, out Dictionary<TArgument, TResult> functionDictionary))
			{
				functionDictionary = new Dictionary<TArgument, TResult>();

				Log.Information("MethodName: {name}, ReturnType: {returnType}", name, function.Method.ReturnType);

				allDictionaries.Add(name, functionDictionary);
			}

			return key =>
			{
				// If the value calculated was not in this function dictionary we need to calculate it and add it.
				if (!functionDictionary.TryGetValue(key, out TResult value))
				{
					value = function(key);
					functionDictionary.Add(key, value);
				}

				// If we have already got value cached from e.g Fibonacci(i) or it has been added to values
				// there is no need to call the CPU expensive function because we already have the result in cache!
				// So we return the previously calculated value.
				return value;
			};
		}
	}
}
