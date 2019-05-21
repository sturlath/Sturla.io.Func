using Serilog;
using System;
using System.Threading;

namespace Sturla.io.Func.CachingOfExpensiveMethodCalls.Console
{
	static class Program
	{
		static void Main(string[] args)
		{
			#region Logging
			Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.WriteTo.Console()
			.CreateLogger();
			#endregion

			// Set up 
			// Wraps the Dictionary<TArgument, TResult> dictionary and the original function into a NEW function, 
			//that gets returned from the Memoize method).
			Fibonacci = Fibonacci.Memoize();

			// First run of two
			for (int i = 0; i < 50; i++)
				Log.Information("{fib}", Fibonacci(i));

			// Now every value is fetched from the Dictionary (not calculated)
			for (int i = 0; i < 50; i++)
				Log.Information("{fib}", Fibonacci(i));

			// Another example
			SuperComplexLowerCasing = SuperComplexLowerCasing.Memoize();

			// Some other super complex and time consuming lowercasing.
			Log.Information("{lower}", SuperComplexLowerCasing("ChachedFirstTime"));//runs for 5 sec
			Log.Information("{lower}", SuperComplexLowerCasing("ChachedFirstTime"));//will run fast because its cached
			Log.Information("{lower}", SuperComplexLowerCasing("NotCached")); //runs for 5 sec
			Log.Information("{lower}", SuperComplexLowerCasing("ChachedFirstTime"));//will run fast because its cached

			System.Console.ReadKey();
		}

		public static Func<int, int> Fibonacci = (n) =>
		{
			return n > 1 ? Fibonacci(n - 1) + Fibonacci(n - 2) : n;
		};

		/// <summary>
		/// Just some method that takes a long time
		/// </summary>
		public static Func<string, string> SuperComplexLowerCasing = (n) =>
		{
			Thread.Sleep(5000);
			// I just didn't come up with anything more clever. Need to find my old Calculus books.
			return n.ToLower();
		};
	}
}
